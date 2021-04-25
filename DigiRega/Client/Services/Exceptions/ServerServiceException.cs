using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services.Exceptions
{
    /// <summary>
    /// Represents a server-side failure, indicated by an HTTP status code 500 or higher.
    /// </summary>
    public class ServerServiceException : ServiceException
    {
        public ServerServiceException() { }

        public ServerServiceException(string? message) : base(message) { }

        public ServerServiceException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
