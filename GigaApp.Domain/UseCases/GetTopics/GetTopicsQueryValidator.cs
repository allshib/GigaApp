﻿using FluentValidation;
using GigaApp.Domain.Exceptions;

namespace GigaApp.Domain.UseCases.GetTopics
{
    internal class GetTopicsQueryValidator : AbstractValidator<GetTopicsQuery>
    {
        public GetTopicsQueryValidator() {
        
            RuleFor(q=>q.ForumId).NotEmpty().WithErrorCode(ValidationErrorCode.Empty);

            RuleFor(q=>q.Skip).GreaterThanOrEqualTo(0).WithErrorCode(ValidationErrorCode.Invalid);
            RuleFor(q=>q.Take).GreaterThanOrEqualTo(0).WithErrorCode(ValidationErrorCode.Invalid);
        }
    }
}
