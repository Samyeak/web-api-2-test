using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.DataAccess;

namespace NUnitTestProject1
{
    [TestFixture]
    public class ProductsControllerTests
    {
        IProductsController controller;
        IProductsRepository repo;

        [SetUp]
        public void Setup()
        {
            repo = new ProductsRepository();
            controller = new ProductsController(repo);
        }

        [Test]
        public void GetAllProducts()
        {
            var result = controller.GetAllProducts();
            Assert.Equals(result, result);
        }
    }
}
