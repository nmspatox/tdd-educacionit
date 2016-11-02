using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public class DiaFeriado: Feriado
    {
        public int Dia { get; set; }
        public int Mes { get; set; }

        public DiaFeriado(int dia, int mes)
        {
            Dia = dia;
            Mes = mes;
        }

        protected override bool ChequearSiDiaEsFeriado(DateTime fecha)
        {            
            return fecha.Day == Dia && fecha.Month == Mes;
        }
    }
}
