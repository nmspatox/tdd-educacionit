﻿using System;
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

        [Test]
        public void TieneFechaEsperadaInicio() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;

            // Act            
            unProyecto.FechaEsperadaInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaEsperadaInicio);
        }

        [Test]
        public void TieneFechaEsperadaFin() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaFin = DateTime.Now.AddDays(1);

            // Act            
            unProyecto.FechaEsperadaFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaFin, unProyecto.FechaEsperadaFin);
        }

        [Test]
        public void NoTieneFechaEsperadaInicioYTieneFechaEsperadaFin() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = null;
            DateTime? fechaFin = DateTime.Now.AddDays(1);

            // Act
            unProyecto.FechaEsperadaFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaEsperadaInicio);
            Assert.AreEqual(fechaFin, unProyecto.FechaEsperadaFin);
        }

        [Test]
        public void NoTieneFechaEsperadaFinYTieneFechaEsperadaInicio() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;
            DateTime? fechaFin = null;

            // Act
            unProyecto.FechaEsperadaInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaEsperadaInicio);
            Assert.AreEqual(fechaFin, unProyecto.FechaEsperadaFin);
        }

        [Test]
        public void TieneFechaEsperadaFinIncorrectaLanzaExcepcion() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            unProyecto.FechaEsperadaInicio = hoy;
            Exception ex = Assert.Catch(()=> unProyecto.FechaEsperadaFin = ayer);

            // Assert
            Assert.IsAssignableFrom<FechaFinNoValidaException>(ex);
        }

        [Test]
        public void TieneFechaEsperadaInicioIncorrectaLanzaExcepcion() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act            
            unProyecto.FechaEsperadaFin = ayer;
            Exception ex = Assert.Catch(() => unProyecto.FechaEsperadaInicio = hoy);

            // Assert
            Assert.IsAssignableFrom<FechaInicioNoValidaException>(ex);
        }

        [Test]
        public void TieneFechaRealInicio() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;

            // Act            
            unProyecto.FechaRealInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaRealInicio);
        }

        [Test]
        public void TieneFechaRealFin() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaFin = DateTime.Now;

            // Act            
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaFin, unProyecto.FechaRealFin);
        }

        [Test]
        public void NoTieneFechaRealInicioYTieneFechaRealFin() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = null;
            DateTime? fechaFin = DateTime.Now.AddDays(1);

            // Act
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaRealInicio);
            Assert.AreEqual(fechaFin, unProyecto.FechaRealFin);
        }

        [Test]
        public void NoTieneFechaRealFinYTieneFechaRealInicio() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;
            DateTime? fechaFin = null;

            // Act
            unProyecto.FechaRealInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaRealInicio);
            Assert.AreEqual(fechaFin, unProyecto.FechaRealFin);
        }

        [Test]
        public void TieneFechaRealFinIncorrectaLanzaExcepcion() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            unProyecto.FechaRealInicio = hoy;
            Exception ex = Assert.Catch(() => unProyecto.FechaRealFin = ayer);

            // Assert
            Assert.IsAssignableFrom<FechaFinNoValidaException>(ex);
        }

        [Test]
        public void TieneFechaRealInicioIncorrectaLanzaExcepcion() {
            // Arrange            
            Calendario unCalendario = new Calendario();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act            
            unProyecto.FechaRealFin = ayer;
            Exception ex = Assert.Catch(() => unProyecto.FechaRealInicio = hoy);

            // Assert
            Assert.IsAssignableFrom<FechaInicioNoValidaException>(ex);
        }


    }
}
