using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto
{
    /// <summary>
    /// Wrapper for service responses with paged data.
    /// </summary>
    /// <typeparam name="TData">Type of the wrapped payload.</typeparam>
    public class PagedServiceResponse<TData> : ServiceResponse<TData>
    {
        /// <summary>
        /// Total count of elements.
        /// </summary>
        public int Total { get; init; }
        
        /// <summary>
        /// Count of elements skipped before those in this response.
        /// </summary>
        public int Offset { get; init; }
        
        /// <summary>
        /// Count of elements returned in this response.
        /// </summary>
        public int Count { get; init; }

        public PagedServiceResponse(TData data) : base(data)
        {
            if (data is ICollection collection)
            {
                Count = collection.Count;
            }
        }

        public PagedServiceResponse(TData data, int total, int count, int offset) : base(data)
        {
            Total = total;
            Offset = offset;
            Count = count;
        }
    }
}
