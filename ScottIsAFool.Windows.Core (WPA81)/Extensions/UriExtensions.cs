﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Cimbalino.Toolkit.Extensions;

namespace ScottIsAFool.Windows.Core.Extensions
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for <see cref="Uri"/> instances.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets a collection of query string values.
        /// </summary>
        /// <param name="uri">The current uri.</param>
        /// <returns>A collection that contains the query string values.</returns>
        public static IDictionary<string, string> QueryDictionary(this Uri uri)
        {
            return uri.QueryString().ToDictionary(x => WebUtility.UrlDecode(x.Key), x => WebUtility.UrlDecode(x.Value));
        }
    }
}
