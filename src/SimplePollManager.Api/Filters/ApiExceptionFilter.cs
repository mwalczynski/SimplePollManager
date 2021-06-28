namespace SimplePollManager.Api.Filters
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using SimplePollManager.Application.Common.Exceptions;

    public class ApiExceptionFilter : ExceptionFilterAttribute
    {

        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;

        public ApiExceptionFilter()
        {
            this.exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BusinessRuleValidationException), this.HandleBusinessRuleValidationException },
                { typeof(ValidationException), this.HandleValidationException },
                { typeof(NotFoundException), this.HandleNotFoundException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            this.HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (this.exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }

            this.HandleUnknownException(context);
        }

        private void HandleBusinessRuleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as BusinessRuleValidationException;

            var details = new ProblemDetails()
            {
                Title = "Business rule broken",
                Status = StatusCodes.Status409Conflict,
                Detail = exception.Details,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            };

            context.Result = new ConflictObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
