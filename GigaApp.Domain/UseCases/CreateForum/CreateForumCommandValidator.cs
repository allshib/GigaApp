using FluentValidation;
using FluentValidation.Validators;
using GigaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateForum
{
    /// <summary>
    /// Валидатор, проверяющий создание форума
    /// </summary>
    public class CreateForumCommandValidator : AbstractValidator<CreateForumCommand>
    {
        public CreateForumCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(100).WithErrorCode(ValidationErrorCode.TooLong);

        }
    }
}
