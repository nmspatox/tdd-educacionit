using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_StringDictionary
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void StringVacioDevuelveCero()
        {
            // Arrange
            string str = "";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void StringConUnNumeroDevuelveElNumero()
        {
            // Arrange
            string str = "1";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void StringConDosNumerosDevuelveLaSuma()
        {
            // Arrange
            string str = "1,2";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void StringConTresNumerosDevuelveLaSuma()
        {
            // Arrange
            string str = "1,4,5";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void StringConNumerosYSaltosDeLineaDevuelveLaSuma()
        {
            // Arrange
            string str = "1\n4\n2";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(7, result);
        }

        [Test]
        public void StringNuloLanzaNullStringException()
        {
            // Arrange
            string str = null;

            // Act
            Exception ex = Assert.Catch(() => new StringDictionary().Add(str));

            // Assert
            Assert.IsInstanceOf<NullStringException>(ex);
        }

        [Test]
        public void StringConLetrasLanzaNotANumberException()
        {
            // Arrange
            string str = "1,A,5";

            // Act
            Exception ex = Assert.Catch(() => new StringDictionary().Add(str));

            // Assert
            Assert.IsInstanceOf<NotANumberException>(ex);
        }

        [Test]
        public void StringConDelimitadorCustomDeUnCaracterDevuelveSuma()
        {
            // Arrange
            string str = "//;\n1;3;5";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(9, result);
        }

        [Test]
        public void StringConDelimitadorCustomDeMuchosCaracteresDevuelveSuma()
        {
            // Arrange
            string str = "//***\n1***6***5";

            // Act
            int result = new StringDictionary().Add(str);

            // Assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void StringConDelimitadorCustomQueNoUtilizaLanzaNotANumberException()
        {
            // Arrange
            string str = "//***\n1,6,5";

            // Act
            Exception ex = Assert.Catch(() => new StringDictionary().Add(str));

            // Assert
            Assert.IsInstanceOf<NotANumberException>(ex);
        }
    }
}
