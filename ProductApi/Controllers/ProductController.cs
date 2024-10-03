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
        public List<product> Get()
        {
            List<product> products = new List<product>();
            con.Connection.Open();
            string sql = "SELECT * FROM products";
            MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            do
            {
                var result = new product
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Price = reader.GetInt32(2),
                    CreatedTime = reader.GetDateTime(3),

                };
                products.Add(result);
            }

            while (reader.Read());
            con.Connection.Close();
            return products;
        }
        [HttpPost]
        public product Post(string Name, int Price)
        {
            con.Connection.Open();
            Guid Id = Guid.NewGuid();
            DateTime CreatedTime = DateTime.Now;
            string sql = $"INSERT INTO `products` (`Id`, `Name`, `Price`, `CreatedTime`) VALUES ('{Id}','{Name}',{Price},'{CreatedTime.ToString("yyyy-MM-dd HH:mm:ss")}')";
            MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
            cmd.ExecuteNonQuery();
            con.Connection.Close();
            var result = new product
            {
                Id = Id,
                Name = Name,
                Price = Price,
                CreatedTime = DateTime.Now
            };
            return result;
        }
        [HttpPut]
        public product Put(Guid Id, string NewName, int NewPrice)
        {
            con.Connection.Open();
            DateTime CreatedTime = DateTime.Now;
            string sql = $"UPDATE `products` SET `Name`='{NewName}', `Price` = {NewPrice} WHERE `Id` = '{Id}'";
            MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
            cmd.ExecuteNonQuery();
            con.Connection.Close();
            var result = new product
            {
                Id = Id,
                Name = NewName,
                Price = NewPrice,
                CreatedTime = DateTime.Now
            };
            return result;

        }


    }
}
