using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ejer1 {
    class Program {


        static void Main(string[] args) {

            //Dictionary<int, string> dic = new Dictionary<int, string>();
            //dic.Add(3, "Fizz");
            //dic.Add(5, "Buzz");
            //dic.Add(4, "Zarasa");

            //new Ejer1(100, dic).Correr();
            Console.WriteLine(new FizzBuzz().Get(2));
            Console.WriteLine(new FizzBuzz().Get(3));
            Console.WriteLine(new FizzBuzz().Get(5));

            //for (var i = 1; i <= count; i++) {
            //    string result = "";
            //    foreach(var num in dic.Keys) {
            //        if (i % num == 0) {
            //            result += dic[num];
            //        }
            //    }
            //    if(result != "")
            //        Console.WriteLine(result);
            //    else
            //        Console.WriteLine(i);

            //    // Forma 1
            //    //if (i % 3 == 0 && i % 5 == 0) {
            //    //    Console.WriteLine(dic[3] + dic[5]);
            //    //}
            //    //else if (i % 3 == 0) {
            //    //    Console.WriteLine(dic[3]);
            //    //} 
            //    //else if (i % 5 == 0) {
            //    //    Console.WriteLine(dic[5]);
            //    //} else {
            //    //    Console.WriteLine(i);
            //    //}
            //}

            Console.ReadKey();
        }
    }
}
