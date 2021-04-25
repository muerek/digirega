using DigiRega.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services.Exceptions
{
    /// <summary>
    /// Filter to handle <see cref="ServiceException"/>.
    /// </summary>
    /// <remarks>
    /// See instructions here: https://docs.microsoft.com/de-de/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0#validation-failure-error-response
    /// </remarks>
    public class ServiceExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<ServiceExceptionFilter> logger;

        public int Order => int.MaxValue - 10;

        public ServiceExceptionFilter(ILogger<ServiceExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ServiceException serviceException)
            {                
                // Wrap the error message into a payload-less service response DTO.
                var response = new ServiceResponse()
                {
                    Success = false,
                    Message = serviceException.Message
                };

                // Different types of exceptions may correspond to specific HTTP status codes.
                context.Result = new ObjectResult(response)
                {
                    StatusCode = serviceException switch
                    {
                        NotFoundServiceException => StatusCodes.Status404NotFound,
                        UnauthorizedServiceException => StatusCodes.Status401Unauthorized,
                        ForbiddenServiceException => StatusCodes.Status403Forbidden,
                        _ => StatusCodes.Status400BadRequest
                    }
                };

                // Avoid other exceptions handling that messes with what we did here.
                context.ExceptionHandled = true;

                logger.LogWarning(serviceException, "Handled service exception and returned appropriate HTTP status code.");
            }
            else if (context.Exception != null)
            {
                logger.LogError(context.Exception, "Unhandled exception.");
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
