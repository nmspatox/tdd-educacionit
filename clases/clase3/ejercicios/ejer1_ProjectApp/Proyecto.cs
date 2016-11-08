using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_ProjectApp
{
    public class Proyecto
    {
        #region Fields y Properties

        public string Nombre { get; set; }

        public Calendario Calendario { get; }

        private DateTime? _fechaEsperadaInicio;
        public DateTime? FechaEsperadaInicio
        {
            get
            {
                return _fechaEsperadaInicio;
            }
            set
            {
                if (value.HasValue && FechaEsperadaFin.HasValue && value > FechaEsperadaFin)
                {
                    throw new FechaInicioNoValidaException();
                }

                _fechaEsperadaInicio = value;
            }
        }

        private DateTime? _fechaEsperadaFin;
        public DateTime? FechaEsperadaFin
        {
            get
            {
                return _fechaEsperadaFin;
            }
            set
            {
                if (value.HasValue && FechaEsperadaInicio.HasValue && value < FechaEsperadaInicio)
                {
                    throw new FechaFinNoValidaException();
                }

                _fechaEsperadaFin = value;
            }
        }

        private DateTime? _fechaRealInicio;
        public DateTime? FechaRealInicio
        {
            get
            {
                return _fechaRealInicio;
            }
            set
            {
                if (value.HasValue && FechaRealFin.HasValue && value > FechaRealFin)
                {
                    throw new FechaInicioNoValidaException();
                }

                _fechaRealInicio = value;
            }
        }

        private DateTime? _fechaRealFin;
        public DateTime? FechaRealFin
        {
            get
            {
                return _fechaRealFin;
            }
            set
            {
                if (value.HasValue && FechaRealInicio.HasValue && value < FechaRealInicio)
                {
                    throw new FechaFinNoValidaException();
                }

                _fechaRealFin = value;
            }
        }

        private List<Tarea> _tareas;
        public IEnumerable<Tarea> Tareas
        {
            get { return _tareas; }
        }

        private List<Persona> _personas;
        public IEnumerable<Persona> Personas
        {
            get
            {
                return _personas;
            }
        }

        public int Duracion {
            get
            {
                int totalHs = 0;
                if (FechaRealInicio.HasValue && FechaRealFin.HasValue)
                {
                    int cantDias = FechaRealFin.Value.Subtract(FechaRealInicio.Value).Days + 1;
                    for (int i = 0; i < cantDias; i++)
                    {
                        var fecha = FechaRealInicio.Value.AddDays(i);
                        totalHs += Calendario.EsFeriado(fecha) 
                            ? 0 
                            : Calendario.EsLaboral(fecha) 
                                ? Calendario.HsJornada
                                : 0;
                    }
                }

                return totalHs;
            }
        }        

        #endregion

        #region Constructor

        public Proyecto(string nombre, Calendario calendario)
        {
            if (nombre == null || nombre == "")
            {
                throw new NombreVacioException();
            }

            if (calendario == null)
            {
                throw new CalendarioVacioException();
            }
            
            _tareas = new List<Tarea>();
            _personas = new List<Persona>();

            Nombre = nombre;
            Calendario = calendario;
        }

        #endregion

        #region Métodos Públicos

        public void AgregarTarea(Tarea unaTarea)
        {
            if (_tareas.Contains(unaTarea))
            {
                throw new NoSePuedeAgregarUnaTareaExistenteException();
            }
            _tareas.Add(unaTarea);
        }

        public void QuitarTarea(Tarea unaTarea)
        {
            if (!_tareas.Contains(unaTarea))
            {
                throw new NoSePuedeQuitarUnaTareaNoExistenteException();
            }
            _tareas.Remove(unaTarea);
        }

        public void EliminarTodasLasTareas()
        {
            _tareas.Clear();
        }

        public void AgregarPersona(Persona unaPersona)
        {
            if (_personas.Contains(unaPersona))
            {
                throw new NoSePuedeAgregarUnaPersonaExistenteException();
            }

            _personas.Add(unaPersona);
        }

        public void QuitarPersona(Persona unaPersona)
        {
            if (!_personas.Contains(unaPersona))
            {
                throw new NoSePuedeQuitarUnaPersonaNoExistenteException();
            }

            _personas.Remove(unaPersona);
        }

        public void EliminarTodasLasPersonas()
        {
            _personas.Clear();
        }

        #endregion
    }
}