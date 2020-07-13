using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public interface IProductsRepository: IGenericRepository<Product>
    {
        
    }
    public class ProductsRepository: GenericRepository<Product>, IProductsRepository
    {

        public override IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (var connection = GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "Select * from public.\"Products\"";
                    command.CommandType = System.Data.CommandType.Text;
                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            products.Add(new Product
                            {
                                Id = rdr.GetInt32(rdr.GetOrdinal("Id")),
                                Name = rdr.GetString(rdr.GetOrdinal("Name")),
                                Category = rdr.GetString(rdr.GetOrdinal("Category")),
                                Price = rdr.GetDecimal(rdr.GetOrdinal("Price"))
                            });
                        }
                        return products;
                    }
                }
            }
        }
    }
}