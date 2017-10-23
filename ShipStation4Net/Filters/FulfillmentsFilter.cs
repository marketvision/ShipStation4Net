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

        /// <summary>
        /// Sets the direction of the sort order.
        /// </summary>
        public SortDir? SortDir { get; set; }


        protected override Dictionary<string, object> GetFilters()
        {
            var res = base.GetFilters();

            res["fulfillmentId"] = FulfillmentId;
            res["orderId"] = OrderId;
            res["orderNumber"] = OrderNumber;
            res["trackingNumber"] = TrackingNumber;
            res["recipientName"] = RecipientName;
            res["createDateStart"] = CreateDateStart;
            res["createDateEnd"] = CreateDateEnd;
            res["shipDateStart"] = ShipDateStart;
            res["shipDateEnd"] = ShipDateEnd;
            res["sortBy"] = SortBy;
            res["sortDir"] = SortDir;

            return res;
        }
    }
}
