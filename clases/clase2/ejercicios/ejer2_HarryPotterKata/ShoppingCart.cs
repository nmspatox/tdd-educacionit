using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer2_HarryPotterKata
{
    public class ShoppingCart
    {
        public IList<Book> Books { get; private set; }

        public ShoppingCart()
        {
            Books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
    }
}
