using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using WebAPI.DataAccess;
using WebAPI.Models;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public interface IProductsController
    {
        IEnumerable<Product> GetAllProducts();
        //IHttpActionResult GetProduct(int id);
    }

    public class ProductsController : ApiController, IProductsController
    {
        public IProductsRepository repo;
        public ProductsController(IProductsRepository _repo)
        {
            repo = _repo;
        }
        Product[] products = new Product[]
        {
            new Product{ Id = 1, Name = "Tomato Soup", Category="Groceries", Price = 1},
            new Product{ Id = 2, Name = "Yi-yo", Category="Toys", Price = 3.75M},
            new Product{ Id = 3, Name = "Hammer", Category="Hardware", Price = 17M},
        };

        public IEnumerable<Product> GetAllProducts()
        {
            //return products;
            return repo.GetAll();
        }

        public async Task<IHttpActionResult> GetProduct(int id)
        {
            var product = await repo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}