using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public class CalculadoraNormalHsTrabajo : CalculadoraHsTrabajo
    {
        public override int CalcularHsTrabajo(DateTime fecha, DiaInfo dia, IList<Feriado> feriados)
        {
            int hs = dia.HsDeTrabajo;

            if (feriados != null)
            {
                bool continuar = true;
                int i = 0;
                while(continuar && i < feriados.Count)
                {
                    if (feriados[i].EsDiaFeriado(fecha))
                    {
                        hs = feriados[i].HsDeTrabajo;
                        continuar = false;
                    }
                    i++;
                }
            }

            return hs;
        }
    }
}
