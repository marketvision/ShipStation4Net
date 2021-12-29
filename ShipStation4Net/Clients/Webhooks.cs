﻿#region License
/*
 * Copyright 2021 MarketVision Consulting Group
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

using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Responses;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients
{
    public class Webhooks : ClientBase
    {
        public Webhooks(Configuration configuration) : base(configuration)
        {
            BaseUri = "webhooks";
        }

        /// <summary>
        /// Retrieves a list of registered webhooks for the account.
        /// </summary>
        public Task<WebhookResponse> GetItemsAsync()
        {
            return GetDataAsync<WebhookResponse>();
        }

        /// <summary>
        /// Subscribes to a specific type of webhook. If a store_id is passed in, the webhooks will only be triggered for that specific store_id.
        /// The event type that is passed in will determine what type of webhooks will be sent.
        ///
        /// https://www.shipstation.com/docs/api/webhooks/subscribe/
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<SubscribeToWebhookResponse> CreateAsync(SubscribeToWebhookRequest request)
        {
            return PostDataAsync<SubscribeToWebhookRequest, SubscribeToWebhookResponse>("subscribe", request);
        }

        /// <summary>
        /// Unsubscribes from a certain webhook.
        /// </summary>
        /// <param name="id">A unique ID generated by ShipStation and assigned to each webhook.</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(int id)
        {
            return DeleteDataAsync(id);
        }
    }
}