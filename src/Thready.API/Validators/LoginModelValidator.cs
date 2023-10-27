using FluentValidation;
using Thready.API.Dtos.Authentication;

namespace Thready.API.Validators;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Username)
            .MaximumLength(15)
            .MinimumLength(3);
        
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .MaximumLength(21);
    }
}