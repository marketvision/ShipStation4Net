using Newtonsoft.Json;
using ShipStation4Net.Domain.Entities;
using ShipStation4Net.Exceptions;
using ShipStation4Net.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ShipStation4Net.Tests
{
    public class TestProducts : TestBase
    {
        [Fact]
        public async void TestGetProduct()
        {
            var product = new Product();
            var testProduct = JsonConvert.DeserializeObject<Product>(File.ReadAllText("Results/product_test.json"));

            try
            {
                product = await Client.Products.GetAsync(testProduct.ProductId.Value);
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                product = await Client.Products.GetAsync(testProduct.ProductId.Value);
            }

            Assert.Equal(JsonConvert.SerializeObject(testProduct), JsonConvert.SerializeObject(product));
        }

        [Fact]
        public async void TestGetPageOneProducts()
        {
            var products = new List<Product>();

            try
            {
                products = await Client.Products.GetPageAsync(1) as List<Product>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                products = await Client.Products.GetPageAsync(1) as List<Product>;
            }

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithDateFilter()
        {
            var products = new List<Product>();
            var productsFilter = new ProductsFilter
            {
                StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(30))
            };

            try
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithShowInactiveFilter()
        {
            var products = new List<Product>();
            var productsFilter = new ProductsFilter
            {
                ShowInactive = true
            };

            try
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }

            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void TestGetProductsWithNameFilter()
        {
            var products = new List<Product>();
            var testProduct = JsonConvert.DeserializeObject<Product>(File.ReadAllText("Results/product_test.json"));
            var productsFilter = new ProductsFilter
            {
                Name = testProduct.Name
            };

            try
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }
            catch (ApiLimitReachedException ex)
            {
                await Task.Delay(TimeSpan.FromSeconds(ex.RemainingSecondsBeforeReset));
            }
            finally
            {
                products = await Client.Products.GetPageAsync(1, 100, productsFilter) as List<Product>;
            }

            Assert.True(products.Count > 0);
        }
    }
}
