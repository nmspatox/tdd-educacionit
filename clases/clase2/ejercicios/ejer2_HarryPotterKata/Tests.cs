using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer2_HarryPotterKata
{
    [TestFixture]
    public class Tests
    {
        float pricePerBook = 8;

        #region Tests

        [Test]
        public void Compran2LibrosIguales()
        {
            // Arrange
            var calc = new PriceCalculator();
            var cart = new ShoppingCart();
            float expectedPrice = pricePerBook * 2;

            // Act
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 1", pricePerBook));
            float totalPrice = calc.CalculatePrice(cart.Books);

            // Assert
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [Test]
        public void Compran3LibrosIgualesY1Distinto()
        {
            // Arrange
            var calc = new PriceCalculator();
            var cart = new ShoppingCart();
            float discount = 0.05F;
            float expectedPrice = (pricePerBook * 2 * (1 - discount)) + pricePerBook * 2;

            // Act
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 2", pricePerBook));
            float totalPrice = calc.CalculatePrice(cart.Books);

            // Assert
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [Test]
        public void Compran5CopiasDeTodaLaColeccion()
        {
            // Arrange
            var calc = new PriceCalculator();
            var cart = new ShoppingCart();
            float discount = 0.45F;
            float expectedPrice = (pricePerBook * 7 * (1 - discount)) * 5;

            // Act
            CargarColeccionCompleta(cart);
            CargarColeccionCompleta(cart);
            CargarColeccionCompleta(cart);
            CargarColeccionCompleta(cart);
            CargarColeccionCompleta(cart);

            float totalPrice = calc.CalculatePrice(cart.Books);

            // Assert
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [Test]
        public void Compran3LibrosDistintos()
        {
            // Arrange
            var calc = new PriceCalculator();
            var cart = new ShoppingCart();
            float discount = 0.1F;
            float expectedPrice = pricePerBook * 3 * (1 - discount);

            // Act
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 2", pricePerBook));
            cart.AddBook(new Book("Libro 3", pricePerBook));
            float totalPrice = calc.CalculatePrice(cart.Books);

            // Assert
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        [Test]
        public void Compran4LibrosDistintos()
        {
            // Arrange
            var calc = new PriceCalculator();
            var cart = new ShoppingCart();
            float discount = 0.15F;
            float expectedPrice = pricePerBook * 4 * (1- discount);

            // Act
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 2", pricePerBook));
            cart.AddBook(new Book("Libro 3", pricePerBook));
            cart.AddBook(new Book("Libro 4", pricePerBook));
            float totalPrice = calc.CalculatePrice(cart.Books);

            // Assert
            Assert.AreEqual(expectedPrice, totalPrice);
        }

        #endregion

        #region Metodos Auxiliares

        private void CargarColeccionCompleta(ShoppingCart cart)
        {
            cart.AddBook(new Book("Libro 1", pricePerBook));
            cart.AddBook(new Book("Libro 2", pricePerBook));
            cart.AddBook(new Book("Libro 3", pricePerBook));
            cart.AddBook(new Book("Libro 4", pricePerBook));
            cart.AddBook(new Book("Libro 5", pricePerBook));
            cart.AddBook(new Book("Libro 6", pricePerBook));
            cart.AddBook(new Book("Libro 7", pricePerBook));
        }

        #endregion
    }
}
