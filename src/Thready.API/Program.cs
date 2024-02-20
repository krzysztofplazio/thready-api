using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Thready.Application.Exceptions.Users;
using Thready.Application.ProblemDetails.Users;
using Thready.Application.Validators;
using Thready.Core.Models;
using Thready.Core.Repositories.Users;
using Thready.Infrastructure.Contexts;
using Thready.Infrastructure.Repositories.Users;
using Hellang.Middleware.ProblemDetails;
using Serilog;
using MediatR;
using FluentValidation.AspNetCore;
using FluentValidation;
using Thready.Application.Dtos.Authentication;
using Microsoft.IdentityModel.Tokens;

var AppCorsPolicy = "_appCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(setup =>
{
    setup.IncludeExceptionDetails = (_, _) =>
           builder.Environment.IsDevelopment()
        || builder.Environment.IsStaging();
    setup.Map<UserException>(ex => new UserExceptionDetails(ex));
});
// Add services to the container.

builder.Host.UseSerilog((ctx, loggerConfiguration) => 
{    
    loggerConfiguration
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Thready.API", typeof(Program).Assembly.GetName().Name)
        .Enrich.WithProperty("Environment", ctx.HostingEnvironment);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AppCorsPolicy,
                      policy  =>
                      {
                          policy.SetIsOriginAllowed(_ => true);
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

builder.Services.AddControllers(config => 
{
    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
})
.AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = Assembly.Load("Thready.Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<ThreadyDatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ThreadyDatabase"), 
                      conf => conf.MigrationsAssembly("Thready.Infrastructure"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = "COOKIE";
    options.DefaultChallengeScheme = "COOKIE";
})
.AddCookie(options =>
{
    options.LoginPath = "/api/auth/login";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.Events.OnRedirectToAccessDenied = (context) => 
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToLogin = (context) => 
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
})
.AddPolicyScheme("COOKIE", "COOKIE", options =>
{
    options.ForwardDefaultSelector = context =>
    {       
        return CookieAuthenticationDefaults.AuthenticationScheme;
    };
});


var app = builder.Build();
app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors(AppCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
