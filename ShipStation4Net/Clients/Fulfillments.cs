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
using ShipStation4Net.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Fulfillments : ClientBase, IGetsPaginatedResponses<Fulfillment, FulfillmentsFilter>, IGets<Fulfillment, int>, IGetsResourceUrlResponses<Fulfillment>
    {
        public Fulfillments(Configuration configuration) : base(configuration)
        {
            BaseUri = "fulfillments";
        }

        public Task<IList<Fulfillment>> GetAllPagesAsync(FulfillmentsFilter filter)
        {
            return GetAllPagesAsync(filter, "");
        }
        private async Task<IList<Fulfillment>> GetAllPagesAsync(FulfillmentsFilter filter, string resourceUrl)
        {
            var items = new List<Fulfillment>();
            filter = filter ?? new FulfillmentsFilter();

            var pageOne = await GetDataAsync<PaginatedResponse<Fulfillment>>(resourceUrl, filter).ConfigureAwait(false);
            items.AddRange(pageOne.Items);
            if (pageOne.Pages > 1)
            {
                items.AddRange(await GetPageRangeAsync(2, pageOne.Pages, filter.PageSize, filter, resourceUrl).ConfigureAwait(false));
            }

            return items;
        }

        public async Task<Fulfillment> GetAsync(int id)
        {
            var filter = new FulfillmentsFilter { FulfillmentId = id };

            var response = await GetDataAsync<PaginatedResponse<Fulfillment>>(filter).ConfigureAwait(false);

            return response.Items[0];
        }


        public  Task<IList<Fulfillment>> GetPageAsync(int page, int pageSize = 100, FulfillmentsFilter filter = null)
        {
            return GetPageAsync(page, pageSize, filter, "");
        }
        private async Task<IList<Fulfillment>> GetPageAsync(int page, int pageSize, FulfillmentsFilter filter, string resourceUrl)
        {
            if (page < 1) throw new ArgumentException(nameof(page), "Cannot be a negative or zero");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            filter = filter ?? new FulfillmentsFilter();

            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<PaginatedResponse<Fulfillment>>(resourceUrl, filter).ConfigureAwait(false);
            return response.Items;
        }

        public Task<IList<Fulfillment>> GetPageRangeAsync(int start, int end, int pageSize = 100, FulfillmentsFilter filter = null)
        {
            return GetPageRangeAsync(start, end, pageSize, filter, "");
        }
        public async Task<IList<Fulfillment>> GetPageRangeAsync(int start, int end, int pageSize, FulfillmentsFilter filter, string resourceUrl)
        {
            if (start < 1) throw new ArgumentException(nameof(start), "Cannot be a negative or zero");
            if (start > end) throw new ArgumentException(nameof(end), "Invalid page range");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            var items = new List<Fulfillment>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, filter, resourceUrl).ConfigureAwait(false));
            }
            return items;
        }

        public Task<IList<Fulfillment>> GetResourceResponsesAsync(string resourceUrl)
        {
            return GetAllPagesAsync(null, resourceUrl);
        }
    }
}
