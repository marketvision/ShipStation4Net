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

using Newtonsoft.Json.Linq;
using ShipStation4Net.Clients.Interfaces;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Filters;
using ShipStation4Net.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Shipments : ClientBase, IGetsPaginatedResponses<Shipment, ShipmentsFilter>, IGetsResourceUrlResponses<Shipment>
    {
        public Shipments(Configuration configuration) : base(configuration)
        {
            BaseUri = "shipments";
        }

        /// <summary>
        /// Obtains a list of shipments that match the specified criteria. Please note the following:
        /// Only valid shipments with labels generated in ShipStation will be returned in the response.Orders that have been marked as 
        /// shipped either through the UI or the API will not appear as they are considered external shipments.
        /// To include every shipment's associated shipmentItems in the response, be sure to set the includeShipmentItems parameter to 
        /// true.
        /// </summary>
        /// <param name="filter">The ShipmentsFilter</param>
        /// <returns>A list of shipments that match the specified criteria.</returns>
        public Task<IList<Shipment>> GetAllPagesAsync(ShipmentsFilter filter = null)
        {
            return GetAllPagesAsync(filter, "");

        }
        private async Task<IList<Shipment>> GetAllPagesAsync(ShipmentsFilter filter, string resourceUrl)
        {
            var items = new List<Shipment>();
            filter = filter ?? new ShipmentsFilter();

            var pageOne = await GetDataAsync<PaginatedResponse<Shipment>>(resourceUrl, filter).ConfigureAwait(false);
            items.AddRange(pageOne.Items);
            if (pageOne.Pages > 1)
            {
                items.AddRange(await GetPageRangeAsync(2, pageOne.Pages, filter.PageSize, filter, resourceUrl).ConfigureAwait(false));
            }

            return items;
        }
        public Task<IList<Shipment>> GetPageAsync(int page, int pageSize = 100, ShipmentsFilter filter = null)
        {
            return GetPageAsync(page, pageSize, filter, "");
        }
        private async Task<IList<Shipment>> GetPageAsync(int page, int pageSize, ShipmentsFilter filter, string resourceUrl)
        {
            if (page < 1) throw new ArgumentException(nameof(page), "Cannot be a negative or zero");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            filter = filter ?? new ShipmentsFilter();

            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<PaginatedResponse<Shipment>>(resourceUrl, filter).ConfigureAwait(false);
            return response.Items;
        }

        public Task<IList<Shipment>> GetPageRangeAsync(int start, int end, int pageSize = 100, ShipmentsFilter filter = null)
        {
            return GetPageRangeAsync(start, end, pageSize, filter, "");
        }
        private async Task<IList<Shipment>> GetPageRangeAsync(int start, int end, int pageSize, ShipmentsFilter filter, string resourceUrl)
        {
            if (start < 1) throw new ArgumentException(nameof(start), "Cannot be a negative or zero");
            if (start > end) throw new ArgumentException(nameof(end), "Invalid page range");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            var items = new List<Shipment>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, filter, resourceUrl).ConfigureAwait(false));
            }
            return items;
        }

        /// <summary>
        /// Creates a shipping label. The labelData field returned in the response is a base64 encoded PDF value. Simply decode and save 
        /// the output as a PDF file to retrieve a printable label. 
        /// </summary>
        /// <param name="request">A request that specifies the parameters of the shipment label.</param>
        /// <returns>The shipping label matching the requested parameters.</returns>
        public Task<ShipmentLabel> CreateShipmentLabelAsync(ShipmentLabelRequest request)
        {
            return PostDataAsync<ShipmentLabelRequest, ShipmentLabel>("createlabel", request);
        }

        /// <summary>
        /// Retrieves shipping rates for the specified shipping details. 
        /// </summary>
        /// <param name="request">A request that specifies the parameters of a shipment.</param>
        /// <returns>A list of rates matching the requested shipment.</returns>
        public Task<IList<Rate>> GetRatesAsync(RatesRequest request)
        {
            return PostDataAsync<RatesRequest, IList<Rate>>("getrates", request);
        }

        /// <summary>
        /// Voids the specified label by shipmentId. 
        /// </summary>
        /// <param name="shipmentId">ID of the shipment to void.</param>
        /// <returns>A response indicating whether or not the request was successful.</returns>
        public async Task<bool> VoidLabelAsync(int shipmentId)
        {
            var voidLabelRequest = new JObject();
            voidLabelRequest["shipmentId"] = shipmentId;

            var response = await PostDataAsync<JObject, ApprovedResponse>("voidlabel", voidLabelRequest).ConfigureAwait(false);

            return response.Success;
        }

        /// <summary>
        /// Takes the resourceURL of a SHIP_NOTIFY or ITEM_SHIP_NOTIFY and retrieves the list of Shipments
        /// </summary>
        /// <see cref="https://help.shipstation.com/hc/en-us/articles/360025856252-ShipStation-Webhooks"/>
        /// <param name="resourceUrl">The full url based via the webhook</param>
        /// <returns></returns>
        public Task<IList<Shipment>> GetResourceResponsesAsync(string resourceUrl)
        {
            return GetAllPagesAsync(null, resourceUrl);
        }
    }
}
