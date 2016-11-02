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
            CargarDiasLunesAViernes8hs(cal);                   

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
            CargarDiasLunesAViernes8hs(cal);

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
            CargarDiasLunesAViernes8hs(cal);

            // Assert
            Assert.IsFalse(cal.EsDiaLaboral(fecha));
        }

        [Test]
        public void SeCarganDiasDeLunesAViernes8hsY1deJulio2013SeTrabaja8Hs()
        {
            // Arrange
            var cal = new Calendario();
            var fecha = new DateTime(2013, 7, 1);

            // Act            
            CargarDiasLunesAViernes8hs(cal);

            // Assert
            Assert.AreEqual(8, cal.HsDeTrabajo(fecha));
        }

        [Test]
        public void SeCarganDiasDeLunesAViernes8hsY6deJulio2013SeTrabaja0Hs()
        {
            // Arrange
            var cal = new Calendario();
            var fecha = new DateTime(2013, 7, 6);

            // Act            
            CargarDiasLunesAViernes8hs(cal);

            // Assert
            Assert.AreEqual(0, cal.HsDeTrabajo(fecha));
        }

        [Test]
        public void SeCarganDiasViernes6hsY5deJulio2013SeTrabaja6hs()
        {
            // Arrange
            var cal = new Calendario();
            var fechaLunes = new DateTime(2013, 7, 1);
            var fechaViernes = new DateTime(2013, 7, 5);

            // Act            
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Sunday, 0));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Saturday, 0));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Monday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Tuesday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Wednesday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Thursday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Friday, 6));

            // Assert
            Assert.AreEqual(8, cal.HsDeTrabajo(fechaLunes));
            Assert.AreEqual(6, cal.HsDeTrabajo(fechaViernes));
        }

        [Test]
        public void NoSeCarganFeriadosEntoncesNingunaFechaEsFeriado()
        {
            // Arrange
            var cal = new Calendario();
            var fecha1 = new DateTime(2006, 3, 24);
            var fecha2 = new DateTime(2013, 12, 25);

            // Act            


            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fecha1));
            Assert.IsFalse(cal.EsDiaFeriado(fecha1));
        }

        [Test]
        public void SeCargaFeriadoEsaFechaEsFeriado()
        {
            // Arrange
            var cal = new Calendario();
            var fecha = new DateTime(2013, 12, 25);

            // Act            
            cal.Feriados.Add(new DiaFeriado(fecha.Day, fecha.Month));

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fecha));
        }

        [Test]
        public void SeCargaFeriadoConFechaInicioEntoncesEsaFechaEsFeriado()
        {
            // Arrange
            var cal = new Calendario();
            var fechaInicio = new DateTime(2006, 1, 1);
            var fechaFeriado = new DateTime(2006, 3, 24);            

            // Act            
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month) { FechaInicio= fechaInicio });

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaFeriado));            
        }

        [Test]
        public void SeCargaFeriadoConFechaInicioEntoncesFechasAñosPosterioresSonFeriados()
        {
            // Arrange
            var cal = new Calendario();
            var fechaInicio = new DateTime(2006, 1, 1);
            var fechaFeriado = new DateTime(2006, 3, 24);

            var fechaPosterior1 = new DateTime(2013, 3, 24);
            var fechaPosterior2 = new DateTime(2017, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month) { FechaInicio = fechaInicio });

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaPosterior1));
            Assert.IsTrue(cal.EsDiaFeriado(fechaPosterior2));
        }

        [Test]
        public void SeCargaFeriadoConFechaInicioEntoncesFechasAñosAnterioresNoSonFeriados()
        {
            // Arrange
            var cal = new Calendario();
            var fechaInicio = new DateTime(2006, 1, 1);
            var fechaFeriado = new DateTime(2006, 3, 24);
            var fechaAnterior1 = new DateTime(2005, 3, 24);
            var fechaAnterior2 = new DateTime(2000, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month) { FechaInicio = fechaInicio });

            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fechaAnterior1));
            Assert.IsFalse(cal.EsDiaFeriado(fechaAnterior2));
        }

        //
        [Test]
        public void SeCargaFeriadoConFechaFinEntoncesFechasAñosIgualesOAnterioresSonFeriados()
        {
            // Arrange
            var cal = new Calendario();
            var fechaFin = new DateTime(2014, 12, 31);
            var fechaFeriado = new DateTime(2014, 5, 1);

            var fechaAnterior1 = new DateTime(2009, 5, 1);
            var fechaAnterior2 = new DateTime(2014, 5, 1);

            // Act            
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month) { FechaFin = fechaFin });

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaAnterior1));
            Assert.IsTrue(cal.EsDiaFeriado(fechaAnterior2));
        }

        [Test]
        public void SeCargaFeriadoConFechaFinEntoncesFechasAñosPosterioresNoSonFeriados()
        {
            // Arrange
            var cal = new Calendario();
            var fechaFin = new DateTime(2006, 1, 1);
            var fechaFeriado = new DateTime(2006, 3, 24);
            var fechaPosterior1 = new DateTime(2015, 3, 24);
            var fechaPosterior2 = new DateTime(2020, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month) { FechaFin = fechaFin });

            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fechaPosterior1));
            Assert.IsFalse(cal.EsDiaFeriado(fechaPosterior2));
        }

        [Test]
        public void SeCargaFeriadoEntoncesEsaFechaSeTrabaja0Hs()
        {
            // Arrange
            var cal = new Calendario();
            var fechaFeriado = new DateTime(2016, 11, 1); // Martes

            // Act         
            CargarDiasLunesAViernes8hs(cal);   
            cal.Feriados.Add(new DiaFeriado(fechaFeriado.Day, fechaFeriado.Month));

            // Assert
            Assert.AreEqual(0, cal.HsDeTrabajo(fechaFeriado));
        }

        private void CargarDiasLunesAViernes8hs(Calendario cal)
        {
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Sunday, 0));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Saturday, 0));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Monday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Tuesday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Wednesday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Thursday, 8));
            cal.CargarDia(new DiaInfo((int)DayOfWeek.Friday, 8));            
        }
    }
}
