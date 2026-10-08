using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateOnly RegisteredAt { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
