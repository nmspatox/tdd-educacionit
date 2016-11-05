using System;

namespace CalendarioKata
{
    public class DiaFeriadoAnual: Feriado
    {
        public int Dia { get; set; }
        public int Mes { get; set; }

        public DiaFeriadoAnual(int dia, int mes)
        {
            Dia = dia;
            Mes = mes;
        }

        public override bool EsDiaFeriado(DateTime fecha)
        {            
            return fecha.Day == Dia && fecha.Month == Mes;
        }
    }
}
