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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShipStation4Net.Filters;
using ShipStation4Net.Responses;
using ShipStation4Net.Responses.PaginatedResponses;

namespace ShipStation4Net.Clients
{
    public class Customers : ClientBase, IGets<Customer>, IGetsPaginatedResponses<Customer>
    {
        public Customers(Configuration configuration) : base(configuration)
        {
            BaseUri = "customers";
        }
        /// <summary>
        /// Gets the customer specified by a numerical id.
        /// </summary>
        /// <param name="id">The system generated identifier for the Customer. Example: 12345678.</param>
        /// <returns>The specified customer.</returns>
        public async Task<Customer> GetAsync(int id)
        {
            return await GetDataAsync<Customer>(id.ToString());
        }

        /// <summary>
        /// Obtains a list of customers that match the specified criteria.
        /// </summary>
        /// <param name="filter">A customer filter</param>
        /// <returns>A list of all customers.</returns>
        public async Task<IList<Customer>> GetAllPagesAsync(IFilter filter)
        {
            var items = new List<Customer>();
            filter = filter ?? new CustomersFilter { Page = 1, PageSize = 100 };
            
            var pageOne = await GetDataAsync<CustomersPaginatedResponse>((CustomersFilter)filter);
            items.AddRange(pageOne.Items);
            items.AddRange(await GetPageRangeAsync(2, pageOne.TotalPages, 500, filter));

            return items;
        }

        public async Task<IList<Customer>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var items = new List<Customer>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, (CustomersFilter)filter));
            }
            return items;
        }

        public async Task<IList<Customer>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            filter = (filter == null) ? new CustomersFilter() : filter;

            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<CustomersPaginatedResponse>(filter);
            return response.Items;
        }
    }
}
