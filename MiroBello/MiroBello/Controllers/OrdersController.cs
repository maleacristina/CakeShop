using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiroBello.Data;
using MiroBello.Models;


namespace MiroBello.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Bill> GetBills()
        {
            return _context.Bills.Include(b => b.Client).ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillId == id);

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public Bill PutBill([FromRoute] int id, [FromBody] Bill bill)
        {

            var billInDb = _context.Bills.SingleOrDefault(b => b.BillId == id);
            billInDb.OrderStatus = bill.OrderStatus;
            _context.SaveChanges();
            return billInDb;

        }

        // POST: api/Orders
        [HttpPost("{id}")]
        public void Post(int id, [FromBody]ProductsOnCart products)
        {
            var clientCart = _context.ClientCart.Include(c => c.Products).SingleOrDefault(c => c.ClientAccoundId == id);
            var bills = new Bill
            {
                ClientAccountId = id,
                OrderPlacementDate = DateTime.Now,
                OrderStatus = "Comanda primita",
                TotalPrice = clientCart.TotalPriceOfCartForUser.Value
            };

            var billsProduct = new List<ProductsOnBills>();
            foreach(var productOnCart in clientCart.Products)
            {
                var productsOnBill = new ProductsOnBills
                {
                    ProductId = productOnCart.ProductId,
                    Quantity = productOnCart.Quantity,
                    TotalPricePerProduct = productOnCart.TotalPricePerProduct,
                    BillId = bills.BillId
                };
                billsProduct.Add(productsOnBill);

               
            }
            bills.Products = billsProduct;
            _context.Bills.Add(bills);
            _context.Remove(clientCart);
           
            _context.SaveChanges();

            //var productsOnBill = new List<ProductsOnBills>();
            //foreach(var product in clientCart.Products)
            //{
            //    productsOnBill.Add(product)
            //}

        }


        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bill = await _context.Bills.SingleOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return Ok(bill);
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}