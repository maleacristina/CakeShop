using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiroBello.Data;
using MiroBello.Models;

namespace MiroBello.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public ClientAccount GetNameClientAccount(string id)
        {
            var client = _context.ClientAccounts.Where(p => p.FirstName == id).SingleOrDefault();

            return client;
        }

        [HttpGet]
        public ClientAccount GetTelephoneClientAccount(string id)
        {
            var telephone = _context.ClientAccounts.Where(p => p.PhoneNumber == id).SingleOrDefault();

            return telephone;
        }
        [HttpGet]
        public ClientAccount GetAddressClientAccount(string id)
        {
            var address = _context.ClientAccounts.Where(p => p.Address == id).SingleOrDefault();

            return address;
        }

        [HttpGet]
        public Bill GetDateBill(DateTime id)
        {
            var date = _context.Bills.Where(p => p.OrderPlacementDate == id).SingleOrDefault();

            return date;
        }

        [HttpGet]
        public Bill GetTotalBill(int id)
        {
            var total = _context.Bills.Where(p => p.TotalPrice == id).SingleOrDefault();

            return total;
        }

       

        [HttpGet]
        public Bill GetStatusBill(string id)
        {
            var total = _context.Bills.Where(p => p.OrderStatus == id).SingleOrDefault();

            return total;
        }

    }
}