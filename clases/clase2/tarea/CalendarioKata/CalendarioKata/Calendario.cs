using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarioKata
{
    public class Calendario
    {
        #region Fields y Properties

        private DiaInfo[] _diasSemana;

        public int DiasCargados {
            get {
                return _diasSemana.Count(x => x != null);
            }
        }

        public IList<Feriado> Feriados { get; set; }

        public CalculadoraHsTrabajo CalculadoraHs { get; set; }

        #endregion

        #region Constructor

        public Calendario():this(new CalculadoraNormalHsTrabajo())
        {

        }

        public Calendario(CalculadoraHsTrabajo calculadoraHs)
        {
            _diasSemana = new DiaInfo[7];
            Feriados = new List<Feriado>();

            CalculadoraHs = calculadoraHs;

            for(int i=0; i < 7; i++)
            {
                _diasSemana[i] = null;
            }
        }

        #endregion

        #region Metodos Publicos

        public void CargarDia(DiaInfo dia)
        {
            ValidarSiYaExiste(dia.DiaDeLaSemana);
            ValidarDiaDeLaSemana(dia.DiaDeLaSemana);
            ValidarHsDeTrabajo(dia.HsDeTrabajo);

            _diasSemana[dia.DiaDeLaSemana] = dia;
        }

        public bool EsDiaLaboral(DateTime fecha)
        {
            var dia = GetDia(fecha);
            return dia != null && dia.EsLaboral();
        }

        public int HsDeTrabajo(DateTime fecha)
        {
            return CalculadoraHs.CalcularHsTrabajo(fecha, GetDia(fecha), Feriados);
        }

        public DiaInfo GetDia(int diaDeLaSemana)
        {
            ValidarDiaDeLaSemana(diaDeLaSemana);

            return _diasSemana[diaDeLaSemana];
        }

        public bool EsDiaFeriado(DateTime fecha)
        {
            foreach(var feriado in Feriados)
            {
                if (feriado.EsDiaFeriado(fecha))
                    return true;
            }

            return false;
        }

        #endregion

        #region Metodos Auxiliares

        private DiaInfo GetDia(DateTime fecha)
        {            
            return _diasSemana.SingleOrDefault(x => x.DiaDeLaSemana == (int)fecha.DayOfWeek);
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
            if (diaDeLaSemana < 0 || diaDeLaSemana > 6)
            {
                throw new DiaNoValidoException();
            }
        }

        #endregion
    }
}
