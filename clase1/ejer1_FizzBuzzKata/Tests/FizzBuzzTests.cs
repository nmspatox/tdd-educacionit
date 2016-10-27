using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ejer1_FizzbuzzKata.Tests {

    [TestFixture]
    public class FizzBuzzTests {

        [Test]
        public void SeMuestraNumero() {
            int numero = 2;
            var obj = new FizzBuzz();
            string result = obj.Get(numero);

            Assert.AreEqual(numero.ToString(), result);
        }

        [Test]
        public void SeMuestraFizz() {
            int numero = 3;
            var obj = new FizzBuzz();
            string result = obj.Get(numero);

            Assert.AreEqual("Fizz", result);
        }

        [Test]
        public void SeMuestraBuzz() {
            int numero = 5;
            var obj = new FizzBuzz();
            string result = obj.Get(numero);

            Assert.AreEqual("Buzz", result);
        }

        [Test]
        public void SeMuestraFizzBuzz() {
            int numero = 15;
            var obj = new FizzBuzz();
            string result = obj.Get(numero);

            Assert.AreEqual("FizzBuzz", result);
        }
    }
}
