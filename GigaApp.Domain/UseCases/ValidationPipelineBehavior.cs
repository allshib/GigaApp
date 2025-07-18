﻿using FluentValidation;
using MediatR;

namespace GigaApp.Domain.UseCases
{
    internal class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> validator;

        public ValidationPipelineBehavior(IValidator<TRequest> validator)
        {
            this.validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
            return await next();
        }
    }
}
