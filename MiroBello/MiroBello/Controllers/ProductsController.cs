using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiroBello.Models;

namespace MiroBello.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        // GET: api/Default
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return  new Product[] {
                    new Product{ Id=1, Name="Tort1", Price=13.45, Currency="USD", Category="Torturi pentru nunta", ImageURL="images/45.png", Details="Este un tort foarte bun pentru evenimentul tau!"},
                    new Product{ Id=2, Name="Tort2", Price=100.56, Currency="USD", Category="Torturi pentru Copii", ImageURL="images/39.png", Details="aaaaaaaaaaaaaaaaaaaaaa!"},
                    new Product{ Id=3, Name="Tort3", Price=32.00, Currency="USD", Category="Torturi pentru Majorat", ImageURL="images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
                    new Product{ Id=4, Name="Tort3", Price=32.00, Currency="USD", Category="Torturi pentru Majorat", ImageURL="images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
                    new Product{ Id=5, Name="Tort3", Price=32.00, Currency="USD", Category="Torturi pentru Majorat", ImageURL="images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
                    new Product{ Id=6, Name="Tort3", Price=32.00, Currency="USD", Category="Torturi pentru Majorat", ImageURL="images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"}

            };
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Default
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
