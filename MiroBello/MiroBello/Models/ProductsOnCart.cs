using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class ProductsOnCart
    {
        public int Id { get; set; }

        public Nullable<int> ProductId  { get; set; }
        public Product Product { get; set; }

        public double Quantity { get; set; }

        public Nullable<int> ClientCartId { get; set; }
        public ClientCart ClientCart { get; set; }
    }
}
