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
    public class OrdersFilter : Filter
    {
        /// <summary>
        /// Sort the responses by a set value.The response will be sorted based off the ascending dates(oldest to most current.) If left empty, the response will be sorted by ascending orderId.
        /// Example: OrderDate
        /// </summary>
        public OrdersSortBy? SortBy { get; set; }

        /// <summary>
        /// Returns orders that were created in ShipStation after the specified date
        /// Example: 2015-01-01 00:00:00. 
        /// </summary>
        public DateTime? CreateDateStart { get; set; }

        /// <summary>
        /// Returns fulfillments created on or after the specified createDate
        /// Example: 2015-01-01 00:00:00. 
        /// </summary>
        public DateTime? CreateDateEnd { get; set; }

        /// <summary>
        /// Returns orders greater than the specified date
        /// Example: 2015-01-01 00:00:00.
        /// </summary>
        public DateTime? ModifyDateStart { get; set; }

        /// <summary>
        /// Returns orders less than or equal to the specified date
        /// Example: 2015-01-08 00:00:00. 
        /// </summary>
        public DateTime? ModifyDateEnd { get; set; }

        /// <summary>
        /// ID of the tag. Call Accounts/ListTags to obtain a list of tags for this account.
        /// </summary>
        public int? TagId { get; set; }

        /// <summary>
        /// The order's status.
        /// Possible values: 
        /// awaiting_payment, awaiting_shipment, pending_fulfillment, shipped, on_hold, cancelled.
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }

        public override HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();

            if (CreateDateStart != null)
            {
                filters.Add("createDateStart", CreateDateStart.Value.ToString());
            }
            if (CreateDateEnd != null)
            {
                filters.Add("createDateEnd", CreateDateEnd.Value.ToString());
            }
            if (ModifyDateStart != null)
            {
                filters.Add("modifyDateStart", ModifyDateStart.Value.ToString());
            }
            if (ModifyDateEnd != null)
            {
                filters.Add("modifyDateEnd", ModifyDateEnd.Value.ToString());
            }
            if (TagId != null)
            {
                filters.Add("tagId", TagId.Value.ToString());
            }
            if (OrderStatus != null)
            {
                filters.Add("orderStatus", JsonConvert.SerializeObject(OrderStatus).Trim('\"'));
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
                filters.Add("sortBy", SortBy.Value.ToString());
            }
            
            request.RequestUri = new Uri(string.Format("{0}?{1}", request.RequestUri, EncodeFilterString(filters)), UriKind.Relative);

            return request;
        }
    }
}
