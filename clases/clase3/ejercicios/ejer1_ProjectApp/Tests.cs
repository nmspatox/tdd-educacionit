﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ejer1_ProjectApp
{
    [TestFixture]
    public class Test
    {
        #region Tests

        // v1.0
        [Test]
        public void TieneNombreVacio()
        {
            // Arrange            
            Proyecto unProyecto = null;
            Calendario unCalendario = GetCalendarioDefault();

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
            Calendario unCalendario = GetCalendarioDefault();

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
            Calendario unCalendario = GetCalendarioDefault();

            // Act
            unProyecto = new Proyecto("PasamosUnTexto", unCalendario);

            // Assert
            Assert.AreEqual("PasamosUnTexto", unProyecto.Nombre);
        }

        [Test]
        public void TieneCalendarioCorrecto()
        {
            // Arrange
            Proyecto unProyecto = null;
            Calendario unCalendario = GetCalendarioDefault();

            // Act
            unProyecto = new Proyecto("PasamosUnTexto", unCalendario);

            // Assert
            Assert.AreEqual(unCalendario, unProyecto.Calendario);
        }

        [Test]
        public void TieneFechaEsperadaInicio()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;

            // Act            
            unProyecto.FechaEsperadaInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaEsperadaInicio);
        }

        [Test]
        public void TieneFechaEsperadaFin()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaFin = DateTime.Now.AddDays(1);

            // Act            
            unProyecto.FechaEsperadaFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaFin, unProyecto.FechaEsperadaFin);
        }

        [Test]
        public void NoTieneFechaEsperadaInicioYTieneFechaEsperadaFin()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void NoTieneFechaEsperadaFinYTieneFechaEsperadaInicio()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void TieneFechaEsperadaFinIncorrectaLanzaExcepcion()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act
            unProyecto.FechaEsperadaInicio = hoy;
            Exception ex = Assert.Catch(() => unProyecto.FechaEsperadaFin = ayer);

            // Assert
            Assert.IsAssignableFrom<FechaFinNoValidaException>(ex);
        }

        [Test]
        public void TieneFechaEsperadaInicioIncorrectaLanzaExcepcion()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void TieneFechaRealInicio()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaInicio = DateTime.Now;

            // Act            
            unProyecto.FechaRealInicio = fechaInicio;

            // Assert
            Assert.AreEqual(fechaInicio, unProyecto.FechaRealInicio);
        }

        [Test]
        public void TieneFechaRealFin()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? fechaFin = DateTime.Now;

            // Act            
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(fechaFin, unProyecto.FechaRealFin);
        }

        [Test]
        public void NoTieneFechaRealInicioYTieneFechaRealFin()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void NoTieneFechaRealFinYTieneFechaRealInicio()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void TieneFechaRealFinIncorrectaLanzaExcepcion()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
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
        public void TieneFechaRealInicioIncorrectaLanzaExcepcion()
        {
            // Arrange            
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            DateTime? hoy = DateTime.Now;
            DateTime? ayer = hoy.Value.AddDays(-1);

            // Act            
            unProyecto.FechaRealFin = ayer;
            Exception ex = Assert.Catch(() => unProyecto.FechaRealInicio = hoy);

            // Assert
            Assert.IsAssignableFrom<FechaInicioNoValidaException>(ex);
        }

        // v1.1
        [Test]
        public void ProyectoNuevoSinTareas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();

            // Act   
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);

            // Assert
            Assert.AreEqual(0, unProyecto.Tareas.Count());
        }

        [Test]
        public void SeAgregaUnaTareaSeTieneUnaTarea()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();

            // Act   
            unProyecto.AgregarTarea(unaTarea);

            // Assert
            Assert.AreEqual(1, unProyecto.Tareas.Count());
        }

        [Test]
        public void SePuedeAgregarUnaTareaYSeLaPuedeQuitar()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();

            // Act   
            unProyecto.AgregarTarea(unaTarea);
            unProyecto.QuitarTarea(unaTarea);

            // Assert
            Assert.AreEqual(0, unProyecto.Tareas.Count());
        }

        [Test]
        public void SePuedeQuitarUnaTareaQueNoFueAgregada()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();

            // Act   
            Exception ex = Assert.Catch(() => unProyecto.QuitarTarea(unaTarea));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeQuitarUnaTareaNoExistenteException>(ex);
        }

        [Test]
        public void SePuedeEliminarTodasLasTareas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();
            Tarea otraTarea = new Tarea();
            Tarea unaTareaMala = new Tarea();

            // Act 
            unProyecto.AgregarTarea(unaTarea);
            unProyecto.AgregarTarea(otraTarea);
            unProyecto.AgregarTarea(unaTareaMala);
            unProyecto.EliminarTodasLasTareas();

            // Assert
            Assert.AreEqual(0, unProyecto.Tareas.Count());
        }

        [Test]
        public void LasTareasAgregadasSonLasMismas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();
            Tarea otraTarea = new Tarea();

            // Act 
            unProyecto.AgregarTarea(unaTarea);
            unProyecto.AgregarTarea(otraTarea);

            // Assert
            Assert.AreEqual(unaTarea, unProyecto.Tareas.ElementAt(0));
            Assert.AreEqual(otraTarea, unProyecto.Tareas.ElementAt(1));
        }

        [Test]
        public void NoSePuedeAgregarUnaTareaExistente()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Tarea unaTarea = new Tarea();
            Tarea otraTarea = new Tarea();

            // Act 
            unProyecto.AgregarTarea(unaTarea);
            Exception ex = Assert.Catch(() => unProyecto.AgregarTarea(unaTarea));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeAgregarUnaTareaExistenteException>(ex);
        }

        [Test]
        public void ProyectoNuevoSinPersonas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();

            // Act   
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);

            // Assert
            Assert.AreEqual(0, unProyecto.Personas.Count());
        }

        [Test]
        public void SeAgregaUnaPersonaSeTieneUnaPersona()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona unaPersona = new Persona();

            // Act   
            unProyecto.AgregarPersona(unaPersona);

            // Assert
            Assert.AreEqual(1, unProyecto.Personas.Count());
        }

        [Test]
        public void SePuedeAgregarUnaPersonaYSeLaPuedeQuitar()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona unaPersona = new Persona();

            // Act   
            unProyecto.AgregarPersona(unaPersona);
            unProyecto.QuitarPersona(unaPersona);

            // Assert
            Assert.AreEqual(0, unProyecto.Personas.Count());
        }

        [Test]
        public void SePuedeQuitarUnaPersonaQueNoFueAgregada()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona unaPersona = new Persona();

            // Act   
            Exception ex = Assert.Catch(() => unProyecto.QuitarPersona(unaPersona));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeQuitarUnaPersonaNoExistenteException>(ex);
        }

        [Test]
        public void SePuedenEliminarTodasLasPersonas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona persona1 = new Persona();
            Persona persona2 = new Persona();
            Persona persona3 = new Persona();

            // Act 
            unProyecto.AgregarPersona(persona1);
            unProyecto.AgregarPersona(persona2);
            unProyecto.AgregarPersona(persona3);
            unProyecto.EliminarTodasLasPersonas();

            // Assert
            Assert.AreEqual(0, unProyecto.Personas.Count());
        }

        [Test]
        public void LasPersonasAgregadasSonLasMismas()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona unaPersona = new Persona();
            Persona otraPersona = new Persona();

            // Act 
            unProyecto.AgregarPersona(unaPersona);
            unProyecto.AgregarPersona(otraPersona);

            // Assert
            Assert.AreEqual(unaPersona, unProyecto.Personas.ElementAt(0));
            Assert.AreEqual(otraPersona, unProyecto.Personas.ElementAt(1));
        }

        [Test]
        public void NoSePuedeAgregarUnaPersonaExistente()
        {
            // Arrange
            Calendario unCalendario = GetCalendarioDefault();
            Proyecto unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            Persona unaPersona = new Persona();

            // Act 
            unProyecto.AgregarPersona(unaPersona);
            Exception ex = Assert.Catch(() => unProyecto.AgregarPersona(unaPersona));

            // Assert
            Assert.IsAssignableFrom<NoSePuedeAgregarUnaPersonaExistenteException>(ex);
        }

        // 1.2
        [Test]
        public void ProyectoDe1SemanaDe8hsDura40hs()
        {
            // Arrange
            var diasNoLaborables = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };
            var duracionJornada = 8;
            var duracionProyectoEsperada = 40;
            var fechaInicio = new DateTime(2016, 11, 7); // Lues
            var fechaFin = new DateTime(2016, 11, 11); // Viernes

            // Act 
            var unCalendario = new Calendario(diasNoLaborables, duracionJornada);
            var unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            unProyecto.FechaRealInicio = fechaInicio;
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(duracionProyectoEsperada, unProyecto.Duracion);
        }

        [Test]
        public void ProyectoDe4diasDe6hsDura24hs()
        {
            // Arrange
            var diasNoLaborables = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };
            var duracionJornada = 6;
            var duracionProyectoEsperada = 24;
            var fechaInicio = new DateTime(2016, 11, 7); // Lunes
            var fechaFin = new DateTime(2016, 11, 10); // Jueves

            // Act 
            var unCalendario = new Calendario(diasNoLaborables, duracionJornada);
            var unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            unProyecto.FechaRealInicio = fechaInicio;
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(duracionProyectoEsperada, unProyecto.Duracion);
        }

        [Test]
        public void ProyectoDe4diasDe6hsConFeriadoDura18hs()
        {
            // Arrange
            var diasNoLaborables = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };            
            var duracionJornada = 6;
            var duracionProyectoEsperada = 18;
            var fechaInicio = new DateTime(2016, 11, 7); // Lunes
            var fechaFin = new DateTime(2016, 11, 10); // Jueves
            var feriado = new DateTime(2016, 11, 8); // Martes

            // Act 
            var unCalendario = new Calendario(diasNoLaborables, duracionJornada);
            unCalendario.AgregarFeriado(feriado);

            var unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            unProyecto.FechaRealInicio = fechaInicio;
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(duracionProyectoEsperada, unProyecto.Duracion);
        }

        [Test]
        public void ProyectoDe2SemanasDe8hsDura80hs()
        {
            // Arrange
            var diasNoLaborables = new List<int>() { (int)DayOfWeek.Saturday, (int)DayOfWeek.Sunday };
            var duracionJornada = 8;
            var duracionProyectoEsperada = 80;
            var fechaInicio = new DateTime(2016, 11, 7); // Lunes Semana 1
            var fechaFin = new DateTime(2016, 11, 18); // Viernes Semana 2

            // Act 
            var unCalendario = new Calendario(diasNoLaborables, duracionJornada);

            var unProyecto = new Proyecto("PasamosUnTexto", unCalendario);
            unProyecto.FechaRealInicio = fechaInicio;
            unProyecto.FechaRealFin = fechaFin;

            // Assert
            Assert.AreEqual(duracionProyectoEsperada, unProyecto.Duracion);
        }

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