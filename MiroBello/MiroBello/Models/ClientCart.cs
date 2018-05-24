using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class ClientCart
    {
        public int Id { get; set; }
        public ClientAccount Client { get; set; }
        public int? ClientAccoundId { get; set; }

        public double? TotalPriceOfCartForUser { get; set; }

        public virtual ICollection<ProductsOnCart> Products { get; set; }
    }
}
