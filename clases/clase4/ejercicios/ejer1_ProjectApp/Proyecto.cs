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

            Nombre = nombre;
            _tareas = new List<Tarea>();
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

        #endregion
    }
}