using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ejer1_ProjectApp
{
    [TestFixture]
    public class Test_v2
    {
        #region Tests

        // v2.0
        [Test]
        public void TareaDebeTenerNombre()
        {
            // Arrange            
            Tarea tarea = null;
            string nombre = null;

            // Act
            Exception ex = Assert.Catch(() => tarea = new Tarea(nombre));

            // Assert
            Assert.IsInstanceOf<TareaSinNombreException>(ex);
        }
        [Test]
        public void TareaTieneNombreCorrecto()
        {
            // Arrange            
            Tarea tarea = null;
            string nombre = "tarea 1";

            // Act
            tarea = new Tarea(nombre);

            // Assert
            Assert.AreEqual(nombre, tarea.Nombre);
        }

        [Test]
        public void TareaTieneFechaFinEsperadaCorrecta()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? fechaFinEsperada = DateTime.Now.AddDays(1);

            // Act
            tarea.FechaFinEsperada = fechaFinEsperada;

            // Assert
            Assert.AreEqual(fechaFinEsperada, tarea.FechaFinEsperada);
        }

        [Test]
        public void TareaTieneFechaInicioEsperadaCorrecta()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? fechaInicioEsperada = DateTime.Now;

            // Act
            tarea.FechaInicioEsperada = fechaInicioEsperada;

            // Assert
            Assert.AreEqual(fechaInicioEsperada, tarea.FechaInicioEsperada);
        }

        [Test]
        public void TareaConFechaInicioEsperadaMayorAFinLanzaExcepcion()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            tarea.FechaFinEsperada = ayer;
            Exception ex = Assert.Catch(()=> tarea.FechaInicioEsperada = hoy );

            // Assert
            Assert.IsAssignableFrom<FechaInicioNoValidaException>(ex);
        }

        [Test]
        public void TareaConFechaFinEsperadaMenorAInicioLanzaExcepcion()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            tarea.FechaInicioEsperada = hoy;            
            Exception ex = Assert.Catch(() => tarea.FechaFinEsperada = ayer);

            // Assert
            Assert.IsAssignableFrom<FechaFinNoValidaException>(ex);
        }

        [Test]
        public void TareaTieneFechaFinRealCorrecta()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? fechaFinReal = DateTime.Now.AddDays(1);

            // Act
            tarea.FechaFinReal = fechaFinReal;

            // Assert
            Assert.AreEqual(fechaFinReal, tarea.FechaFinReal);
        }

        [Test]
        public void TareaTieneFechaInicioRealCorrecta()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? fechaInicioReal = DateTime.Now;

            // Act
            tarea.FechaInicioReal = fechaInicioReal;

            // Assert
            Assert.AreEqual(fechaInicioReal, tarea.FechaInicioReal);
        }

        [Test]
        public void TareaConFechaInicioRealMayorAFinLanzaExcepcion()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            tarea.FechaFinReal = ayer;
            Exception ex = Assert.Catch(() => tarea.FechaInicioReal = hoy);

            // Assert
            Assert.IsAssignableFrom<FechaInicioNoValidaException>(ex);
        }

        [Test]
        public void TareaConFechaFinRealMenorAInicioLanzaExcepcion()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            tarea.FechaInicioReal = hoy;
            Exception ex = Assert.Catch(() => tarea.FechaFinReal = ayer);

            // Assert
            Assert.IsAssignableFrom<FechaFinNoValidaException>(ex);
        }

        [Test]
        public void LaFechaInicioEsperadaEsLaMenorFechaInicioEsperadaDeTareas()
        {
            // Arrange            
            var calendario = GetCalendarioDefault();
            var proyecto = new Proyecto("proyecto", calendario);
            var tareaAyer = new Tarea("tarea 1");
            var tareaHoy = new Tarea("tarea 2");            
            DateTime? ayer = DateTime.Now.AddDays(-1);
            DateTime? hoy = DateTime.Now;

            // Act
            tareaAyer.FechaInicioEsperada = ayer;
            tareaHoy.FechaInicioEsperada = hoy;            
            proyecto.AgregarTarea(tareaHoy);
            proyecto.AgregarTarea(tareaAyer);

            // Assert
            Assert.AreEqual(ayer, proyecto.FechaInicioEsperada);
        }

        [Test]
        public void LaFechaFinEsperadaEsLaMayorFechaFinEsperadaDeTareas()
        {
            // Arrange            
            var calendario = GetCalendarioDefault();
            var proyecto = new Proyecto("proyecto", calendario);
            var tareaAyer = new Tarea("tarea 1");
            var tareaHoy = new Tarea("tarea 2");
            DateTime? ayer = DateTime.Now.AddDays(-1);
            DateTime? hoy = DateTime.Now;

            // Act
            tareaAyer.FechaFinEsperada = ayer;
            tareaHoy.FechaFinEsperada = hoy;
            proyecto.AgregarTarea(tareaHoy);
            proyecto.AgregarTarea(tareaAyer);

            // Assert
            Assert.AreEqual(hoy, proyecto.FechaFinEsperada);
        }

        [Test]
        public void LaFechaInicioRealEsLaMenorFechaInicioRealDeTareas()
        {
            // Arrange            
            var calendario = GetCalendarioDefault();
            var proyecto = new Proyecto("proyecto", calendario);
            var tareaAyer = new Tarea("tarea 1");
            var tareaHoy = new Tarea("tarea 2");
            DateTime? ayer = DateTime.Now.AddDays(-1);
            DateTime? hoy = DateTime.Now;

            // Act
            tareaAyer.FechaInicioReal = ayer;
            tareaHoy.FechaInicioReal = hoy;
            proyecto.AgregarTarea(tareaHoy);
            proyecto.AgregarTarea(tareaAyer);

            // Assert
            Assert.AreEqual(ayer, proyecto.FechaInicioReal);
        }

        [Test]
        public void LaFechaFinRealEsLaMayorFechaFinRealDeTareas()
        {
            // Arrange            
            var calendario = GetCalendarioDefault();
            var proyecto = new Proyecto("proyecto", calendario);
            var tareaAyer = new Tarea("tarea 1");
            var tareaHoy = new Tarea("tarea 2");
            DateTime? ayer = DateTime.Now.AddDays(-1);
            DateTime? hoy = DateTime.Now;

            // Act
            tareaAyer.FechaFinReal = ayer;
            tareaHoy.FechaFinReal = hoy;
            proyecto.AgregarTarea(tareaHoy);
            proyecto.AgregarTarea(tareaAyer);

            // Assert
            Assert.AreEqual(hoy, proyecto.FechaFinReal);
        }

        // v2.1
        [Test]
        public void TareaNuevaSinSubtareas()
        {
            // Arrange
            Tarea tarea = null;

            // Act   
            tarea = new Tarea("tarea");

            // Assert
            Assert.AreEqual(0, tarea.Subtareas.Count());
        }

        [Test]
        public void SeAgregaUnaSubtareaSeTieneUnaSubtarea()
        {
            // Arrange
            var tarea = new Tarea("tarea");
            var subtarea = new Tarea("subtarea");

            // Act   
            tarea.AgregarSubtarea(tarea);

            // Assert
            Assert.AreEqual(1, tarea.Subtareas.Count());
        }

        [Test]
        public void SePuedeAgregarUnaSubtareaYSeLaPuedeQuitar()
        {
            // Arrange            
            var tarea = new Tarea("tarea");
            var subtarea = new Tarea("subtarea");

            // Act   
            tarea.AgregarSubtarea(subtarea);
            tarea.QuitarSubtarea(subtarea);

            // Assert
            Assert.AreEqual(0, tarea.Subtareas.Count());
        }

        [Test]
        public void NoSePuedeQuitarUnaSubtareaQueNoFueAgregada()
        {
            // Arrange
            var tarea = new Tarea("tarea");
            var subtarea = new Tarea("subtarea");

            // Act   
            Exception ex = Assert.Catch(() => tarea.QuitarSubtarea(subtarea));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeQuitarUnaSubtareaNoExistenteException>(ex);
        }

        [Test]
        public void SePuedeEliminarTodasLasSubtareas()
        {
            // Arrange
            var tarea = new Tarea("tarea");
            var subtarea1 = new Tarea("subtarea 1");
            var subtarea2 = new Tarea("subtarea 2");
            var subtarea3 = new Tarea("subtarea 3");

            // Act 
            tarea.AgregarSubtarea(subtarea1);
            tarea.AgregarSubtarea(subtarea2);
            tarea.AgregarSubtarea(subtarea3);
            tarea.EliminarTodasLasSubtareas();

            // Assert
            Assert.AreEqual(0, tarea.Subtareas.Count());
        }

        [Test]
        public void LasTareasAgregadasSonLasMismas()
        {
            // Arrange
            var tarea = new Tarea("tarea");
            var subtarea1 = new Tarea("subtarea 1");
            var subtarea2 = new Tarea("subtarea 2");

            // Act 
            tarea.AgregarSubtarea(subtarea1);
            tarea.AgregarSubtarea(subtarea2);

            // Assert
            Assert.AreEqual(subtarea1, tarea.Subtareas.ElementAt(0));
            Assert.AreEqual(subtarea2, tarea.Subtareas.ElementAt(1));
        }

        [Test]
        public void NoSePuedeAgregarUnaSubtareaExistente()
        {
            // Arrange
            Tarea tarea = new Tarea("tarea");
            Tarea subtarea1 = new Tarea("subtarea 1");

            // Act 
            tarea.AgregarSubtarea(subtarea1);
            Exception ex = Assert.Catch(() => tarea.AgregarSubtarea(subtarea1));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeAgregarUnaSubtareaExistenteException>(ex);
        }

        // v2.2
        // TODO

        #endregion

        #region Metodos Auxiliares

        private Calendario GetCalendarioDefault()
        {
            var diasNoLaborables = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };
            var duracionJornada = 8;

            return new Calendario(diasNoLaborables, duracionJornada);
        }

        #endregion

    }
}