using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Client.Services.Exceptions
{
    /// <summary>
    /// Represents an error occuring during backend communication in the services.
    /// </summary>
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException() { }

        public ServiceException(string? message) : base(message) { }

        public ServiceException(string? message, Exception? innerException) : base(message, innerException) { }

        /// <summary>
        /// Creates the most specific <see cref="ServiceException"/> for an <see cref="HttpRequestException"/>.
        /// </summary>
        /// <param name="httpRequestException"><see cref="HttpRequestException"/> thrown.</param>
        /// <param name="message">Message describing the current situation.</param>
        /// <returns><see cref="ServiceException"/> matching the HTTP error.</returns>
        public static ServiceException FromHttpRequestException(HttpRequestException httpRequestException, string? message = null)
        {
            return FromHttpStatus(httpRequestException.StatusCode ?? HttpStatusCode.BadRequest, message ?? httpRequestException.Message);
        }

        /// <summary>
        /// Creates the most specific <see cref="ServiceException"/> for the given HTTP status code.
        /// </summary>
        /// <param name="statusCode">HTTP status code.</param>
        /// <param name="message">Message describing the current situation.</param>
        /// <returns><see cref="ServiceException"/> matching the HTTP status code.</returns>
        public static ServiceException FromHttpStatus(HttpStatusCode statusCode, string? message = null)
        {
            return statusCode switch
            {
                < HttpStatusCode.BadRequest => throw new InvalidOperationException("Status code needs to indicate an error (400 or greater)."),
                >= HttpStatusCode.InternalServerError => new ServerServiceException(message),
                HttpStatusCode.Forbidden => new ForbiddenServiceException(message),
                HttpStatusCode.Unauthorized => new UnauthorizedServiceException(message),
                _ => new ServiceException(message)
            };
        }
    }
}
