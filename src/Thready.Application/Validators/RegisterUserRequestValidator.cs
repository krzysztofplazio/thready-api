using FluentValidation;
using Thready.Application.Dtos.Users;
using Thready.Application.Utils;

namespace Thready.Application.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(30)
            .MinimumLength(2);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(30)
            .MinimumLength(2);

        RuleFor(x => x.Username)
           .NotEmpty()
           .MaximumLength(15)
           .MinimumLength(3);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(21);
    }
}
