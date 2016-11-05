using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ejer1_StringDictionary
{
    public class StringDictionary
    {        
        public int Add(string str)
        {
            // Antes que nada chequeamos si el string es nulo
            // Si fuese así lanzamos una excepción de tipo NullStringException
            if (str == null)
            {
                throw new NullStringException();
            }

            int sum = 0;            

            // Si el string está vacío no hacemos nada
            if (!string.IsNullOrEmpty(str))
            {
                // Estos son los delimitadores por default
                string[] delimiters = new string[] { ",", "\n" };
                string customDelimiter = null;

                // Intentamos obtener un delimitador custom del string                
                if (TryGetCustomDelimiter(str, out customDelimiter))
                {                    
                    str = str.Substring(str.IndexOf('\n') + 1);
                    delimiters = new string[] { customDelimiter };                    
                }

                // Chequeamos que alguno de los delimitadores exista en el string
                // Si no existe ninguno intentamos obtener un numero del string
                if (delimiters.Any(x => str.IndexOf(x) > -1))
                {
                    sum = IterateDelimiters(str, delimiters);
                }
                else
                {
                    sum = GetNumber(str);
                }
            }            

            return sum;
        }

        private bool TryGetCustomDelimiter(string str, out string delimiter)
        {
            string delimiterStart = "//";
            string delimiterEnd = "\n";
            int delimiterEndIdx = str.IndexOf(delimiterEnd);

            delimiter = null;
            bool result = false;

            // La expresión regular captura la porción del string que esté entre
            // los caracteres de principio y fin de delimitador custom 
            var match = Regex.Match(str, "^" + delimiterStart + "(.+)" + delimiterEnd);
            if (match.Groups.Count > 1)
            {
                delimiter = match.Groups[1].Value;
                result = true;
            }

            return result;
        }

        private int IterateDelimiters(string str, string[] delimiters)
        {
            int sum = 0;
            bool doContinue = true;
            int i = 0;

            while(doContinue && i < delimiters.Length)
            {
                // Chequeamos que el string tenga el delimitador
                if (str.IndexOf(delimiters[i]) > -1)
                {
                    // Si existe hacemos la suma y ya no iteramos los demás
                    // delimitadores (si los hubiese)
                    sum += Add(str, delimiters[i].ToString());
                    doContinue = false;
                }
                i++;
            }

            return sum;
        }

        private int Add(string str, string delimiter)
        {
            // Esta sobrecarga de Split recibe un array de string que nos sirve para los
            // delimitadores custom que tengan mas de un caracter
            string[] arr = str.Split(new string[] { delimiter }, StringSplitOptions.None);
            int sum = 0;

            for (int j = 0; j < arr.Length; j++)
            {
                sum += GetNumber(arr[j]);
            }

            return sum;
        }

        private int GetNumber(string str)
        {
            int num = 0;

            // TryParse devuelve true si logra parsear el string a un int
            // Si devuelve false lanzamos una excepcion de tipo NotANumberException
            if (!int.TryParse(str, out num))
            {
                throw new NotANumberException();
            }

            return num;
        }
    }
}
