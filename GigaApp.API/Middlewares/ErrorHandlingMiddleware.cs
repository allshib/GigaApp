

using FluentValidation;
using GigaApp.API.Middlewares;
using GigaApp.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Data;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {

            ProblemDetails problemDetails;
            switch (exception)
            {
                case IntetntionManagerExeption intetntionManagerExeption:
                    problemDetails = problemDetailsFactory.CreateForm(httpContext, intetntionManagerExeption);
                    break;
                case ValidationException validationException:
                    problemDetails = problemDetailsFactory.CreateForm(httpContext, validationException);
                    logger.LogInformation(exception, "OOPS!");
                    break;
                case DomainException domainException:
                    problemDetails = problemDetailsFactory.CreateForm(httpContext, domainException);
                    logger.LogError(domainException,"Domain Exception occured");
                    break;
                default:
                    problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    StatusCodes.Status500InternalServerError, "Unhandled Error");
                    logger.LogError(exception, "Unhandled exception occured");
                    break;
            }

            httpContext.Response.StatusCode = problemDetails?.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails?.GetType()?? typeof(ProblemDetails));
        }
    }
}