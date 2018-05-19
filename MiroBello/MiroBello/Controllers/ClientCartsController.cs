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
        public IEnumerable<ClientCart> GetClientCart()
        {
            return _context.ClientCart;
        }

        // GET: api/ClientCarts/5
        [HttpGet("{id}")]   
        public async Task<IActionResult> GetClientCart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clientCart = await _context.ClientCart.Include(c => c.Products).SingleOrDefaultAsync(m => m.ClientAccoundId == id);

            if (clientCart == null)
            {
                return NotFound();

            }
            var products = clientCart.Products.ToList();

            var productsOnCart = new List<ProductsOnCartViewModel>();
            foreach (var product in products)
            {
                var productOnCart = new ProductsOnCartViewModel
                {
                    Product = product.Product,
                    TotalPricePerProduct = product.Product.Price * product.Quantity
                };
                productsOnCart.Add(productOnCart);
            }

            return Ok(productsOnCart);
        }

        // PUT: api/ClientCarts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientCart([FromRoute] int id, [FromBody] ClientCart clientCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientCartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

            var clientCart = await _context.ClientCart.SingleOrDefaultAsync(m => m.Id == id);
            if (clientCart == null)
            {
                return NotFound();
            }

            _context.ClientCart.Remove(clientCart);
            await _context.SaveChangesAsync();

            return Ok(clientCart);
        }

        private bool ClientCartExists(int id)
        {
            return _context.ClientCart.Any(e => e.Id == id);
        }
    }
}