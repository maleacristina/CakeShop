using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroBello.Models
{
   
    

    public class Bill
    {
        public int BillId { get; set; }
        public DateTime OrderPlacementDate { get; set; }
        public string OrderStatus { get; set; }
        public double TotalPrice { get; set; }
        public string DeliveryType { get; set; }
        public string PaymentModality { get; set; }
        public Nullable<int> ClientId { get; set; }

    }
}
