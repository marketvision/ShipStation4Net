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
using ShipStation4Net.Domain.Enumerations;
using ShipStation4Net.Filters;
using ShipStation4Net.Responses;
using ShipStation4Net.Responses.PaginatedResponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Orders : ClientBase, IGetsPaginatedResponses<Order>, ICreates<Order>, IGets<Order>
    {
        public Orders(Configuration configuration) : base(configuration)
        {
            BaseUri = "orders";
        }

        /// <summary>
        /// Obtains a list of orders that match the specified criteria. All of the available filters are optional. They do not need to be 
        /// included in the URL. The filter is created just for the particular Orders return type.
        /// </summary>
        /// <param name="filter">An OrdersFilter</param>
        /// <returns>A list of orders filtered by the supplied parameters.</returns>
        public async Task<IList<Order>> GetAllPagesAsync(IFilter filter = null)
        {
            var items = new List<Order>();
            filter = filter ?? new OrdersFilter();

            filter.Page = 1;
            filter.PageSize = 500;

            var pageOne = await GetDataAsync<OrdersPaginatedResponse>((OrdersFilter)filter);
            items.AddRange(pageOne.Items);
            items.AddRange(await GetPageRangeAsync(2, pageOne.TotalPages, 500, filter));

            return items;
        }

        public async Task<IList<Order>> GetPageRangeAsync(int start, int end, int pageSize = 100, IFilter filter = null)
        {
            if (pageSize < 1 || pageSize > 500)
            {
                throw new ArgumentOutOfRangeException("pageSize", "Should be in range 1..500");
            }

            var items = new List<Order>();

            for (int i = start; i <= end; i++)
            {
                items.AddRange(await GetPageAsync(i, pageSize, (OrdersFilter)filter));
            }
            return items;
        }

        public async Task<IList<Order>> GetPageAsync(int page, int pageSize = 100, IFilter filter = null)
        {
            if (pageSize < 1 || pageSize > 500)
            {
                throw new ArgumentOutOfRangeException("pageSize", "Should be in range 1..500");
            }

            filter = filter ?? new OrdersFilter();

            filter.Page = page;
            filter.PageSize = pageSize;

            var response = await GetDataAsync<OrdersPaginatedResponse>((OrdersFilter)filter);
            return response.Items;
        }

        /// <summary>
        /// If the orderKey is specified, the method becomes idempotent and the existing order with that key will be updated. Note: Only 
        /// orders in an open status in ShipStation (awaiting_payment,awaiting_shipment, and on_hold) can be updated through this method. 
        /// cancelled and shipped are locked from modification through the API. 
        /// </summary>
        /// <param name="newItem">A newly created order</param>
        /// <returns>The order with fields assigned to it by the API.</returns>
        public async Task<Order> CreateAsync(Order newItem)
        {
            return await PostDataAsync("createorder", newItem);
        }

        /// <summary>
        /// An alias of CreateAsync because updates are on the same endpoint as creates.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>The updated order.</returns>
        public async Task<Order> UpdateAsync(Order item)
        {
            return await CreateAsync(item);
        }

        /// <summary>
        /// Retrieves a single order from the database.
        /// </summary>
        /// <param name="id">The id of the order to retrieve.</param>
        /// <returns>The order specified by the id.</returns>
        public async Task<Order> GetAsync(int id)
        {
            return await GetDataAsync<Order>(id);
        }

        /// <summary>
        /// Removes order from ShipStation's UI. Note this is a "soft" delete action so the order will still exist in the database, but 
        /// will be set to inactive
        /// </summary>
        /// <param name="id">The id of the order to delete.</param>
        /// <returns>A boolean representing whether or not the delete was successful.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            return await DeleteDataAsync(id);
        }

        /// <summary>
        /// Creates a shipping label for a given order. The labelData field returned in the response is a base64 encoded PDF value. Simply 
        /// decode and save the output as a PDF file to retrieve a printable label. 
        /// </summary>
        /// <param name="userId">The user id to assign</param>
        /// <param name="orderIds">The list of order ids to assign to the user</param>
        /// <returns>A boolean representing whether or not the request was successful.</returns>
        public async Task<bool> AssignUserToOrderAsync(string userId, IList<int> orderIds)
        {
            var assignUserRequest = new JObject();
            assignUserRequest["orderIds"] = new JArray(orderIds);
            assignUserRequest["userId"] = userId;

            var response = await PostDataAsync<JObject, SuccessResponse>("assignuser", assignUserRequest);

            return response.Success;
        }

        /// <summary>
        /// Creates a shipping label for a given order. The labelData field returned in the response is a base64 encoded PDF value. Simply
        /// decode and save the output as a PDF file to retrieve a printable label. 
        /// </summary>
        /// <param name="order">The order to create a label from.</param>
        /// <returns>A Shipment with the label data.</returns>
        public async Task<Shipment> CreateLabelFromOrderAsync(Order order)
        {
            return await PostDataAsync<Order, Shipment>("createlabelfororder", order);
        }

        /// <summary>
        /// This method will change the status of the given order to On Hold until the date specified, when the status will automatically 
        /// change to Awaiting Shipment.
        /// </summary>
        /// <param name="orderId">The id of the order to put on hold.</param>
        /// <param name="holdUntilDate">Date when order is moved from on_hold status to awaiting_shipment.</param>
        /// <returns>A response that represents whether or not the request was completed successfully.</returns>
        public async Task<bool> HoldOrderUntilAsync(int orderId, DateTime holdUntilDate)
        {
            var holdOrderRequest = new JObject();
            holdOrderRequest["orderId"] = orderId;
            holdOrderRequest["holdUntilDate"] = holdUntilDate.ToString();

            var response = await PostDataAsync<JObject, SuccessResponse>("holduntil", holdOrderRequest);

            return response.Success;
        }

        /// <summary>
        /// Lists all orders that match the specified status and tag ID.
        /// </summary>
        /// <param name="tagId">ID of the tag.Call Accounts/ListTags to obtain a list of tags for this account.</param>
        /// <param name="orderStatus">
        /// The order's status.
        /// Possible values:
        /// awaiting_payment , awaiting_shipment , pending_fulfillment , shipped , on_hold , cancelled. </param>
        /// <param name="page">
        /// Page number
        /// Default: 1.
        /// </param>
        /// <param name="pageSize">
        /// Requested page size.Max value is 500.
        /// Default: 100. </param>
        /// <returns>All orders that match the specified status and tag ID.</returns>
        public async Task<IList<Order>> ListOrdersByTagAsync(int tagId, OrderStatus orderStatus, int page = 1, int pageSize = 100)
        {
            var filter = new OrdersByTagFilter
            {
                TagId = tagId,
                OrderStatus = orderStatus,
                Page = page,
                PageSize = pageSize
            };

            var response = await GetDataAsync<OrdersPaginatedResponse>("listbytag", filter);

            return response.Items;
        }

        /// <summary>
        /// Marks an order as shipped without creating a label in ShipStation. Has its own special request.
        /// </summary>
        /// <param name="request">A variant of the order object</param>
        /// <returns>A response that contains the order id and number.</returns>
        public async Task<MarkOrderAsShippedResponse> MarkOrderAsShippedAsync(MarkOrderAsShippedRequest request)
        {
            return await PostDataAsync<MarkOrderAsShippedRequest, MarkOrderAsShippedResponse>("markasshipped", request);
        }

        /// <summary>
        /// Adds a tag to the specified order.
        /// </summary>
        /// <param name="orderId">Identifies the order whose tag will be added.</param>
        /// <param name="tagId">Identifies the tag to add.</param>
        /// <returns>A response representing whether or not the request was successful.</returns>
        public async Task<bool> AddTagToOrderAsync(int orderId, int tagId)
        {
            var addTagRequest = new JObject();
            addTagRequest["orderId"] = orderId;
            addTagRequest["tagId"] = tagId;

            var response = await PostDataAsync<JObject, SuccessResponse>("addtag", addTagRequest);

            return response.Success;
        }

        /// <summary>
        /// Removes a tag from the specified order. 
        /// </summary>
        /// <param name="orderId">Identifies the order whose tag will be removed.</param>
        /// <param name="tagId"></param>
        /// <returns>A response that represents whether or not the request was successful.</returns>
        public async Task<bool> RemoveTagFromOrderAsync(int orderId, int tagId)
        {
            var removeTagRequest = new JObject();
            removeTagRequest["orderId"] = orderId;
            removeTagRequest["tagId"] = tagId;

            var response = await PostDataAsync<JObject, SuccessResponse>("removetag", removeTagRequest);

            return response.Success;
        }

        /// <summary>
        /// This method will change the status of the given order from On Hold to Awaiting Shipment. This endpoint is used when a 
        /// holdUntil Date is attached to an order.
        /// </summary>
        /// <param name="orderId">Identifies the order that will be restored to awaiting_shipment from on_hold.</param>
        /// <returns>A response that represents whether or not the request was successful.</returns>
        public async Task<bool> RestoreOrderFromOnHoldAsync(int orderId)
        {
            var restoreOrderRequest = new JObject();
            restoreOrderRequest["orderId"] = orderId;

            var response = await PostDataAsync<JObject, SuccessResponse>("restorefromhold", restoreOrderRequest);

            return response.Success;
        }

        /// <summary>
        /// Unassigns a user from an order. 
        /// </summary>
        /// <param name="orderIds">Identifies set of orders that will have the user unassigned. Please note that if ANY of the orders 
        /// within the array are not found, then no orders will have their users unassigned.</param>
        /// <returns>A response that represents whether or not the request was successful.</returns>
        public async Task<bool> UnassignUserFromOrderAsync(IList<int> orderIds)
        {
            var unassignUserRequest = new JObject();
            unassignUserRequest["orderIds"] = new JArray(orderIds);

            var response = await PostDataAsync<JObject, SuccessResponse>("unassignuser", unassignUserRequest);
            return response.Success;
        }
    }
}
