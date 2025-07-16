using FluentValidation;
using GigaApp.Domain.Exceptions;

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
