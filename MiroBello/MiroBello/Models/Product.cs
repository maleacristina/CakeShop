using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string ImageURL { get; set; }
        public string Details { get; set; }
        public string Weight { get; set; }
        public Category Category { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public ICollection<ProductsOnBills> Bills { get; set; }
        public virtual ICollection<ClientCart> Carts { get; set; }
    }
}
