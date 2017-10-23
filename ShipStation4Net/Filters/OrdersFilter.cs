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

namespace ShipStation4Net.Filters
{
    public class OrdersFilter : Filter
    {
        /// <summary>
        /// Returns orders that atch the specified name. 
        /// Example: Smith.
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Returns orders that contain items that match the specified keyword.
        /// Fields searched are Sku, Description, and Options
        /// Example: ABC123.
        /// </summary>
        public string ItemKeyword { get; set; }

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
        /// Returns orders greater than the specified date
        /// Example: 2015-01-01 00:00:00.
        /// </summary>
        public DateTime? OrderDateStart { get; set; }

        /// <summary>
        /// Returns orders less than or equal to the specified date
        /// Example: 2015-01-08 00:00:00.
        /// </summary>
        public DateTime? OrderDateEnd { get; set; }

        /// <summary>
        /// Filter by order number, performs a "starts with" search.
        /// Example: 12345.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// The order's status.
        /// Possible values: 
        /// awaiting_payment, awaiting_shipment, pending_fulfillment, shipped, on_hold, cancelled.
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }

        /// <summary>
        /// Returns orders that were paid after the specified date 
        /// Example: 2015-01-01.
        /// </summary>
        public DateTime? PaymentDateStart { get; set; }

        /// <summary>
        /// Returns orders that were paid before the specified date 
        /// Example: 2015-01-08.
        /// </summary>
        public DateTime? PaymentDateEnd { get; set; }

        /// <summary>
        /// Filters orders to a single store. Call List Stores to obtain a list of store Ids. Example: 123456.
        /// </summary>
        public int? StoreId { get; set; }

        /// <summary>
        /// Sort the responses by a set value.The response will be sorted based off the ascending dates(oldest to most current.) If left empty, the response will be sorted by ascending orderId.
        /// Example: OrderDate
        /// </summary>
        public OrdersSortBy? SortBy { get; set; }

        /// <summary>
        /// Sets the direction of the sort order.
        /// </summary>
        public SortDir? SortDir { get; set; }

        protected override Dictionary<string, object> GetFilters()
        {
            var res = base.GetFilters();

            res["customerName"] = CustomerName;
            res["itemKeyword"] = ItemKeyword;
            res["createDateStart"] = CreateDateStart;
            res["createDateEnd"] = CreateDateEnd;
            res["modifyDateStart"] = ModifyDateStart;
            res["modifyDateEnd"] = ModifyDateEnd;
            res["orderDateStart"] = OrderDateStart;
            res["orderDateEnd"] = OrderDateEnd;
            res["orderNumber"] = OrderNumber;
            res["orderStatus"] = OrderStatus;
            res["paymentDateStart"] = PaymentDateStart;
            res["paymentDateEnd"] = PaymentDateEnd;
            res["storeId"] = StoreId;
            res["sortBy"] = SortBy;
            res["sortDir"] = SortDir;

            return res;
        }
    }
}
