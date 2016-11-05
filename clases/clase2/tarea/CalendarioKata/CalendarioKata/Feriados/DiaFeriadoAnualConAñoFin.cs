using System;

namespace CalendarioKata
{
    public class DiaFeriadoAnualConAñoFin: Feriado
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int AñoFin { get; set; }

        public DiaFeriadoAnualConAñoFin(int dia, int mes, int añoFin)
        {
            Dia = dia;
            Mes = mes;
            AñoFin = añoFin;
        }

        public override bool EsDiaFeriado(DateTime fecha)
        {            
            return fecha.Year <= AñoFin && fecha.Day == Dia && fecha.Month == Mes;
        }
    }
}
