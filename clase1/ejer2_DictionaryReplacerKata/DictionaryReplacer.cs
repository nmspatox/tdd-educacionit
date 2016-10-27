using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ejer2_DictionaryReplacerKata {
    public class DictionaryReplacer {

        public string Replace(string text, Dictionary <string ,string> dic) {
            string result = "";
            if (dic != null && dic.Count > 0) {
                result = text;
                foreach(var key in dic.Keys) {
                    result = result.Replace("$" + key + "$", dic[key]);
                }
            }

            return result;
        }
    }
}
