using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public class Calendario
    {
        private DiaInfo[] _diasSemana;

        public int DiasCargados {
            get {
                return _diasSemana.Count(x => x != null);
            }
        }        

        public Calendario()
        {
            _diasSemana = new DiaInfo[7]; 

            for(int i=0; i < 7; i++)
            {
                _diasSemana[i] = null;
            }
        }

        public void CargarDia(DiaInfo dia)
        {
            ValidarSiYaExiste(dia.DiaDeLaSemana);
            ValidarDiaDeLaSemana(dia.DiaDeLaSemana);            
            ValidarHsDeTrabajo(dia.HsDeTrabajo);

            _diasSemana[dia.DiaDeLaSemana - 1] = dia;
        }
        
        public bool EsDiaLaboral(DateTime fecha)
        {
            var dia = GetDia(fecha);
            return dia != null && dia.EsLaboral();
        }

        public int HsDeTrabajo(DateTime fecha)
        {
            var dia = GetDia(fecha);
            return dia.HsDeTrabajo;
        }

        public DiaInfo GetDia(int diaDeLaSemana)
        {
            ValidarDiaDeLaSemana(diaDeLaSemana);

            return _diasSemana[diaDeLaSemana - 1];
        }

        private DiaInfo GetDia(DateTime fecha)
        {
            return _diasSemana.SingleOrDefault(x => x.DiaDeLaSemana == (int)fecha.DayOfWeek + 1);
        }

        private void ValidarSiYaExiste(int diaDeLaSemana)
        {
            if (GetDia(diaDeLaSemana) != null)
            {
                throw new DiaYaCargadoException();
            }
        }

        private void ValidarHsDeTrabajo(int hsDeTrabajo)
        {
            if (hsDeTrabajo < 0 || hsDeTrabajo > 24)
            {
                throw new HsDeTrabajoNoValidasException();
            }
        }

        private void ValidarDiaDeLaSemana(int diaDeLaSemana)
        {
            if (diaDeLaSemana < 1 || diaDeLaSemana > 7)
            {
                throw new DiaNoValidoException();
            }
        }

    }
}
