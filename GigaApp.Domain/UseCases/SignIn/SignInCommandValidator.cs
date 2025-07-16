using FluentValidation;
using GigaApp.Domain.Exceptions;

namespace GigaApp.Domain.UseCases.SignIn
{
    internal class SignInCommandValidator:AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(c => c.Login).NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(20).WithErrorCode(ValidationErrorCode.TooLong);
            RuleFor(c => c.Password).NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
        }
    }
}
