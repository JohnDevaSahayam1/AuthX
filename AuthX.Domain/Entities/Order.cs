using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        // Navigation
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
