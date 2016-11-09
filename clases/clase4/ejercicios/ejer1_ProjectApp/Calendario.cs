using System;
using System.Collections.Generic;

namespace ejer1_ProjectApp {
    public class Calendario {

        private List<DateTime> _feriados;
        public int HsJornada { get; }

        public Calendario(IList<int> diasNoLaborables, int hsJornada)
        {
            _feriados = new List<DateTime>();

            HsJornada = hsJornada;            
        }

        public void AgregarFeriado(DateTime feriado)
        {
            _feriados.Add(feriado);
        }

        public bool EsFeriado(DateTime fecha)
        {
            return _feriados.Contains(fecha);
        }

        public bool EsLaboral(DateTime fecha)
        {
            return fecha.DayOfWeek != DayOfWeek.Sunday && fecha.DayOfWeek != DayOfWeek.Saturday;
        }
    }
}