using FluentValidation;
using FluentValidation.Validators;
using GigaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
