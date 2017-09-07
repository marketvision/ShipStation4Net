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
    public class FulfillmentsFilter : Filter
    {
        /// <summary>
        /// Returns the fulfillment with the specified fulfillment ID.
        /// </summary>
        public int? FulfillmentId { get; set; }

        /// <summary>
        /// Returns fulfillments whose orders have the specified order ID.
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Returns fulfillments whose orders have the specified order number.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Returns fulfillments with the specified tracking number.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Returns fulfillments shipped to the specified recipient name.
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Returns fulfillments with the shipDate on or after the specified date
        /// Example: 2015-01-01. 
        /// </summary>
        public DateTime? CreateDateStart { get; set; }

        /// <summary>
        /// Returns fulfillments with the shipDate on or before the specified date
        /// Example: 2015-01-08.
        /// </summary>
        public DateTime? CreateDateEnd { get; set; }

        /// <summary>
        /// Returns fulfillments with the shipDate on or after the specified date
        /// Example: 2015-01-01. 
        /// </summary>
        public DateTime? ShipDateStart { get; set; }

        /// <summary>
        /// Returns fulfillments with the shipDate on or before the specified date
        /// Example: 2015-01-08. 
        /// </summary>
        public DateTime? ShipDateEnd { get; set; }

        /// <summary>
        /// Sort the responses by a set value.The response will be sorted based off the ascending dates(oldest to most current.) If left
        /// empty, the response will be sorted by ascending createDate.
        /// Example: ShipDate.
        /// Possible values: 
        /// ShipDate, CreateDate.
        /// </summary>
        public FulfillmentsSortBy? SortBy { get; set; }
        
        public override HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();

            if (FulfillmentId != null)
            {
                filters.Add("fulfillmentId", FulfillmentId.Value.ToString());
            }
            if (OrderId != null)
            {
                filters.Add("orderId", OrderId.Value.ToString());
            }
            if (OrderNumber != null)
            {
                filters.Add("orderNumber", OrderNumber);
            }
            if (TrackingNumber != null)
            {
                filters.Add("trackingNumber", TrackingNumber);
            }
            if (RecipientName != null)
            {
                filters.Add("recipientName", RecipientName);
            }

            if (CreateDateStart != null)
            {
                filters.Add("createDateStart", CreateDateStart.Value.ToString());
            }
            if (CreateDateEnd != null)
            {
                filters.Add("createDateEnd", CreateDateEnd.Value.ToString());
            }
            if (ShipDateStart != null)
            {
                filters.Add("shipDateStart", ShipDateStart.Value.ToString());
            }
            if (ShipDateEnd != null)
            {
                filters.Add("shipDateEnd", ShipDateEnd.Value.ToString());
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
                filters.Add("sortBy", SortBy.ToString());
            }

            request.RequestUri = new Uri(string.Format("{0}?{1}", request.RequestUri, EncodeFilterString(filters)), UriKind.Relative);

            return request;
        }
    }
}
