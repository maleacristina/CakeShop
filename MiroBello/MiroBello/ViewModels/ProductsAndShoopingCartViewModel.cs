using MiroBello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.ViewModels
{
    public class ProductsAndShoopingCartViewModel
    {
        public IEnumerable<Product> ProductsInCart { get; set; }
        public ClientCart ShoopingCart { get; set; }
        
    }
}
