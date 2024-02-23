using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace GigaApp.API.Middlewares
{
    public static class ProblemDetailsFactoryEx
    {
        public static ProblemDetails CreateForm(this ProblemDetailsFactory factory, HttpContext httpContext, IntetntionManagerExeption intentionManagerException)
        => factory.CreateProblemDetails(httpContext,
                StatusCodes.Status403Forbidden,
                "Authorization failed",
                detail: intentionManagerException.Message);
        


        public static ProblemDetails CreateForm(this ProblemDetailsFactory factory, HttpContext httpContext,  DomainException domainException)
        => factory.CreateProblemDetails(httpContext,
                domainException.ErrorCode switch
                {
                    DomainErrorCode.Gone => StatusCodes.Status410Gone,
                    _ => StatusCodes.Status500InternalServerError
                },
                "Authorization failed",
                detail: domainException.Message);
        

        public static ProblemDetails CreateForm(this ProblemDetailsFactory factory, HttpContext httpContext, ValidationException validationException)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach(var error in validationException.Errors)
            {
                modelStateDictionary.AddModelError(error.PropertyName, error.ErrorCode);
            }

            return factory.CreateValidationProblemDetails(httpContext,
                modelStateDictionary,
                StatusCodes.Status400BadRequest,
                "Validation failed");
        }
    }
}
