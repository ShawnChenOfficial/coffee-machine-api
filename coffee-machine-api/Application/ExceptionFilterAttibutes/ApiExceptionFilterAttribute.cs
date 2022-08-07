using coffee_machine_api.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace coffee_machine_api.Application.ExceptionFilterAttibutes
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute()
        {
            // define what are the known exception types
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(StatusCodeBasedValidationException), HandleStatusCodeBasedValidationException },
                { typeof(ValidationException), HandleValidationException },
                { typeof(ResourceRequestErrorException), HandlerResourceRequestErrorException }
            };
        }

        /// <summary>
        /// override the default exception handler for MVC pipeline
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleStatusCodeBasedValidationException(ExceptionContext context)
        {
            var exception = (StatusCodeBasedValidationException)context.Exception;

            var details = new ProblemDetails
            {
                Status = exception!.GetStatusCode(),
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = exception.GetStatusCode()
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;

            var erros = exception.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            var details = new ValidationProblemDetails(erros);

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandlerResourceRequestErrorException(ExceptionContext context)
        {
            var exception = (ResourceRequestErrorException)context.Exception;

            var details = new ProblemDetails
            {
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
                Title = "An error occurred while processing your request."
            };

            context.Result = new BadRequestObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}

