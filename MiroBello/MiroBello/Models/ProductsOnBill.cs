using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class ProductsOnBill
    {
        public Nullable<int> BillId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
