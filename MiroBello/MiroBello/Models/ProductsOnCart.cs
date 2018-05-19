using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class ProductsOnCart
    {
        public int Id { get; set; }

        public int ProductId  { get; set; }
        public Product Product { get; set; }

        public int ClientCartId { get; set; }
        public ClientCart ClientCart { get; set; }
    }
}
