using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto
{
    /// <summary>
    /// Wrapper for service responses without payload.
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Flag to indicate successful execution of the client's request.
        /// </summary>
        public bool Success { get; init; } = true;

        /// <summary>
        /// Additional information on the response, in particular any errors.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; init; }

        /// <summary>
        /// Creates a new service response, by default successful without message.
        /// </summary>
        public ServiceResponse() { }
    }

    /// <summary>
    /// Wrapper for service responses with payload.
    /// </summary>
    /// <typeparam name="TData">Type of the wrapped payload.</typeparam>
    public class ServiceResponse<TData> : ServiceResponse
    {
        /// <summary>
        /// The payload requested by the client.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData Data { get; init; }

        /// <summary>
        /// Wraps the data into a new service response indicating successful execution by default.
        /// </summary>
        /// <param name="data"></param>
        public ServiceResponse(TData data)
        {
            Data = data;
            Success = true;
        }

        /// <summary>
        /// Wraps payload data into an service response using implicit casting.
        /// </summary>
        /// <param name="data">Data to be wrapped.</param>
        /// <remarks>This will set all properties but the data to their default values.</remarks>
        public static implicit operator ServiceResponse<TData>(TData data)
        {
            return new ServiceResponse<TData>(data);
        }

        /// <summary>
        /// Unwraps payload data from an service response using implicit casting.
        /// </summary>
        /// <param name="serviceResponse">Service response with wrapped payload.</param>
        public static implicit operator TData(ServiceResponse<TData> serviceResponse)
        {
            return serviceResponse.Data;
        }
    }
}
