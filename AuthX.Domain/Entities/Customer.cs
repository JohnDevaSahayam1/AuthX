using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        // One Customer → Many Orders
        public ICollection<Order> Orders { get; set; }
    }
}
