using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Client.Services.Exceptions
{
    /// <summary>
    /// Represents an error caused by insufficient authorization, indicated by an HTTP 403 status code.
    /// </summary>
    public class ForbiddenServiceException : ServiceException
    {
        public ForbiddenServiceException() { }

        public ForbiddenServiceException(string? message) : base(message) { }

        public ForbiddenServiceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
