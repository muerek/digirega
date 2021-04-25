using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services.Exceptions
{
    /// <summary>
    /// Represents an error caused because a service could not find a resource.
    /// </summary>
    public class NotFoundServiceException : ServiceException
    {
        public NotFoundServiceException() : base() { }

        public NotFoundServiceException(string message) : base(message) { }

        public NotFoundServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
