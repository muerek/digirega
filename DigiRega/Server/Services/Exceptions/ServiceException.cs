using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DigiRega.Server.Services.Exceptions
{
    /// <summary>
    /// Represents a known and expected error thrown in a service that should be presented as its corresponding HTTP status code.
    /// All service exceptions are supposed to be processed into proper service responses and not go to a generic HTTP 500 error.
    /// Different derived exceptions are translated to their HTTP status code in <see cref="ServiceExceptionFilter"/>.
    /// </summary>
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException() : base() { }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
