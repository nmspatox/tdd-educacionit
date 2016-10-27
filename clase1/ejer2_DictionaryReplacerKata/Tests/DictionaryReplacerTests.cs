using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ejer2_DictionaryReplacerKata.Tests {
    [TestFixture]
    public class DictionaryReplacerTests {
             
        [Test]
        public void EsNulo() {
            Dictionary<string, string> dic = null;
            var obj = new DictionaryReplacer();
            var result = obj.Replace("", dic);

            Assert.AreEqual("", result);
        }

        [Test]
        public void EstaVacio() {
            var dic = new Dictionary<string, string>();
            var obj = new DictionaryReplacer();
            var result = obj.Replace("", dic);

            Assert.AreEqual("", result);
        }

        [Test]
        public void MuestraNombre() {
            var texto = "$nombre$";
            var dic = new Dictionary<string, string>();
            dic.Add("nombre", "Juan");

            var obj = new DictionaryReplacer();
            var result = obj.Replace(texto, dic);

            Assert.AreEqual("Juan", result);
        }

        [Test]
        public void MuestraNombreYEdad() {
            var texto = "$nombre$ tiene $edad$ años";
            var dic = new Dictionary<string, string>();
            dic.Add("nombre", "Juan");
            dic.Add("edad", "27");

            var obj = new DictionaryReplacer();
            var result = obj.Replace(texto, dic);

            Assert.AreEqual("Juan tiene 27 años", result);
        }

        [Test]
        public void MuestraTextoOriginal() {
            var texto = "texto sin ninguna clave";
            var dic = new Dictionary<string, string>();
            dic.Add("nombre", "Juan");
            dic.Add("edad", "27");

            var obj = new DictionaryReplacer();
            var result = obj.Replace(texto, dic);

            Assert.AreEqual(texto, result);
        }
    }
}
