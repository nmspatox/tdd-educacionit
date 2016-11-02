using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    [TestFixture]
    public class Tests
    {       
        [Test]
        public void SeCarganDiaLaboralesValidosOk()
        {
            // Arrange
            var cal = new Calendario();

            // Act     
            CargarDiasLunesAViernes(cal);                   

            // Assert
            Assert.AreEqual(7, cal.DiasCargados);
        }

        [Test]
        public void SeCargaDiaLaboralNoValidoLanzaDiaNoValidoException()
        {
            // Arrange
            var cal = new Calendario();

            // Act            
            Exception ex =  Assert.Catch(() => cal.CargarDia(new DiaInfo(12, 4)));
            
            // Assert
            Assert.IsInstanceOf<DiaNoValidoException>(ex);
        }

        [Test]
        public void SeCargaDiaConHsValidasOk()
        {
            // Arrange
            var cal = new Calendario();
            var diaDeLaSemana = 2;
            var hsDeTrabajo = 8;

            // Act            
            cal.CargarDia(new DiaInfo(diaDeLaSemana, hsDeTrabajo));

            // Assert
            Assert.AreEqual(hsDeTrabajo, cal.GetDia(diaDeLaSemana).HsDeTrabajo);
        }

        [Test]
        public void SeCargaDiaCon25HsDeTrabajoLanzaHsDeTrabajoNoValidasException()
        {
            // Arrange
            var cal = new Calendario();
            var diaDeLaSemana = 2;
            var hsDeTrabajo = 25;

            // Act            
            Exception ex = Assert.Catch(() => cal.CargarDia(new DiaInfo(diaDeLaSemana, hsDeTrabajo)));

            // Assert
            Assert.IsInstanceOf<HsDeTrabajoNoValidasException>(ex);
        }

        [Test]
        public void SeCarganDiasDeLunesAViernesY1deJulio2013EsDiaLaboral()
        {
            // Arrange
            var cal = new Calendario();
            var fecha = new DateTime(2013, 7, 1);

            // Act            
            CargarDiasLunesAViernes(cal);

            // Assert
            Assert.IsTrue(cal.EsDiaLaboral(fecha));
        }

        [Test]
        public void SeCarganDiasDeLunesAViernesY6deJulio2013NoEsDiaLaboral()
        {
            // Arrange
            var cal = new Calendario();
            var fecha = new DateTime(2013, 7, 6);

            // Act            
            CargarDiasLunesAViernes(cal);

            // Assert
            Assert.IsFalse(cal.EsDiaLaboral(fecha));
        }

        private void CargarDiasLunesAViernes(Calendario cal)
        {
            cal.CargarDia(new DiaInfo(1, 0));
            cal.CargarDia(new DiaInfo(2, 8));
            cal.CargarDia(new DiaInfo(3, 8));
            cal.CargarDia(new DiaInfo(4, 8));
            cal.CargarDia(new DiaInfo(5, 8));
            cal.CargarDia(new DiaInfo(6, 8));
            cal.CargarDia(new DiaInfo(7, 0));
        }
    }
}
