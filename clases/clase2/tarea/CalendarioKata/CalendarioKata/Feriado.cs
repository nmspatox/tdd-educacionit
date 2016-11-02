using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public abstract class Feriado
    {        
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int HsDeTrabajo { get; set; }

        public Feriado()
        {
            HsDeTrabajo = 0;
        }

        public virtual bool EsDiaFeriado(DateTime fecha)
        {
            return FechaEstaEnRangoInicioFin(fecha) && ChequearSiDiaEsFeriado(fecha);
        }

        protected virtual bool FechaEstaEnRangoInicioFin(DateTime fecha)
        {
            bool estaEnRango = true;

            if ((FechaInicio.HasValue && fecha < FechaInicio)
                || (FechaFin.HasValue && fecha > FechaFin))
            {
                estaEnRango = false;
            }

            return estaEnRango;            
        }

        protected abstract bool ChequearSiDiaEsFeriado(DateTime fecha);
    }
}
