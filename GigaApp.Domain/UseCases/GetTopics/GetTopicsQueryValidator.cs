using FluentValidation;
using GigaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
