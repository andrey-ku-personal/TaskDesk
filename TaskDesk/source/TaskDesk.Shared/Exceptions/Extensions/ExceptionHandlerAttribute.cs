using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TaskDesk.Shared.Exceptions.Extensions;

public class ExceptionHandlerAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BadRequestException b:
                HandleBadRequest(context);
                break;
            case NotFoundException n:
                HandleNotFound(context);
                break;

            case ValidationException v:
                HandleNotValid(context);
                break;
            default:
                HandleInternalError(context);
                break;
        }
    }

    private void HandleNotFound(ExceptionContext context)
    {
        var deatils = new ProblemDetails()
        {
            Status = (int)HttpStatusCode.NotFound,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = context.Exception.Message,
            Instance = context.Exception.Source,
            Detail = context.Exception.StackTrace
        };

        context.Result = new ObjectResult(deatils)
        {
            StatusCode = (int)HttpStatusCode.NotFound
        };

        context.ExceptionHandled = true;
    }

    private void HandleNotValid(ExceptionContext context)
    {
        HandleBadRequest(context);
    }

    private void HandleBadRequest(ExceptionContext context)
    {
        var deatils = new ProblemDetails()
        {
            Status = (int)HttpStatusCode.BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = context.Exception.Message,
            Instance = context.Exception.Source,
            Detail = context.Exception.StackTrace
        };

        context.Result = new ObjectResult(deatils)
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };

        context.ExceptionHandled = true;
    }

    private void HandleInternalError(ExceptionContext context)
    {
        var deatils = new ProblemDetails()
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = context.Exception.Message,
            Instance = context.Exception.Source,
            Detail = context.Exception.StackTrace
        };

        context.Result = new ObjectResult(deatils)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
