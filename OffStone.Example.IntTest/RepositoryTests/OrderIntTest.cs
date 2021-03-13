using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OffStone.Example.Dal.Entities;
using OffStone.Example.Dal.Repositories;
using OffStone.Example.IntTest.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OffStone.Example.IntTest.RepositoryTests
{
    [TestFixture(Description = "Tests the Order specific and generic repository functionality")]
    public class OrderTests
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            if (_serviceProvider == null)
                _serviceProvider = Startup.ConfigureServices();
        }

        [Test(Description = "Tests OrderRepository.GetById(int orderId) with existing orderId")]
        public void OrderRepository_GetById_WithExisting_ReturnsOrder()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var order = orderRepository.GetById(Orders.ExistingOrder_01.OrderId);

            Assert.AreEqual(Orders.ExistingOrder_01.CustomerId, order.CustomerId);
        }

        [Test(Description = "Tests OrderRepository Add, Exists then Remove")]
        public void OrderRepository_Add_Exists_Remove()
        {
            var newOrder = Orders.NewOrder_01;
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();

            orderRepository.Add(newOrder);
            var orderSaved = orderRepository.Save();

            // Check the Order was saved and allocated identity OrderId
            Assert.IsTrue(orderSaved);
            var newOrderId = newOrder.OrderId;
            Assert.Greater(newOrderId, 0, "New OrderId must be a positive integer");

            // Delete the Order to cleanup
            orderRepository.Remove(newOrder);
            orderSaved = orderRepository.Save();

            // Check the Order was deleted
            Assert.IsTrue(orderSaved);
            var orderExists = orderRepository.Exists(o => o.OrderId == newOrderId);
            Assert.IsFalse(orderExists);
        }


        [Test(Description = "Tests OrderRepository.SearchWithEf(customerId)")]
        public void OrderRepository_SearchWithEf_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithEf(Orders.SearchOrder_01.CustomerId, null, null);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.IsInstanceOf<IEnumerable<Order>>(searchResults);
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");
        }

        [Test(Description = "Tests OrderRepository.SearchWithEf(customerId, orderDate, companyName)")]
        public void OrderRepository_SearchWithEf_AllParams_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithEf(Orders.SearchOrder_01.CustomerId, 
                Orders.SearchOrder_01.OrderDate, 
                Orders.SearchOrder_01.Customer.ContactName);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");

            var first = searchResults.First();
            Assert.AreEqual(Orders.SearchOrder_01.CustomerId, first.CustomerId);
            Assert.AreEqual(Orders.SearchOrder_01.OrderDate, first.OrderDate);
            Assert.AreEqual(Orders.SearchOrder_01.Customer.CompanyName, first.Customer.CompanyName);
        }

        [Test(Description = "Tests OrderRepository.SearchWithDapperSplit(customerId)")]
        public void OrderRepository_SearchWithDapperSplit_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithDapperSplit(Orders.SearchOrder_01.CustomerId, null, null);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.IsInstanceOf<IEnumerable<Order>>(searchResults);
            Assert.Greater(searchResults.Count(), 0, 
                "There should be many matching results for this test");
        }

        [Test(Description = "Tests OrderRepository.SearchWithDapperSplit(customerId, orderDate, companyName)")]
        public void OrderRepository_SearchWithDapperSplit_AllParams_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithDapperSplit(Orders.SearchOrder_01.CustomerId,
                Orders.SearchOrder_01.OrderDate,
                Orders.SearchOrder_01.Customer.CompanyName);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");

            var first = searchResults.First();
            Assert.AreEqual(Orders.SearchOrder_01.CustomerId, first.CustomerId);
            Assert.AreEqual(Orders.SearchOrder_01.OrderDate, first.OrderDate);
            Assert.AreEqual(Orders.SearchOrder_01.Customer.CompanyName, first.Customer.CompanyName);
        }

        [Test(Description = "Tests OrderRepository.SearchWithDapperMulti(customerId)")]
        public void OrderRepository_SearchWithDapperMulti_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithDapperMulti(Orders.SearchOrder_01.CustomerId, null, null);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.IsInstanceOf<IEnumerable<Order>>(searchResults);
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");
        }

        [Test(Description = "Tests OrderRepository.SearchWithDapperMulti(customerId, orderDate, companyName)")]
        public void OrderRepository_SearchWithDapperMulti_AllParams_ReturnsMultiple()
        {
            var orderRepository = _serviceProvider.GetService<IOrderRepository>();
            var searchResults = orderRepository.SearchWithDapperMulti(Orders.SearchOrder_01.CustomerId,
                Orders.SearchOrder_01.OrderDate,
                Orders.SearchOrder_01.Customer.ContactName);

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");

            var first = searchResults.First();
            Assert.AreEqual(Orders.SearchOrder_01.CustomerId, first.CustomerId);
            Assert.AreEqual(Orders.SearchOrder_01.OrderDate, first.OrderDate);
            Assert.AreEqual(Orders.SearchOrder_01.Customer.CompanyName, first.Customer.CompanyName);
        }
    }
}