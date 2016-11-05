using System;

namespace CalendarioKata
{
    public abstract class Feriado
    {        
        public int HsDeTrabajo { get; set; }

        public Feriado()
        {
            HsDeTrabajo = 0;
        }

        public abstract bool EsDiaFeriado(DateTime fecha);
    }
}
