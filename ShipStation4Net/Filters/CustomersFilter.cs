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
using System.Collections.Generic;

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

        /// <summary>
        /// Sets the direction of the sort order.
        /// </summary>
        public SortDir? SortDir { get; set; }

        protected override Dictionary<string, object> GetFilters()
        {
            var res = base.GetFilters();

            res["stateCode"] = StateCode;
            res["countryCode"] = CountryCode;
            res["marketplaceId"] = MarketplaceId;
            res["tagId"] = TagId;
            res["sortBy"] = SortBy;
            res["sortDir"] = SortDir;

            return res;
        }
    }
}
