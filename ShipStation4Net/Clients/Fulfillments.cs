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

using ShipStation4Net.Clients.Interfaces;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Filters;
using ShipStation4Net.Responses.PaginatedResponses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Fulfillments : ClientBase, IGetsPaginatedResponses<Fulfillment>, IGets<Fulfillment>
    {
        public Fulfillments(Configuration configuration) : base(configuration)
        {
            BaseUri = "fulfillments";
        }

        public async Task<IList<Fulfillment>> GetAllPagesAsync(IFilter filter)
        {
            var items = new List<Fulfillment>();
            filter = filter ?? new FulfillmentsFilter();
            filter.Page = 1;
            filter.PageSize = 100;

            var pageOne = await GetDataAsync<FulfillmentsPaginatedResponse>((FulfillmentsFilter)filter);
            items.AddRange(pageOne.Items);
            items.AddRange(await GetPageRangeAsync(2, pageOne.TotalPages, 500, (FulfillmentsFilter)filter));

            return items;
        }

        public async Task<Fulfillment> GetAsync(int id)
        {
            var filter = new FulfillmentsFilter { FulfillmentId = id };

            var response = await GetDataAsync<FulfillmentsPaginatedResponse>(filter);

            return response.Items[0];
        }

        public async Task<IList<Fulfillment>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            filter = filter ?? new FulfillmentsFilter();
            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<FulfillmentsPaginatedResponse>((FulfillmentsFilter)filter);
            return response.Items;
        }

        public async Task<IList<Fulfillment>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var items = new List<Fulfillment>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, (FulfillmentsFilter)filter));
            }
            return items;
        }
    }
}
