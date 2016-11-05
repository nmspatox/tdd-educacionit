using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_ProjectApp
{
    public class Proyecto
    {
        public string Nombre { get; set; }

        public Proyecto(string nombre, Calendario calendario)
        {
            if (nombre == null || nombre == "")
            {
                throw new NombreVacioException();
            }

            if (calendario == null)
            {
                throw new CalendarioVacioException();
            }

            Nombre = nombre;
        }
    }
}