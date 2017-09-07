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
using System.Net.Http;

namespace ShipStation4Net.Filters
{
    public class CustomersFilter : Filter
    {
        /// <summary>
        /// Returns customers that reside in the specified stateCode.
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// Returns customers that reside in the specified countryCode.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Returns customers that purchased items from the specified marketplaceId
        /// </summary>
        public int? MarketplaceId { get; set; }

        /// <summary>
        /// Returns customers that have been tagged with the specified tagId.
        /// </summary>
        public int? TagId { get; set; }

        /// <summary>
        /// Sorts the order of the response based off the specified value.
        /// Possible values:
        /// Name , ModifyDate , CreateDate.
        /// </summary>
        public CustomersSortBy? SortBy { get; set; }
        
        public override HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();

            if (StateCode != null)
            {
                filters.Add("stateCode", StateCode);
            }

            if (CountryCode != null)
            {
                filters.Add("countryCode", CountryCode);
            }

            if (MarketplaceId != null)
            {
                filters.Add("marketplaceId", MarketplaceId.Value.ToString());
            }

            if (TagId != null)
            {
                filters.Add("tagId", TagId.Value.ToString());
            }

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

            request.RequestUri = new Uri(string.Format("{0}?{1}", request.RequestUri, EncodeFilterString(filters)), UriKind.Relative);

            return request;
        }
    }
}
