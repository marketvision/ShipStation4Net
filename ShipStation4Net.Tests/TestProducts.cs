using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestProducts : TestBase
    {
        int testProductId = 12162243;

        [Fact]
        public async void TestGetProduct()
        {
            var product = await Client.Products.GetAsync(testProductId);

            Assert.IsType<Product>(product);
            Assert.Equal(testProductId, product.ProductId);
        }

        [Fact]
        public async void TestGetPageOneProducts()
        {
            var products = await Client.Products.GetPageAsync(1) as List<Product>;

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithDateFilter()
        {
            var productsFilter = new ProductsFilter
            {
                StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(30))
            };

            var products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithShowInactiveFilter()
        {
            var productsFilter = new ProductsFilter
            {
                ShowInactive = true
            };

            var products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithNameFilter()
        {
            var productsFilter = new ProductsFilter
            {
                Name = "Sweepstakes Giveaway"
            };

            var products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;

            Assert.True(products.Count > 0);
        }
    }
}
