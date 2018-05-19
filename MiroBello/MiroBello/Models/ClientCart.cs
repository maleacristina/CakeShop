using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
    public class ClientCart
    {
        public int CartId { get; set; }
        public ClientAccount Client { get; set; }
        public int? ClientAccoundId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
