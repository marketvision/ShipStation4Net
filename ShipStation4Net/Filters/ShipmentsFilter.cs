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
using System.Text;

namespace ShipStation4Net.Filters
{
    public class ShipmentsFilter : Filter
    {
        /// <summary>
        /// Returns shipments shipped to the specified recipient name.
        /// </summary>
        public string RecipientsName { get; set; }

        /// <summary>
        /// Returns shipments shipped to the specified country code.
        /// </summary>
        public string RecipientsCountryCode { get; set; }

        /// <summary>
        /// Returns shipments whose orders have the specified order number.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Returns shipments whose orders have the specified order ID.
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Returns shipments shipped with the specified carrier.
        /// </summary>
        public string CarrierCode { get; set; }

        /// <summary>
        /// Returns shipments shipped with the specified shipping service.
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Returns shipments with the specified tracking number.
        /// </summary>
        public string TrackingNumber { get; set; }

        /// <summary>
        /// Sort the responses by a set value.The response will be sorted based off the ascending dates(oldest to most current.) If left empty, the response will be sorted by ascending createDate.
        /// Example: ShipDate.
        /// </summary>
        public ShipmentsSortBy? SortBy { get; set; }

        /// <summary>
        /// Returns shipments created on or after the specified createDate
        /// Example: 2015-01-01 00:00:00. 
        /// </summary>
        public DateTime CreateDateStart { get; set; }

        /// <summary>
        /// Returns shipments created on or before the specified createDate
        /// Example: 2015-01-08 00:00:00. 
        /// </summary>
        public DateTime CreateDateEnd { get; set; }

        /// <summary>
        /// Returns shipments voided on or after the specified date
        /// Example: 2015-01-01 00:00:00. 
        /// </summary>
        public DateTime VoidDateStart { get; set; }

        /// <summary>
        /// Returns shipments voided on or before the specified date
        /// Example: 2015-01-08 00:00:00. 
        /// </summary>
        public DateTime VoidDateEnd { get; set; }

        /// <summary>
        /// Returns shipments with the shipDate on or after the specified date
        /// Example: 2015-01-01. 
        /// </summary>
        public DateTime ShipDateStart { get; set; }

        /// <summary>
        /// Returns shipments with the shipDate on or before the specified date
        /// Example: 2015-01-08. 
        /// </summary>
        public DateTime ShipDateEnd { get; set; }

        /// <summary>
        /// Specifies whether to include shipment items with results Default value: false.
        /// Example: false. 
        /// </summary>
        public bool? IncludeShipmentItems { get; set; }
        
        public override HttpRequestMessage AddFilter(HttpRequestMessage request)
        {
            var filters = new Dictionary<string, string>();

            if (RecipientsName != null)
            {
                filters.Add("recipientsName", RecipientsName);
            }
            if (RecipientsCountryCode != null)
            {
                filters.Add("recipientsCountryCode", RecipientsCountryCode);
            }
            if (OrderNumber != null)
            {
                filters.Add("orderNumber", OrderNumber);
            }
            if (OrderId != null)
            {
                filters.Add("orderId", OrderId.Value.ToString());
            }
            if (CarrierCode != null)
            {
                filters.Add("carrierCode", CarrierCode);
            }
            if (ServiceCode != null)
            {
                filters.Add("serviceCode", ServiceCode);
            }
            if (TrackingNumber != null)
            {
                filters.Add("trackingNumber", TrackingNumber);
            }
            if (CreateDateStart != null)
            {
                filters.Add("createDateStart", CreateDateStart.ToString());
            }
            if (CreateDateEnd != null)
            {
                filters.Add("createDateEnd", CreateDateEnd.ToString());
            }
            if (VoidDateStart != null)
            {
                filters.Add("voidDateStart", VoidDateStart.ToString());
            }
            if (VoidDateEnd != null)
            {
                filters.Add("voidDateEnd", VoidDateEnd.ToString());
            }
            if (IncludeShipmentItems != null)
            {
                filters.Add("includeShipmentItems", IncludeShipmentItems.Value.ToString());
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
