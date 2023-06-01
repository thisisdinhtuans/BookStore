using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
