using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // One Category → Many Books
        public ICollection<Book> Books { get; set; }
    }
}
