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
using ShipStation4Net.Responses.PaginatedResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Shipments : ClientBase, IGetsPaginatedResponses<Shipment>
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
        public async Task<IList<Shipment>> GetAllPagesAsync(IFilter filter)
        {
            var items = new List<Shipment>();
            filter = filter ?? new ShipmentsFilter { Page = 1, PageSize = 500 };
            var pageOne = await GetDataAsync<ShipmentsPaginatedResponse>((ShipmentsFilter)filter);
            items.AddRange(pageOne.Items);
            items.AddRange(await GetPageRangeAsync(2, pageOne.TotalPages, 500, (ShipmentsFilter)filter));

            return items;
        }

        public async Task<IList<Shipment>> GetPageAsync(int page, int pageSize = 50, IFilter filter = null)
        {
            filter = filter ?? new ShipmentsFilter { Page = page, PageSize = pageSize };
            var response = await GetDataAsync<ShipmentsPaginatedResponse>((ShipmentsFilter)filter);
            return response.Items;
        }

        public async Task<IList<Shipment>> GetPageRangeAsync(int start, int end, int pageSize = 50, IFilter filter = null)
        {
            var items = new List<Shipment>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, (ShipmentsFilter)filter));
            }
            return items;
        }

        /// <summary>
        /// Creates a shipping label. The labelData field returned in the response is a base64 encoded PDF value. Simply decode and save 
        /// the output as a PDF file to retrieve a printable label. 
        /// </summary>
        /// <param name="request">A request that specifies the parameters of the shipment label.</param>
        /// <returns>The shipping label matching the requested parameters.</returns>
        public async Task<ShipmentLabel> CreateShipmentLabelAsync(ShipmentLabelRequest request)
        {
            return await PostDataAsync<ShipmentLabelRequest, ShipmentLabel>("createlabel", request);
        }

        /// <summary>
        /// Retrieves shipping rates for the specified shipping details. 
        /// </summary>
        /// <param name="request">A request that specifies the parameters of a shipment.</param>
        /// <returns>A list of rates matching the requested shipment.</returns>
        public async Task<IList<Rate>> GetRatesAsync(RatesRequest request)
        {
            return await PostDataAsync<RatesRequest, IList<Rate>>("getrates", request);
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

            var response = await PostDataAsync<JObject, SuccessResponse>("voidlabel", voidLabelRequest);

            return response.Success;
        }
    }
}
