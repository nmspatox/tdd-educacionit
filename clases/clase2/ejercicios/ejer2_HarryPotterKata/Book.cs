using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer2_HarryPotterKata
{
    public class Book
    {
        public string Title { get; private set; }
        public float Price { get; private set; }

        public Book(string title, float price)
        {
            Title = title;
            Price = price;
        }
    }
}
