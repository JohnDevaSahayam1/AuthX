using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        // One Author → Many Books
        public ICollection<Book> Books { get; set; }
    }
}