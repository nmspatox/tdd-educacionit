using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ejer1_ProjectApp
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TieneNombreVacio()
        {
            // Arrange
            Proyecto unProyecto = null;
            Calendario unCalendario = new Calendario();

            // Act
            Exception ex = Assert.Catch(() => unProyecto = new Proyecto("", unCalendario));

            // Assert
            Assert.IsInstanceOf<NombreVacioException>(ex);
        }

        [Test]
        public void TieneCalendarioNulo()
        {
            // Arrange
            Proyecto unProyecto = null;
            Calendario unCalendario = null;

            // Act
            Exception ex = Assert.Catch(() => unProyecto = new Proyecto("proyecto1", unCalendario));

            // Assert
            Assert.IsInstanceOf<CalendarioVacioException>(ex);
        }

        [Test]
        public void TieneNombreNulo()
        {
            // Arrange
            Proyecto unProyecto = null;
            Calendario unCalendario = new Calendario();

            // Act
            Exception ex = Assert.Catch(() => unProyecto = new Proyecto(null, unCalendario));

            // Assert
            Assert.IsInstanceOf<NombreVacioException>(ex);
        }


        [Test]
        public void TieneNombreCorrecto()
        {
            // Arrange
            Proyecto unProyecto = null;
            Calendario unCalendario = new Calendario();

            // Act
            unProyecto = new Proyecto("PasamosUnTexto", unCalendario);

            // Assert
            Assert.AreEqual("PasamosUnTexto", unProyecto.Nombre);
        }        

    }
}
