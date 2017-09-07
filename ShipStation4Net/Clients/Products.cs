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
using ShipStation4Net.Responses.PaginatedResponses;

namespace ShipStation4Net.Clients
{
    public class Products : ClientBase, IGets<Product>, IUpdates<Product>, IGetsPaginatedResponses<Product>
    {
        public Products(Configuration configuration) : base(configuration)
        {
            BaseUri = "products";
        }

        public async Task<Product> GetAsync(int id)
        {
            return await GetDataAsync<Product>(id);
        }

        /// <summary>
        /// Obtains a list of products that match the specified criteria. All of the available filters are optional. They do not need to 
        /// be included in the URL. 
        /// </summary>
        /// <param name="filter">A Products filter.</param>
        /// <returns>A list of products that match the specified criteria.</returns>
        public async Task<IList<Product>> GetAllPagesAsync(IFilter filter)
        {
            var items = new List<Product>();
            filter = filter ?? new ProductsFilter { Page = 1, PageSize = 500 };
            var pageOne = await GetDataAsync<ProductsPaginatedResponse>((ProductsFilter)filter);
            items.AddRange(pageOne.Items);
            items.AddRange(await GetPageRangeAsync(2, pageOne.TotalPages, 500, (ProductsFilter)filter));

            return items;
        }

        public async Task<IList<Product>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            if (pageSize > 500)
            {
                pageSize = 500;
            }
            filter = filter ?? new ProductsFilter { Page = page, PageSize = pageSize };
            var response = await GetDataAsync<ProductsPaginatedResponse>((ProductsFilter)filter);
            return response.Items;
        }

        public async Task<IList<Product>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            var items = new List<Product>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, (ProductsFilter)filter));
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
        public async Task<Product> UpdateAsync(int id, Product item)
        {
            return await PutDataAsync(id.ToString(), item);
        }
    }
}
