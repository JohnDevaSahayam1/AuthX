using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // Foreign Keys
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}
