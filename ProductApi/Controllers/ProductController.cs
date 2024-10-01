using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using MySql.Data.MySqlClient;
namespace ProductApi.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Connect con = new();
        [HttpGet]
        public List<product> Get() {
            List<product> products = new List<product>();
            con.Connection.Open();
            string sql = "SELECT * FROM products";
            MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            do {
                var result = new product
                {
                    Id = reader.GetGuid(0),
                    Name=reader.GetString(1),
                    Price=reader.GetInt32(2),
                    CreatedTime=reader.GetDateTime(3),

                };
                products.Add(result);
                    }
            
            while (reader.Read());
            con.Connection.Close();
            return products;
                }
    }
}
