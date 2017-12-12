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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ShipStation4Net.Filters
{
    public class Filter : IFilter
    {
        public Filter()
        {
            Page = 1;
            PageSize = 100;
        }

        /// <summary>
        /// Page number.
        /// Default: 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Requested page size.Max value is 500.
        /// Default: 100. 
        /// </summary>
        public int PageSize { get; set; }

        protected virtual Dictionary<string, object> GetFilters()
        {
            if (Page < 1) throw new ArgumentException(nameof(Page), "Cannot be a negative or zero");
            if (PageSize < 1 || PageSize > 500) throw new ArgumentOutOfRangeException(nameof(PageSize), "Should be in range 1..500");

            return new Dictionary<string, object>()
            {
                {"page", Page},
                {"pageSize", PageSize}
            };
        }

        public virtual HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = GetFilters();

            request.RequestUri = new Uri($"{request.RequestUri}?{EncodeFilterString(filters)}", UriKind.Relative);

            return request;
        }

        public string EncodeFilterString(IDictionary<string, object> filters)
        {
            foreach (var item in filters.ToList())
            {
                if (item.Value == null)
                {
                    filters.Remove(item.Key);
                    continue;
                }

                if (item.Value.GetType() == typeof(string))
                {
                    continue;
                }

                filters[item.Key] = JsonConvert.SerializeObject(item.Value, ClientBase.SerializerSettings).Trim('\"');
            }

            return string.Join("&", filters.Select(kvp => $"{kvp.Key}={HttpUtility.UrlEncode(kvp.Value.ToString())}"));
        }
    }
}
