using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Thready.API.Contexts;
using Thready.API.Exceptions.Users;
using Thready.API.ProblemDetails.Users;
using Thready.API.Repositories.Users;
using Thready.Models.Models;

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

builder.Services.AddControllers()
                .AddJsonOptions(options => 
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddDbContext<ThreadyDatabaseContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("TheradyDatabase")));

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret"])),
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
