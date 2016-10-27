using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1_FizzbuzzKata
{
    public class FizzBuzz {

        private Dictionary<int, string> _dic;

        public FizzBuzz() {
            _dic = new Dictionary<int, string>();
            _dic.Add(3, "Fizz");
            _dic.Add(5, "Buzz");
        }

        public string Get(int i) {
            string result = "";
            foreach (var num in _dic.Keys) {
                if (i % num == 0) {
                    result += _dic[num];
                }
            }
            if (result != "")
                return result;
            else
                return i.ToString();
        }
    }
}
