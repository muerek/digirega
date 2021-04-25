using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    /// <summary>
    /// Base class with common functionality for any services in this app.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Geduwino's data context to access the database via EF Core.
        /// </summary>
        protected readonly DigiRegaDbContext db;

        /// <summary>
        /// Mapper configured for DTO-from/to-model mapping for Geduwino.
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Provides information on the HTTP request the current operation resulted from.
        /// In particular, it is used to find out which user triggered it.
        /// </summary>
        protected readonly IHttpContextAccessor httpContextAccessor;
        
        /// <summary>
        /// Logger.
        /// </summary>
        protected readonly ILogger<ServiceBase> logger;

        /// <summary>
        /// Initializes a new service with all necessary dependencies.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        public ServiceBase(
            DigiRegaDbContext db,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ServiceBase> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a new service response indicating success without further payload or message.
        /// </summary>
        /// <returns>An empty service response indicating success.</returns>
        protected ServiceResponse SuccessfulServiceResponse()
        {
            return new ServiceResponse();
        }

        /// <summary>
        /// Creates a new service response indicating success with a message but without further payload.
        /// </summary>
        /// <param name="message">The message to be included in the service response.</param>
        /// <returns>A service response indicating success with the specified message.</returns>
        protected ServiceResponse SuccessfulServiceResponse(string message)
        {
            return new ServiceResponse()
            {
                Message = message
            };
        }

        /// <summary>
        /// Creates a new service response indicating failure without further payload or message.
        /// </summary>
        /// <returns>An empty service response indicating failure.</returns>
        protected ServiceResponse UnsuccessfulServiceResponse()
        {
            return new ServiceResponse()
            {
                Success = false
            };
        }

        /// <summary>
        /// Creates a new service response indicating failure with a message but without further payload.
        /// </summary>
        /// <param name="message">The message to be included in the service response.</param>
        /// <returns>A service response indicating failure with the specified message.</returns>
        protected ServiceResponse UnsuccessfulServiceResponse(string message)
        {
            return new ServiceResponse()
            {
                Success = false,
                Message = message
            };
        }

        /// <summary>
        /// Gets the unique identifier of the user who has triggered the current operation.
        /// </summary>
        /// <returns>Unique identifier of the user. Null if none.</returns>
        protected int? GetUserId()
        {
            var idClaim = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(idClaim, out int id))
            {
                return id;
            }
            return null;
        }

        /// <summary>
        /// Gets the user who has triggered the current operation.
        /// This returns the full user model object from the database.
        /// Use <see cref="GetUserId"/> if the unique ID is enough.
        /// </summary>
        /// <returns>The user who triggered the current operation.</returns>
        protected async Task<User> GetUserAsync()
        {
            var user = await db.Users.SingleOrDefaultAsync(u => u.Id == GetUserId());
            if (user == null)
            {
                throw new Exception("User unknown.");
            }
            return user;
        }
    }
}