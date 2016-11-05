using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer2_HarryPotterKata
{
    public class PriceCalculator
    {              
        public float CalculatePrice(IEnumerable<Book> books)
        {
            float totalPrice = 0;
            // Creamos una copia de la lista pero ordenada por titulo
            var orderedBooks = books.OrderBy(x => x.Title).ToList();
            bool doContinue = true;

            while (doContinue)
            {
                // SumPrice va a recorrer y calcular el precio para los libros distintos
                // cada vez que lo llamemos hasta que ya no haya libros por procesar en orderedBooks
                totalPrice += SumPrice(orderedBooks);
                if (!orderedBooks.Any(x=> x != null))
                {
                    doContinue = false;
                }
            }
            
            return totalPrice;
        }

        private float SumPrice(IList<Book> orderedBooks)
        {            
            string previousTitle = string.Empty;
            int differentBooks = 0;
            float total = 0;

            for (int i = 0; i < orderedBooks.Count; i++)
            {
                if (orderedBooks[i] != null && orderedBooks[i].Title != previousTitle)
                {
                    differentBooks++;
                    previousTitle = orderedBooks[i].Title;
                    total += orderedBooks[i].Price;
                    // Seteamos en null porque este libro ya fue contabilizado
                    orderedBooks[i] = null;
                }
            }

            return total * (1 - GetDiscount(differentBooks));
        }

        private float GetDiscount(int differentBooks)
        {
            float discount = 0;

            switch (differentBooks)
            {
                case 1:
                    discount = 0;
                    break;
                case 2:
                    discount = 0.05F;
                    break;
                case 3:
                    discount = 0.1F;
                    break;
                case 4:
                    discount = 0.15F;
                    break;
                case 5:
                    discount = 0.2F;
                    break;
                case 6:
                    discount = 0.3F;
                    break;
                case 7:
                    discount = 0.45F;
                    break;                
            }

            return discount;
        }
    }
}
