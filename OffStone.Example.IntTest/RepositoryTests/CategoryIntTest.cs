using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OffStone.Example.Dal.Entities;
using OffStone.Example.DAL.Repositories;
using OffStone.Example.IntTest.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OffStone.Example.IntTest.RepositoryTests
{
    [TestFixture(Description = "Tests the generic functionality for Category but common to all repositories")]
    public class CategoryTests
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            if (_serviceProvider == null)
                _serviceProvider = Startup.ConfigureServices();
        }

        [Test(Description = "Tests CategoryRepository.GetById(int orderId) with existing orderId")]
        public void CategoryRepository_GetById_WithExisting_ReturnsCategory()
        {
            var repository = _serviceProvider.GetService<IGenericRepository<Category>>();
            var entity = repository.GetById(Categories.categories.First().CategoryId);

            Assert.AreEqual(Categories.categories.First().CategoryName, entity.CategoryName);
        }

        [Test(Description = "Tests Category GenericRepository Add, Exists then Remove")]
        public void GenricRepository_Add_Exists_Remove()
        {
            var newEntity = Categories.NewCategory_01;
            var repository = _serviceProvider.GetService<IGenericRepository<Category>>();

            repository.Add(newEntity);
            var saved = repository.Save();

            // Check the Entity was saved and allocated identity generated Id
            Assert.IsTrue(saved);
            var newEntityId = newEntity.CategoryId;
            Assert.Greater(newEntityId, 0, "New Id must be a positive integer");

            // Delete the Entity to cleanup
            repository.Remove(newEntity);
            saved = repository.Save();

            // Check the Entity was deleted
            Assert.IsTrue(saved);
            var exists = repository.Exists(c => c.CategoryId == newEntityId);
            Assert.IsFalse(exists);
        }

        [Test(Description = "Tests Category GenericRepository Find with existing criteria")]
        public void GenricRepository_Find_WithExisting_ReturnsMultiple()
        {
            var repository = _serviceProvider.GetService<IGenericRepository<Category>>();
            var searchResults = repository.Find(c => c.CategoryName.StartsWith("Con"));

            Console.WriteLine($"Count: {searchResults.Count()}");
            Assert.IsInstanceOf<IEnumerable<Category>>(searchResults);
            Assert.Greater(searchResults.Count(), 0,
                "There should be many matching results for this test");
        }
    }
}