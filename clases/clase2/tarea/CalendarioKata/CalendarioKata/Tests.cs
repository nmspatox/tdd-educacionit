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
        #region Tests

        [Test]
        public void SeCarganDiaLaboralesValidosOk()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = null;

            // Act     
            cal = new Calendario(dias);

            // Assert
            Assert.AreEqual(7, cal.DiasCargados);
        }

        [Test]
        public void SeCargaDiaLaboralNoValidoLanzaDiaNoValidoException()
        {
            // Arrange
            var dias = new List<DiaInfo>() { new DiaInfo(12, 4) };
            Calendario cal = null;

            // Act            
            Exception ex = Assert.Catch(() => cal = new Calendario(dias) );

            // Assert
            Assert.IsInstanceOf<DiaNoValidoException>(ex);
        }

        [Test]
        public void SeCarganDiasRepetidosLanzaDiaYaCargadoException()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            dias.Add(new DiaInfo((int)DayOfWeek.Monday, 6));
            Calendario cal = null;

            // Act            
            Exception ex = Assert.Catch(() => cal = new Calendario(dias));

            // Assert
            Assert.IsInstanceOf<DiaYaCargadoException>(ex);
        }

        [Test]
        public void SeCargaDiaConHsValidasOk()
        {
            // Arrange
            var diaDeLaSemana = 2;
            var hsDeTrabajo = 8;
            var dias = new List<DiaInfo>() { new DiaInfo(diaDeLaSemana, hsDeTrabajo) };
            Calendario cal = null;

            // Act           
            cal = new Calendario(dias);

            // Assert
            Assert.AreEqual(hsDeTrabajo, cal.GetDia(diaDeLaSemana).HsDeTrabajo);
        }

        [Test]
        public void SeCargaDiaCon25HsDeTrabajoLanzaHsDeTrabajoNoValidasException()
        {
            // Arrange
            var diaDeLaSemana = 2;
            var hsDeTrabajo = 25;
            var dias = new List<DiaInfo>() { new DiaInfo(diaDeLaSemana, hsDeTrabajo) };
            Calendario cal = null;

            // Act            
            Exception ex = Assert.Catch(() => cal = new Calendario(dias));

            // Assert
            Assert.IsInstanceOf<HsDeTrabajoNoValidasException>(ex);
        }

        [Test]
        public void SeCarganDiasValidosYLunesEsDiaLaboral()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            var fecha = new DateTime(2013, 7, 1); // Lunes
            Calendario cal = null;

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.IsTrue(cal.EsDiaLaboral(fecha));
        }

        [Test]
        public void SeCarganDiasValidosYSabadoNoEsDiaLaboral()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            var fecha = new DateTime(2013, 7, 6); // Sabado
            Calendario cal = null;

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.IsFalse(cal.EsDiaLaboral(fecha));
        }

        [Test]
        public void SeCarganDiasValidosYLunesSeTrabaja8Hs()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = null;
            var fecha = new DateTime(2013, 7, 1); // Lunes

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.AreEqual(8, cal.HsDeTrabajo(fecha));
        }

        [Test]
        public void SeCarganDiasValidosYSabadoSeTrabaja0Hs()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = null;
            var fecha = new DateTime(2013, 7, 6); // Sabado

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.AreEqual(0, cal.HsDeTrabajo(fecha));
        }

        [Test]
        public void SeCargaDiasCon8hsYViernesCon6hs()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            dias.Single(x => x.DiaDeLaSemana == (int)DayOfWeek.Friday).HsDeTrabajo = 6;

            Calendario cal = null;
            var fechaLunes = new DateTime(2013, 7, 1);
            var fechaViernes = new DateTime(2013, 7, 5);

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.AreEqual(8, cal.HsDeTrabajo(fechaLunes));
            Assert.AreEqual(6, cal.HsDeTrabajo(fechaViernes));
        }

        [Test]
        public void NoSeCarganFeriadosEntoncesNingunaFechaEsFeriado()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = null;
            var fecha1 = new DateTime(2006, 3, 24); // Domingo
            var fecha2 = new DateTime(2013, 12, 25); // Lunes - Navidad

            // Act            
            cal = new Calendario(dias);

            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fecha1));
            Assert.IsFalse(cal.EsDiaFeriado(fecha2));
        }

        [Test]
        public void SeCargaFeriadoEsaFechaEsFeriado()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var fecha = new DateTime(2013, 12, 25);

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnual(fecha.Day, fecha.Month));

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fecha));
        }

        [Test]
        public void SeCargaFeriadoConAñoInicioEntoncesEsaFechaEsFeriado()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var añoInicio = 2006;
            var fechaFeriado = new DateTime(2006, 3, 24); // Domingo

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnualConAñoInicio(fechaFeriado.Day, fechaFeriado.Month, añoInicio));

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaFeriado));
        }

        [Test]
        public void SeCargaFeriadoConAñoInicioEntoncesFechasAñosPosterioresSonFeriados()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var añoInicio = 2006;
            var fechaFeriado = new DateTime(2006, 3, 24);
            var fechaPosterior1 = new DateTime(2013, 3, 24);
            var fechaPosterior2 = new DateTime(2017, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnualConAñoInicio(fechaFeriado.Day, fechaFeriado.Month, añoInicio));

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaPosterior1));
            Assert.IsTrue(cal.EsDiaFeriado(fechaPosterior2));
        }

        [Test]
        public void SeCargaFeriadoConAñoInicioEntoncesFechasAñosAnterioresNoSonFeriados()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var añoInicio = 2006;
            var fechaFeriado = new DateTime(2006, 3, 24);
            var fechaAnterior1 = new DateTime(2005, 3, 24);
            var fechaAnterior2 = new DateTime(2000, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnualConAñoInicio(fechaFeriado.Day, fechaFeriado.Month, añoInicio));

            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fechaAnterior1));
            Assert.IsFalse(cal.EsDiaFeriado(fechaAnterior2));
        }

        [Test]
        public void SeCargaFeriadoConAñoFinEntoncesFechasAñosIgualesOAnterioresSonFeriados()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var añoFin = 2014;
            var fechaFeriado = new DateTime(2014, 5, 1);
            var fechaAnterior1 = new DateTime(2009, 5, 1);
            var fechaAnterior2 = new DateTime(2014, 5, 1);

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnualConAñoFin(fechaFeriado.Day, fechaFeriado.Month, añoFin));

            // Assert
            Assert.IsTrue(cal.EsDiaFeriado(fechaAnterior1));
            Assert.IsTrue(cal.EsDiaFeriado(fechaAnterior2));
        }

        [Test]
        public void SeCargaFeriadoConAñoFinEntoncesFechasAñosPosterioresNoSonFeriados()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var añoFin = 2006;
            var fechaFeriado = new DateTime(2006, 3, 24);
            var fechaPosterior1 = new DateTime(2015, 3, 24);
            var fechaPosterior2 = new DateTime(2020, 3, 24);

            // Act            
            cal.Feriados.Add(new DiaFeriadoAnualConAñoFin(fechaFeriado.Day, fechaFeriado.Month, añoFin));

            // Assert
            Assert.IsFalse(cal.EsDiaFeriado(fechaPosterior1));
            Assert.IsFalse(cal.EsDiaFeriado(fechaPosterior2));
        }

        [Test]
        public void SeCargaFeriadoEntoncesEsaFechaSeTrabaja0Hs()
        {
            // Arrange
            var dias = GenerarDiasLunesAViernes8hs();
            Calendario cal = new Calendario(dias);
            var fechaFeriado = new DateTime(2016, 11, 1); // Martes

            // Act         
            cal.Feriados.Add(new DiaFeriadoAnual(fechaFeriado.Day, fechaFeriado.Month));

            // Assert
            Assert.AreEqual(0, cal.HsDeTrabajo(fechaFeriado));
        }

        #endregion

        #region Metodos Auxiliares

        private IList<DiaInfo> GenerarDiasLunesAViernes8hs()
        {
            return new List<DiaInfo>()
            {
                new DiaInfo((int)DayOfWeek.Sunday, 0),
                new DiaInfo((int)DayOfWeek.Saturday, 0),
                new DiaInfo((int)DayOfWeek.Monday, 8),
                new DiaInfo((int)DayOfWeek.Tuesday, 8),
                new DiaInfo((int)DayOfWeek.Wednesday, 8),
                new DiaInfo((int)DayOfWeek.Thursday, 8),
                new DiaInfo((int)DayOfWeek.Friday, 8)
            };                      
        }

        #endregion
    }
}
