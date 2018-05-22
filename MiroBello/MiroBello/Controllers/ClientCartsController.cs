using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiroBello.Data;
using MiroBello.Models;
using MiroBello.ViewModels;

namespace MiroBello.Controllers
{
    [Produces("application/json")]
    [Route("api/ClientCarts")]
    public class ClientCartsController : Controller
    {
        private readonly DataContext _context;

        public ClientCartsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ClientCarts
        [HttpGet]
        public IEnumerable<ClientCart> GetShoopingCarts()
        {


            var shoopingCarts = _context.ClientCart.Include(cart => cart.Products).ToList();


            return shoopingCarts;
        }


        // GET: api/ClientCarts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoopingCartForClient([FromRoute]int id)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cart = _context.ClientCart.Include(c => c.Products).SingleOrDefault(m => m.ClientAccoundId == id);

            foreach (var product in cart.Products)
            {

                _context.Products.SingleOrDefault(prod => prod.ProductId == product.ProductId);
            }
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        public ClientCart Update(int id, ClientCart cartToUpdate)
        {
            ClientCart currentCart = _context.ClientCart.SingleOrDefault(cart => cart.Id == id);
            currentCart.Products = cartToUpdate.Products;
            
            return currentCart;
        }


        //// PUT: api/ClientCarts/5
        //[HttpPut("{id}")]
        //public ProductsAndShoopingCartViewModel PutProductCart(int id, [FromBody] ProductsAndShoopingCartViewModel productOnCart)
        //{
        //    var shoopingCartDb = _context.ProductsOnCart.Single(p => p.Id == id);
        //    shoopingCartDb.Quantity = productOnCart.Quantity;
        //    _context.SaveChanges();



        //    return productOnCart;
        //}

        // POST: api/ClientCarts
        [HttpPost]
        public async Task<IActionResult> PostClientCart([FromBody]Product productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addProductOnCart = new ProductsOnCart
            {
                ClientCartId = 1,
                ProductId = Convert.ToInt32(productId),
                Quantity = 1
            };

            _context.ProductsOnCart.Add(addProductOnCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCart", new { id = productId });
        }

        // DELETE: api/ClientCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientCart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productCart = await _context.ProductsOnCart.SingleOrDefaultAsync(m => m.Id == id);
            if (productCart == null)
            {
                return NotFound();
            }

            _context.ProductsOnCart.Remove(productCart);
            await _context.SaveChangesAsync();

            return Ok(productCart);
        }

        private bool ClientCartExists(int id)
        {
            return _context.ClientCart.Any(e => e.Id == id);
        }
    }
}