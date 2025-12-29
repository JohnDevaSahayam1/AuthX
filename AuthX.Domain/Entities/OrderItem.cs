using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        // Foreign Keys
        public int OrderId { get; set; }
        public int BookId { get; set; }

        // Navigation
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
