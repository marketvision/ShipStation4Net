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
using System.Collections.Generic;

namespace ShipStation4Net.Domain.Entities
{
    public class Product : IEntity
    {
        [JsonProperty("aliases")]
        public string Aliases { get; set; }

        /// <summary>
        /// The system generated identifier for the product. This is a read-only field.
        /// </summary>
        [JsonProperty("productId")]
        public int? ProductId { get; set; }

        /// <summary>
        /// Stock keeping Unit. A user-defined value for a product to help identify the product. It is suggested that each product should 
        /// contain a unique SKU.
        /// </summary>
        [JsonProperty("sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Name or description of the product.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        [JsonProperty("price")]
        public double? Price { get; set; }

        /// <summary>
        /// The seller's cost for this product.
        /// </summary>
        [JsonProperty("defaultCost")]
        public double? DefaultCost { get; set; }

        /// <summary>
        /// The length of the product. Unit of measurement is UI dependent. No conversions will be made from one UOM to another. See our 
        /// knowledge base here for more details.
        /// </summary>
        [JsonProperty("length")]
        public int? Length { get; set; }

        /// <summary>
        /// The width of the product. Unit of measurement is UI dependent. No conversions will be made from one UOM to another. See our 
        /// knowledge base here for more details.
        /// </summary>
        [JsonProperty("width")]
        public int? Width { get; set; }

        /// <summary>
        /// The height of the product. Unit of measurement is UI dependent. No conversions will be made from one UOM to another. See our 
        /// knowledge base here for more details.
        /// </summary>
        [JsonProperty("height")]
        public int? Height { get; set; }

        /// <summary>
        /// The weight of a single item in ounces.
        /// </summary>
        [JsonProperty("weightOz")]
        public int? WeightOz { get; set; }

        /// <summary>
        /// Seller's private notes for the product.
        /// </summary>
        [JsonProperty("internalNotes")]
        public string InternalNotes { get; set; }

        /// <summary>
        /// Stock keeping Unit for the fulfillment of that product by a 3rd party.
        /// </summary>
        [JsonProperty("fulfillmentSku")]
        public string FulfillmentSku { get; set; }

        /// <summary>
        /// Specifies whether or not the product is an active record.
        /// </summary>
        [JsonProperty("active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// The Product Category used to organize and report on similar products. See our knowledge base here for more information on 
        /// Product Categories.
        /// </summary>
        [JsonProperty("productCategory")]
        public ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Specifies the product type. See our knowledge base here for more information on Product Types.
        /// </summary>
        [JsonProperty("productType")]
        public object ProductType { get; set; }

        /// <summary>
        /// The warehouse location associated with the product record.
        /// </summary>
        [JsonProperty("warehouseLocation")]
        public string WarehouseLocation { get; set; }

        /// <summary>
        /// The default domestic shipping carrier for this product.
        /// </summary>
        [JsonProperty("defaultCarrierCode")]
        public string DefaultCarrierCode { get; set; }

        /// <summary>
        /// The default domestic shipping service for this product.
        /// </summary>
        [JsonProperty("defaultServiceCode")]
        public string DefaultServiceCode { get; set; }

        /// <summary>
        /// The default domestic packageType for this product.
        /// </summary>
        [JsonProperty("defaultPackageCode")]
        public string DefaultPackageCode { get; set; }

        /// <summary>
        /// The default international shipping carrier for this product.
        /// </summary>
        [JsonProperty("defaultIntlCarrierCode")]
        public string DefaultIntlCarrierCode { get; set; }

        /// <summary>
        /// The default international shipping service for this product.
        /// </summary>
        [JsonProperty("defaultIntlServiceCode")]
        public string DefaultInternationalServiceCode { get; set; }

        /// <summary>
        /// The default international packageType for this product.
        /// </summary>
        [JsonProperty("defaultIntlPackageCode")]
        public string DefaultInternationalPackageCode { get; set; }

        /// <summary>
        /// The default domestic Confirmation type for this Product.
        /// </summary>
        [JsonProperty("defaultConfirmation")]
        public string DefaultConfirmation { get; set; }

        /// <summary>
        /// The default international Confirmation type for this Product.
        /// </summary>
        [JsonProperty("defaultIntlConfirmation")]
        public string DefaultInternationalConfirmation { get; set; }

        /// <summary>
        /// The default customs Description for the product.
        /// </summary>
        [JsonProperty("customsDescription")]
        public string CustomsDescription { get; set; }

        /// <summary>
        /// The default customs Declared Value for the product.
        /// </summary>
        [JsonProperty("customsValue")]
        public double? CustomsValue { get; set; }

        /// <summary>
        /// The default Harmonized Code for the Product.
        /// </summary>
        [JsonProperty("customsTariffNo")]
        public string CustomsTariffNumber { get; set; }

        /// <summary>
        /// The default 2 digit ISO Origin Country for the Product.
        /// </summary>
        [JsonProperty("customsCountryCode")]
        public string CustomsCountryCode { get; set; }

        /// <summary>
        /// If true, this product will not be included on international customs forms.
        /// </summary>
        [JsonProperty("noCustoms")]
        public bool? NoCustoms { get; set; }

        /// <summary>
        /// The Product Tag used to organize and visually identify products. See our knowledge base here for more information on Product 
        /// Defaults and tags.
        /// </summary>
        [JsonProperty("tags")]
        public IList<Tag> Tags { get; set; }
    }
}
