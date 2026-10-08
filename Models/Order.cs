using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
    }
}
