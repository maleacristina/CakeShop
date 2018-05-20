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
        public IEnumerable<ProductsOnCartViewModel> GetProducts()
        {


            var productsOnCart = _context.ProductsOnCart.Include(c => c.Product).Where(m => m.ClientCartId == 1).ToList();




            var cart = new List<ProductsOnCartViewModel>();
            foreach (var productCart in productsOnCart)
            {
                var productOnCart = new ProductsOnCartViewModel
                {
                    Product = productCart.Product,
                    Quantity = productCart.Quantity
                };
                cart.Add(productOnCart);
            }

            return cart;
        }

        // GET: api/ClientCarts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientCart(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productsOnCart = _context.ProductsOnCart.Include(c => c.Product).Where(m => m.ClientCartId == id).ToList();

            if (productsOnCart == null)
            {
                return NotFound(id);

            }


            var cart = new List<ProductsOnCartViewModel>();
            foreach (var productCart in productsOnCart)
            {
                var productOnCart = new ProductsOnCartViewModel
                {
                    ProductOnCartId = productCart.Id,
                    Product = productCart.Product,
                    Quantity = productCart.Quantity
                };
                cart.Add(productOnCart);
            }

            return Ok(cart);
        }

        // PUT: api/ClientCarts/5
        [HttpPut("{id}")]
        public ProductsOnCartViewModel PutProductCart(int id, [FromBody] ProductsOnCartViewModel productOnCart)
        {



            var shoopingCartDb = _context.ProductsOnCart.Single(p => p.Id == id);
            shoopingCartDb.Quantity = productOnCart.Quantity;
            _context.SaveChanges();



            return productOnCart;
        }

        // POST: api/ClientCarts
        [HttpPost]
        public async Task<IActionResult> PostClientCart([FromBody] ClientCart clientCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ClientCart.Add(clientCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientCart", new { id = clientCart.Id }, clientCart);
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