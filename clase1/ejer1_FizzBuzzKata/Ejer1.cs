using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_FizzbuzzKata {
    public class Ejercicio1 {

        private int _count = 0;
        private Dictionary<int, string> _dic;

        public Ejercicio1(int count, Dictionary<int, string> numeros) {
            _count = count;
            _dic = numeros;
        }

        public void Correr() {

            for (var i = 1; i <= _count; i++) {
                string result = "";
                foreach (var num in _dic.Keys) {
                    if (i % num == 0) {
                        result += _dic[num];
                    }
                }
                if (result != "")
                    Console.WriteLine(result);
                else
                    Console.WriteLine(i);

                // Forma 1
                //if (i % 3 == 0 && i % 5 == 0) {
                //    Console.WriteLine(dic[3] + dic[5]);
                //}
                //else if (i % 3 == 0) {
                //    Console.WriteLine(dic[3]);
                //} 
                //else if (i % 5 == 0) {
                //    Console.WriteLine(dic[5]);
                //} else {
                //    Console.WriteLine(i);
                //}
            }
        }
    }
}
