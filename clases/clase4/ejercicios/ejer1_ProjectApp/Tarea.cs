using System;
using System.Collections.Generic;

namespace ejer1_ProjectApp {
    public class Tarea {

        public string Nombre { get; set; }

        private DateTime? _fechaInicioEsperada;
        public DateTime? FechaInicioEsperada
        {
            get
            {
                return _fechaInicioEsperada;
            }
            set
            {
                if (value.HasValue && FechaFinEsperada.HasValue && value.Value > FechaFinEsperada)
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
                if (value.HasValue && FechaInicioEsperada.HasValue && value.Value < FechaInicioEsperada)
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
                if (value.HasValue && FechaFinReal.HasValue && value.Value > FechaFinReal)
                {
                    throw new FechaInicioNoValidaException();
                }

                _fechaInicioReal = value;
            }
        }

        private DateTime? _fechaFinReal;
        public DateTime? FechaFinReal
        {
            get
            {
                return _fechaFinReal;
            }
            set
            {
                if (value.HasValue && FechaInicioReal.HasValue && value.Value < FechaInicioReal)
                {
                    throw new FechaFinNoValidaException();
                }

                _fechaFinReal = value;
            }
        }

        private IList<Tarea> _subtareas;
        public IEnumerable<Tarea> Subtareas { get { return _subtareas; } }

        public Tarea(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new TareaSinNombreException();
            }

            _subtareas = new List<Tarea>();
            Nombre = nombre;
        }

        public void AgregarSubtarea(Tarea subtarea)
        {
            if (_subtareas.Contains(subtarea))
            {
                throw new NoSePuedeAgregarUnaSubtareaExistenteException();
            }
            _subtareas.Add(subtarea);
        }

        public void QuitarSubtarea(Tarea subtarea)
        {
            if (!_subtareas.Contains(subtarea))
            {
                throw new NoSePuedeQuitarUnaSubtareaNoExistenteException();
            }
            _subtareas.Clear();
        }

        public void EliminarTodasLasSubtareas()
        {
            _subtareas.Clear();
        }
    }
}