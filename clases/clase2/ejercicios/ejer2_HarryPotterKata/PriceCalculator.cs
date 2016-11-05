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
            var localBooks = books.OrderBy(x => x.Title).ToList();
            bool doContinue = true;

            while (doContinue)
            {
                // SumPrice va a recorrer y calcular el precio para los libros distintos
                // cada vez que lo llamemos hasta que no haya libros por procesar en localBooks
                totalPrice += SumPrice(localBooks);
                if (!localBooks.Any(x=> x != null))
                {
                    doContinue = false;
                }
            }
            
            return (float) Math.Round((decimal)totalPrice, 1);
        }

        private float SumPrice(IList<Book> localBooks)
        {            
            string previousTitle = string.Empty;
            int differentBooks = 0;
            float total = 0;

            for (int i = 0; i < localBooks.Count; i++)
            {
                if (localBooks[i] != null && localBooks[i].Title != previousTitle)
                {
                    differentBooks++;
                    previousTitle = localBooks[i].Title;
                    total += localBooks[i].Price;
                    // Seteamos en null porque este libro ya fue contabilizado
                    localBooks[i] = null;
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
