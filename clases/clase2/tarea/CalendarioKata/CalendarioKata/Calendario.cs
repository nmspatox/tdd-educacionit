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

        private Dictionary<int, DiaInfo> _diasLaborales;

        public Dictionary<int, DiaInfo> DiasLaborales { get; private set; }

        public int DiasCargados {
            get {
                return _diasLaborales.Count();
            }
        }

        public IList<Feriado> Feriados { get; set; }

        public CalculadoraHsTrabajo CalculadoraHs { get; set; }

        #endregion

        #region Constructor

        public Calendario(IEnumerable<DiaInfo> diasLaborales, CalculadoraHsTrabajo calculadoraHs = null)
        {            
            if (diasLaborales.Count() == 0)
            {
                throw new CalendarioSinDiasException();
            }

            if (diasLaborales.Any(x=> x == null))
            {
                throw new CalendarioConDiasNoValidosException();
            }

            _diasLaborales = new Dictionary<int, DiaInfo>();
            foreach(var dia in diasLaborales)
            {                
                ValidarSiYaExiste(dia.DiaDeLaSemana);
                ValidarDiaDeLaSemana(dia.DiaDeLaSemana);
                ValidarHsDeTrabajo(dia.HsDeTrabajo);

                _diasLaborales.Add(dia.DiaDeLaSemana, dia);
            }

            DiasLaborales = _diasLaborales;

            Feriados = new List<Feriado>();
            CalculadoraHs = calculadoraHs ?? new CalculadoraNormalHsTrabajo();            
        }

        #endregion

        #region Metodos Publicos

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
            DiaInfo result = null;
            _diasLaborales.TryGetValue(diaDeLaSemana, out result);
            return result;
        }        

        public bool EsDiaFeriado(DateTime fecha)
        {
            foreach(var feriado in Feriados)
            {
                if (feriado.EsDiaFeriado(fecha))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Metodos Auxiliares

        private DiaInfo GetDia(DateTime fecha)
        {            
            return GetDia((int)fecha.DayOfWeek);
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
