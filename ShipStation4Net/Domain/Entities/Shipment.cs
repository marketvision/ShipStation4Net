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
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class ShipmentItem
    {
        [JsonProperty("orderItemId")]
        public int OrderItemId { get; set; }

        [JsonProperty("lineItemKey")]
        public string LineItemKey { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unitPrice")]
        public int UnitPrice { get; set; }

        [JsonProperty("warehouseLocation")]
        public string WarehouseLocation { get; set; }

        [JsonProperty("options")]
        public string Options { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("fulfillmentSku")]
        public string FulfillmentSku { get; set; }
    }

    public class Shipment
    {
        [JsonProperty("shipmentId")]
        public int ShipmentId { get; set; }

        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("shipDate")]
        public string ShipDate { get; set; }

        [JsonProperty("shipmentCost")]
        public double ShipmentCost { get; set; }

        [JsonProperty("insuranceCost")]
        public double InsuranceCost { get; set; }

        [JsonProperty("trackingNumber")]
        public string TrackingNumber { get; set; }

        [JsonProperty("isReturnLabel")]
        public bool IsReturnLabel { get; set; }

        [JsonProperty("batchNumber")]
        public string BatchNumber { get; set; }

        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonProperty("serviceCode")]
        public string ServiceCode { get; set; }

        [JsonProperty("packageCode")]
        public string PackageCode { get; set; }

        [JsonProperty("confirmation")]
        public string Confirmation { get; set; }

        [JsonProperty("warehouseId")]
        public int WarehouseId { get; set; }

        [JsonProperty("voided")]
        public bool Voided { get; set; }

        [JsonProperty("voidDate")]
        public DateTime? VoidDate { get; set; }

        [JsonProperty("marketplaceNotified")]
        public bool MarketplaceNotified { get; set; }

        [JsonProperty("notifyErrorMessage")]
        public string NotifyErrorMessage { get; set; }

        [JsonProperty("shipTo")]
        public Address ShipTo { get; set; }

        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        [JsonProperty("dimensions")]
        public Dimensions Dimensions { get; set; }

        [JsonProperty("insuranceOptions")]
        public InsuranceOptions InsuranceOptions { get; set; }

        [JsonProperty("advancedOptions")]
        public AdvancedOptions AdvancedOptions { get; set; }

        [JsonProperty("shipmentItems")]
        public IList<ShipmentItem> ShipmentItems { get; set; }

        [JsonProperty("labelData")]
        public string LabelData { get; set; }

        [JsonProperty("formData")]
        public string FormData { get; set; }
    }


}
