﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiroBello.Models;
using MiroBello.Data;
using MiroBello.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MiroBello.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            //return new Product[] {
            //        new Product{ ProductId=1, Name="Tort1", Price=13.45, Currency="USD", CategoryId=1, ImageURL="Images/45.png", Details="Este un tort foarte bun pentru evenimentul tau!"},
            //        new Product{ ProductId=2, Name="Tort2", Price=100.56, Currency="USD", CategoryId=1, ImageURL="Images/39.png", Details="aaaaaaaaaaaaaaaaaaaaaa!"},
            //        new Product{ ProductId=3, Name="Tort3", Price=32.00, Currency="USD", CategoryId=2, ImageURL="Images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
            //        new Product{ ProductId=4, Name="Tort3", Price=32.00, Currency="USD", CategoryId=3, ImageURL="Images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
            //        new Product{ ProductId=5, Name="Tort3", Price=32.00, Currency="USD", CategoryId=4, ImageURL="Images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"},
            //        new Product{ ProductId=6, Name="Tort3", Price=32.00, Currency="USD", CategoryId=2, ImageURL="Images/38.png", Details="bbbbbbbbbbbbbbbbbbbbbbbbbbb!"}

            //};
            var products = _context.Products.ToList();
            //var productsOnCart = new List<ProductsOnCartViewModel>();
            //foreach (var product in products)
            //{
            //    var productOnCart = new ProductsOnCartViewModel
            //    {
            //        Product = product,
            //        TotalPricePerProduct = product.Price * product.Weight
            //    };
            //}
            return products;
        }

        // GET: api/products/5
        [HttpGet("{id}", Name = "Get")]
        public Product Get(int id)
        {
            var product = _context.Products.Where(p => p.ProductId == id).SingleOrDefault();
            
            return product;
        }
        
        // POST: api/Default
        
        // PUT: api/Default/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product value)
        {
            var product =_context.ProductsOnCart.SingleOrDefault(p => p.ProductId == id);
            var addProductOnCart = new ProductsOnCart();
            if (product != null)
            {
                product.Quantity++;
            }
            else
            {
                addProductOnCart.ClientCartId = 1;
                addProductOnCart.ProductId = id;
                addProductOnCart.Quantity = 1;
                _context.ProductsOnCart.Add(addProductOnCart);
            }
            
            _context.SaveChanges();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
