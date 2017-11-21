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
    public class OrdersByTagFilter : Filter
    {
        /// <summary>
        /// The order's status.
        /// Possible values: 
        /// awaiting_payment, awaiting_shipment, pending_fulfillment, shipped, on_hold, cancelled.
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }

        /// <summary>
        /// ID of the tag. Call Accounts/ListTags to obtain a list of tags for this account.
        /// </summary>
        public int? TagId { get; set; }

        protected override Dictionary<string, object> GetFilters()
        {
            var res = base.GetFilters();

            res["orderStatus"] = OrderStatus;
            res["tagId"] = TagId;

            return res;
        }
    }
}
