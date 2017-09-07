#region License
/*
 * Copyright 2017 Brandon James
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
#endregion

using ShipStation4Net.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ShipStation4Net.Filters
{
    public class Filter : IFilter
    {
        /// <summary>
        /// Sets the direction of the sort order.
        /// </summary>
        public SortDir? SortDir { get; set; }

        /// <summary>
        /// Page number.
        /// Default: 1.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Requested page size.Max value is 500.
        /// Default: 100. 
        /// </summary>
        public int? PageSize { get; set; }

        public virtual HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();
            
            if (PageSize != null)
            {
                PageSize = (PageSize < 500) ? PageSize : 500;
                filters.Add("pageSize", PageSize.Value.ToString());
            }
            if (Page != null)
            {
                filters.Add("page", Page.Value.ToString());
            }
            if (SortDir != null)
            {
                filters.Add("sortDir", SortDir.Value.ToString());
            }

            request.RequestUri = new Uri(string.Format("{0}?{1}", request.RequestUri, EncodeFilterString(filters)));

            return request;
        }

        public string EncodeFilterString(IDictionary<string, string> filters)
        {
            return string.Join("&",
                    filters.Select(kvp =>
                    string.Format("{0}={1}", kvp.Key, HttpUtility.UrlEncode(kvp.Value))));
        }
    }
}
