using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services.Exceptions
{
    /// <summary>
    /// Represents an error caused by insufficient authorization of the user.
    /// The user is authenticated but does not have sufficient privileges to complete the operation.
    /// </summary>
    /// <remarks>
    /// If the user is not authorized or presents invalid authorization data, throw <see cref="UnauthorizedServiceException"/>.
    /// </remarks>
    public class ForbiddenServiceException : ServiceException
    {
        public ForbiddenServiceException() : base() { }

        public ForbiddenServiceException(string message) : base(message) { }

        public ForbiddenServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
