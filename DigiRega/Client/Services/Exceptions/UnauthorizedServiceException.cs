using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services.Exceptions
{
    /// <summary>
    /// Represents an error caused by missing authorization, indicated by an HTTP 401 status code.
    /// </summary>
    public class UnauthorizedServiceException : ServiceException
    {
        public UnauthorizedServiceException() { }

        public UnauthorizedServiceException(string? message) : base(message) { }

        public UnauthorizedServiceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
