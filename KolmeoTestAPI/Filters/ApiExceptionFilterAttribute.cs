using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace KolmeoTestAPI
{
    class ErrorNode
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
    }

    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //private readonly ILogger _logger;
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        //public ApiExceptionFilterAttribute(ILogger logger)
        public ApiExceptionFilterAttribute()
        {
            //_logger = logger;

            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(NotFoundException), HandleObjectNotFoundException },
                { typeof(ValidationException), HandleValidationException }
            };

        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType().BaseType;

            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }


        }

        private void HandleValidationException(ExceptionContext context)
        {
            List<string> errors = new();
            var exception = context.Exception as ValidationException;
            foreach (var e in exception.Errors)
            {
                errors.Add(e);
            }
            context.Result = new BadRequestObjectResult(errors);

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = context.Exception as ErrorCodeException;
            List<string> errors = new();

            errors.Add(exception.ErrorCode.ToString());

            context.Result = new ObjectResult(errors)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }

        private void HandleObjectNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as ErrorCodeException;
            List<string> errors = new();

            errors.Add(exception.ErrorCode.ToString());

            context.Result = new ObjectResult(errors)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            context.ExceptionHandled = true;
        }
    }
}
