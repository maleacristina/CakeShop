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
            if(cart == null)
            {
                return null;
            }
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
                else
                {
                    var product = _context.Products.SingleOrDefault(p => p.ProductId == id);
                    productExist = new ProductsOnCart
                    {
                        Product = product,
                        ClientCartId = clientCart.Id,
                        Quantity = 1.0,
                        TotalPricePerProduct = product.Price * 1,
                    };
                    
                    clientCart.Products.Add(productExist);
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

                var newCartOfClient = new ClientCart
                {
                    ClientAccoundId = clientId,
                    TotalPriceOfCartForUser = product.Price * 1.0
                };

                var cartWithNoProductsInIt = new ProductsOnCart
                {
                    ClientCartId = newCartOfClient.Id,
                    ClientCart = newCartOfClient,
                    Product = product,
                    ProductId = id,
                    Quantity = 1.0,
                    TotalPricePerProduct = product.Price * 1.0

                };

                
                _context.Add(newCartOfClient);
                
                _context.Add(cartWithNoProductsInIt);
                _context.SaveChanges();


                //trebuie verificat
            }
        }











        // POST: api/ClientCarts
        [HttpPost("{id}")]
        public ProductsOnCart PostClientCart(int id, [FromBody]ProductsOnCart product)
        {
            var cart = _context.ClientCart.SingleOrDefault(ca => ca.ClientAccoundId == id);
            var productInDb = _context.ProductsOnCart.Include(p => p.Product).SingleOrDefault(p => p.ClientCartId == cart.Id && p.ProductId==product.ProductId );

            productInDb.Quantity = product.Quantity;
            productInDb.TotalPricePerProduct = productInDb.Product.Price * product.Quantity;

            _context.SaveChanges();

            

            return productInDb;
        }

        // DELETE: api/ClientCarts/5
        [HttpDelete("{id}")]
        public void DeleteClientCart(int id,int productId)
        {
            

            var clientCart =  _context.ClientCart.Include(c => c.Products).SingleOrDefault(c => c.ClientAccoundId == id);
            
            var product = _context.ProductsOnCart.SingleOrDefault(p => p.ProductId == productId && p.ClientCartId == clientCart.Id);
            _context.ProductsOnCart.Remove(product);

            double? total=0.0;
            foreach(var prod in clientCart.Products)
            {
                total += prod.TotalPricePerProduct;
            }
            clientCart.TotalPriceOfCartForUser = total;

            _context.SaveChanges();

           
        }

        private bool ClientCartExists(int id)
        {
            return _context.ClientCart.Any(e => e.Id == id);
        }
    }
}