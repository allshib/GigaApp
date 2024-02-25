using FluentValidation;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.UseCases.SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.SignOn
{
    internal class SignOnCommandValidator : AbstractValidator<SignOnCommand>
    {
        public SignOnCommandValidator()
        {
            RuleFor(c => c.Login).NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(20).WithErrorCode(ValidationErrorCode.TooLong);
            RuleFor(c => c.Password).NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
        }
    }
}
