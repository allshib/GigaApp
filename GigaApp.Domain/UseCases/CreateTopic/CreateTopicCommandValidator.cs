﻿using FluentValidation;
using GigaApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    internal class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
    {
        public CreateTopicCommandValidator()
        {
            RuleFor(c => c.ForumId).NotEmpty().WithErrorCode(ValidationErrorCode.Empty);
            RuleFor(c => c.Title).Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode(ValidationErrorCode.Empty)
                .MaximumLength(100).WithErrorCode(ValidationErrorCode.TooLong);
        }
    }
}
