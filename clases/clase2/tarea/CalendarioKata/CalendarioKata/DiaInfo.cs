using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public class DiaInfo
    {
        public int DiaDeLaSemana { get; set; }
        public int HsDeTrabajo { get; set; }

        public DiaInfo(DayOfWeek dia, int hsDeTrabajo): this((int)dia, hsDeTrabajo)
        {
        }

        public DiaInfo(int dia, int hsDeTrabajo)
        {
            DiaDeLaSemana = dia;
            HsDeTrabajo = hsDeTrabajo;
        }

        public bool EsLaboral()
        {
            return HsDeTrabajo > 0;
        }
    }
}
