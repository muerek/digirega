using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Utilities
{
    /// <summary>
    /// Holds extension methods for <see cref="NavigationManager"/>.
    /// </summary>
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Tries to parse the current URI for query parameters, find the given key and return the corresponding value.
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="key">Query parameter key to look for.</param>
        /// <param name="value">Value for the given key.</param>
        /// <returns></returns>
        public static bool TryGetQueryParameter(this NavigationManager navigation, string key, out StringValues value)
        {
            var query = new Uri(navigation.Uri).Query;
            var parameters = QueryHelpers.ParseQuery(query);
            return parameters.TryGetValue(key, out value);
        }

        /// <summary>
        /// Navigates to the specified URI after adding all given query parameters to it.
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="uri">URI to navigate to.</param>
        /// <param name="parameters">Dictionary containing all query parameters.</param>
        /// <param name="forceLoad"></param>
        public static void NavigateToWithQueryString(this NavigationManager navigation, string uri,
            Dictionary<string, string> parameters, bool forceLoad = false)
        {
            var uriWithQuery = QueryHelpers.AddQueryString(uri, parameters);
            navigation.NavigateTo(uriWithQuery, forceLoad);
        }
    }
}
