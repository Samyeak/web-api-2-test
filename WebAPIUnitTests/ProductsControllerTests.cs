using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Moq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPIUnitTests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        IProductsRepository repo;
        IProductsController controller;

        [SetUp]
        public void Setup()
        {
            //#region Mock Repository
            //Mock<IProductsRepository> mockRepo = new Mock<IProductsRepository>();
            //mockRepo.Setup(x => x.GetAll()).Returns(new List<Product> { new Product { Id = 1, Name = "Gomato Goup", Category = "Goop", Price = 1 } }); 
            //repo = mockRepo.Object;
            //#endregion

            repo = new ProductsRepository();
            controller = new ProductsController(repo);
        }

        [Test]
        public void TestMe()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAllProducts()
        {
            List<Product> result;
            try
            {
                result = controller.GetAllProducts().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            List<Product> expectedResult = new List<Product> { new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 } };

            result.Should().BeEquivalentTo(expectedResult, options => options.ComparingByValue<List<Product>>());
        }
    }
}
