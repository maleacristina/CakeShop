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
        public ClientCart GetShoopingCartForClient([FromRoute]int id)
        {



            var cart = _context.ClientCart.Include(c => c.Products).SingleOrDefault(m => m.ClientAccoundId == id);

            foreach (var product in cart.Products)
            {

                _context.Products.SingleOrDefault(prod => prod.ProductId == product.ProductId);
            }


            return cart;
        }

        public ClientCart Update(int id, ClientCart cartToUpdate)
        {
            ClientCart currentCart = _context.ClientCart.SingleOrDefault(cart => cart.Id == id);
            currentCart.Products = cartToUpdate.Products;

            return currentCart;
        }


        // PUT: api/ClientCarts/5
        [HttpPut("{id}")]
        public void PutProductInCart(int id, int clientId, [FromBody] ProductsAndShoopingCartViewModel productOnCart)
        {
            var clientCart = _context.ClientCart.Include(c => c.Products).SingleOrDefault();

            if (clientCart != null)
            {
                var productExist = clientCart.Products.SingleOrDefault(c => c.ProductId == id);

                if (productExist != null)
                {
                    productExist.Quantity++;
                    var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
                    productExist.TotalPricePerProduct = product.Price * productExist.Quantity;

                }
                double? total = 0.0;
                foreach (var product in clientCart.Products)
                {
                    total = total + product.TotalPricePerProduct;
                }
                clientCart.TotalPriceOfCartForUser = total;
                _context.SaveChanges();
            }
            else
            {
                var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
                var cartWithNoProductsInIt = new ProductsOnCart
                {
                    ClientCartId = clientId,
                    ProductId = product.ProductId,
                    Quantity = 1.0,
                    TotalPricePerProduct = product.Price * 1.0

                };

                var newCartOfClient = new ClientCart
                {
                    ClientAccoundId = clientId,
                    TotalPriceOfCartForUser = cartWithNoProductsInIt.TotalPricePerProduct

                };
                _context.SaveChanges();
                //trebuie verificat
            }
        }











        // POST: api/ClientCarts
        [HttpPost("{id}")]
        public async Task<IActionResult> PostClientCart(int id,  [FromBody]ProductsOnCart product)
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
        public void DeleteClientCart(int id,int productId)
        {
            

            var clientCart =  _context.ClientCart.Include(c => c.Products).SingleOrDefault(c => c.Id == id);
            
            var product = _context.ProductsOnCart.SingleOrDefault(p => p.ProductId == productId && p.ClientCartId == id);
            clientCart.Products.Remove(product);

            double? total=0.0;
            foreach(var prod in clientCart.Products)
            {
                total += prod.TotalPricePerProduct;
            }
            clientCart.TotalPriceOfCartForUser = total;

            _context.SaveChangesAsync();

           
        }

        private bool ClientCartExists(int id)
        {
            return _context.ClientCart.Any(e => e.Id == id);
        }
    }
}