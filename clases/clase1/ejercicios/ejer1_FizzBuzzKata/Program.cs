using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_FizzbuzzKata
{
    class Program {

        static void Main(string[] args) {

            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(3, "Fizz");
            dic.Add(5, "Buzz");
            dic.Add(4, "Zarasa");

            new Ejercicio1(100, dic).Correr();            
            Console.ReadKey();
        }
    }
}
