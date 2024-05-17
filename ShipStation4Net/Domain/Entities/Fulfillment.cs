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
using System;

namespace ShipStation4Net.Domain.Entities
{
	public class Fulfillment : IEntity
	{
		[JsonProperty("fulfillmentId")]
		public int? FulfillmentId { get; set; }

		[JsonProperty("orderId")]
		public long? OrderId { get; set; }

		[JsonProperty("orderNumber")]
		public string OrderNumber { get; set; }

		[JsonProperty("userId")]
		public string UserId { get; set; }

		[JsonProperty("customerEmail")]
		public string CustomerEmail { get; set; }

		[JsonProperty("trackingNumber")]
		public string TrackingNumber { get; set; }

		[JsonProperty("shipDate")]
		public DateTime? ShipDate { get; set; }

		[JsonProperty("voidDate")]
		public DateTime? VoidDate { get; set; }

		[JsonProperty("deliveryDate")]
		public DateTime? DeliveryDate { get; set; }

		[JsonProperty("carrierCode")]
		public string CarrierCode { get; set; }

		[JsonProperty("fulfillmentProviderCode")]
		public string FulfillmentProviderCode { get; set; }

		[JsonProperty("fulfillmentServiceCode")]
		public string FulfillmentServiceCode { get; set; }

		[JsonProperty("fulfillmentFee")]
		public double? FulfillmentFee { get; set; }

		[JsonProperty("voidRequested")]
		public bool? IsVoidRequested { get; set; }

		[JsonProperty("voided")]
		public bool? IsVoided { get; set; }

		[JsonProperty("marketplaceNotified")]
		public bool? IsMarketplaceNotified { get; set; }

		[JsonProperty("notifyErrorMessage")]
		public string NotifyErrorMessage { get; set; }

		[JsonProperty("shipTo")]
		public Address ShipTo { get; set; }
	}
}
