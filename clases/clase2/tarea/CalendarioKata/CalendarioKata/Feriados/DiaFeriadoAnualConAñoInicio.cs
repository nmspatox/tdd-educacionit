using System;

namespace CalendarioKata
{
    public class DiaFeriadoAnualConAñoInicio: Feriado
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int AñoInicio { get; set; }

        public DiaFeriadoAnualConAñoInicio(int dia, int mes, int añoInicio)
        {
            Dia = dia;
            Mes = mes;
            AñoInicio = añoInicio;
        }

        public override bool EsDiaFeriado(DateTime fecha)
        {            
            return fecha.Year >= AñoInicio && fecha.Day == Dia && fecha.Month == Mes;
        }
    }
}
