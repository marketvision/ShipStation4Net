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
using ShipStation4Net.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ShipStation4Net.Filters
{
    public class ProductsFilter : Filter
    {
        /// <summary>
        /// Returns products that match the specified SKU.
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Returns products that match the specified product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns products that match the specified productCategoryId.
        /// </summary>
        public string ProductCategoryId { get; set; }

        /// <summary>
        /// Returns products that match the specified productTypeId.
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Returns products that match the specified tagId.
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// Returns products that were created after the specified date.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Returns products that were created before the specified date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Sorts the order of the response based off the specified value.
        /// Possible values: 
        /// SKU , ModifyDate , CreateDate.
        /// </summary>
        public ProductsSortBy? SortBy { get; set; }

        /// <summary>
        /// Specifies whether the list should include inactive products.
        /// Default: false. 
        /// </summary>
        public bool? ShowInactive { get; set; }

        public override HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();

            if (Sku != null)
            {
                filters.Add("sku", Sku);
            }
            if (Name != null)
            {
                filters.Add("name", Name);
            }
            if (ProductCategoryId != null)
            {
                filters.Add("productCategoryId", ProductCategoryId);
            }
            if (ProductTypeId != null)
            {
                filters.Add("productTypeId", ProductTypeId);
            }
            if (TagId != null)
            {
                filters.Add("tagId", TagId);
            }
            if (StartDate != null)
            {
                filters.Add("startDate", StartDate.Value.ToString());
            }
            if (EndDate != null)
            {
                filters.Add("endDate", EndDate.Value.ToString());
            }
            if (ShowInactive != null)
            {
                filters.Add("showInactive", ShowInactive.Value.ToString());
            }

            if (PageSize != null)
            {
                filters.Add("pageSize", PageSize.Value.ToString());
            }
            if (Page != null)
            {
                filters.Add("page", Page.Value.ToString());
            }

            if (SortBy != null)
            {
                filters.Add("sortBy", JsonConvert.SerializeObject(SortBy).Trim('\"'));
            }

            request.RequestUri = new Uri(string.Format("{0}?{1}", request.RequestUri, EncodeFilterString(filters)), UriKind.Relative);

            return request;
        }
    }
}
