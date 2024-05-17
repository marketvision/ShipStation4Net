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

namespace ShipStation4Net.Clients
{
    public class Products : ClientBase, IGets<Product, int>, IUpdates<Product>, IGetsPaginatedResponses<Product, ProductsFilter>
    {
        public Products(Configuration configuration) : base(configuration)
        {
            BaseUri = "products";
        }

        public Task<Product> GetAsync(int id)
        {
            return GetDataAsync<Product>(id);
        }

        /// <summary>
        /// Obtains a list of products that match the specified criteria. All of the available filters are optional. They do not need to 
        /// be included in the URL. 
        /// </summary>
        /// <param name="filter">A Products filter.</param>
        /// <returns>A list of products that match the specified criteria.</returns>
        public async Task<IList<Product>> GetAllPagesAsync(ProductsFilter filter)
        {
            var items = new List<Product>();
            filter = filter ?? new ProductsFilter();

            var pageOne = await GetDataAsync<PaginatedResponse<Product>>(filter).ConfigureAwait(false);
            items.AddRange(pageOne.Items);
			if (pageOne.Pages > 1)
			{
				items.AddRange(await GetPageRangeAsync(2, pageOne.Pages, filter.PageSize, filter).ConfigureAwait(false));
			}

            return items;
        }

        public async Task<IList<Product>> GetPageAsync(int page, int pageSize = 100, ProductsFilter filter = null)
        {
            if (page < 1) throw new ArgumentException(nameof(page), "Cannot be a negative or zero");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            filter = filter ?? new ProductsFilter();

            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<PaginatedResponse<Product>>(filter).ConfigureAwait(false);
            return response.Items;
        }

        public async Task<IList<Product>> GetPageRangeAsync(int start, int end, int pageSize = 100, ProductsFilter filter = null)
        {
            if (start < 1) throw new ArgumentException(nameof(start), "Cannot be a negative or zero");
            if (start > end) throw new ArgumentException(nameof(end), "Invalid page range");
            if (pageSize < 1 || pageSize > 500) throw new ArgumentOutOfRangeException(nameof(pageSize), "Should be in range 1..500");

            var items = new List<Product>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, filter).ConfigureAwait(false));
            }
            return items;
        }

        /// <summary>
        /// Updates an existing product.This call does not currently support partial updates.The entire resource must be provided in the 
        /// body of the request.
        /// </summary>
        /// <param name="id">
        /// The system generated identifier for the Product.
        /// Example: 12345678. 
        /// </param>
        /// <param name="item"></param>
        /// <returns>The updated product.</returns>
        public Task<Product> UpdateAsync(int id, Product item)
        {
            return PutDataAsync(id.ToString(), item);
        }
    }
}
