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

        private DateTime? _fechaInicioEsperada;
        public DateTime? FechaInicioEsperada
        {
            get
            {
                return _fechaInicioEsperada;
            }
            set
            {
                if (value.HasValue && FechaFinEsperada.HasValue && value > FechaFinEsperada)
                {
                    throw new FechaInicioNoValidaException();
                }

                _fechaInicioEsperada = value;
            }
        }

        private DateTime? _fechaFinEsperada;
        public DateTime? FechaFinEsperada
        {
            get
            {
                return _fechaFinEsperada;
            }
            set
            {
                if (value.HasValue && FechaInicioEsperada.HasValue && value < FechaInicioEsperada)
                {
                    throw new FechaFinNoValidaException();
                }

                _fechaFinEsperada = value;
            }
        }

        private DateTime? _fechaInicioReal;
        public DateTime? FechaInicioReal
        {
            get
            {
                return _fechaInicioReal;
            }
            set
            {
                if (value.HasValue && FechaFinReal.HasValue && value > FechaFinReal)
                {
                    throw new FechaInicioNoValidaException();
                }

                _fechaInicioReal = value;
            }
        }

        private DateTime? _fechaRealFin;
        public DateTime? FechaFinReal
        {
            get
            {
                return _fechaRealFin;
            }
            set
            {
                if (value.HasValue && FechaInicioReal.HasValue && value < FechaInicioReal)
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
                if (FechaInicioReal.HasValue && FechaFinReal.HasValue)
                {
                    int cantDias = FechaFinReal.Value.Subtract(FechaInicioReal.Value).Days + 1;
                    for (int i = 0; i < cantDias; i++)
                    {
                        var fecha = FechaInicioReal.Value.AddDays(i);
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

            ActualizarFechasEsperadas();
            ActualizarFechasReales();
        }

        private void ActualizarFechasReales()
        {
            FechaInicioReal = _tareas.Min(x => x.FechaInicioReal);
            FechaFinReal = _tareas.Max(x => x.FechaFinReal);
        }

        private void ActualizarFechasEsperadas()
        {
            FechaInicioEsperada = _tareas.Min(x => x.FechaInicioEsperada);
            FechaFinEsperada = _tareas.Max(x => x.FechaFinEsperada);
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