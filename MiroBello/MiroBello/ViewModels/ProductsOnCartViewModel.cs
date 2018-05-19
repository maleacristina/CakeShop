using MiroBello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.ViewModels
{
    public class ProductsOnCartViewModel
    {
        public Product Product { get; set; }
        public double TotalPricePerProduct { get; set; }
    }
}
