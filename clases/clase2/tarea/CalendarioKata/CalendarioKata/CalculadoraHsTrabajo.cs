using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public abstract class CalculadoraHsTrabajo
    {
        public abstract int CalcularHsTrabajo(DateTime fecha, DiaInfo dia, IList<Feriado> feriados);
    }
}
