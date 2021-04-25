using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services.Exceptions
{
    /// <summary>
    /// Represents an error caused because the user is not authorized for an operation requiring authorization.
    /// </summary>
    /// <remarks>
    /// If the user is authenticated and presents valid authorization, but it does not cover the requested operation, throw <see cref="ForbiddenServiceException"/>
    /// </remarks>
    public class UnauthorizedServiceException : ServiceException
    {
        public UnauthorizedServiceException() { }

        public UnauthorizedServiceException(string message) : base(message) { }

        public UnauthorizedServiceException(string message, Exception innerException) : base(message, innerException) { }

    }
}
