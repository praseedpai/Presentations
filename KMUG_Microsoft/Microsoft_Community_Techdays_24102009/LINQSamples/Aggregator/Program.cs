using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aggregator
{
    class Program
    {
        public static T Agg<T>(IEnumerable<T> x, Func<T, T, T> f, T init)
        {
            T s = init;
            foreach (T t in x)
            {
                s = f(s, t);

            }
            return s;
        }


        public static void Adder()
        {
            Func<double, double, double> adder = (x, y) => x + y;

            double[] arr = { 10, 20, 30 };
            double n = Agg<double>(arr, adder, 0);
            n = Agg<double>(arr, (x, y) => (x + y), 0);
            Console.WriteLine(n);



        }

        public static void Greatest()
        {
            Func<double, double, double> comparer = (x, y) => { return (x > y) ? x : y; };
            double[] arr = { 10, 20, 30 };
            double n = Agg<double>(arr, comparer, -100);
            Console.WriteLine(n);

        }

        public static double MulAll(List<double> ls)
        {
            return Agg<double>(ls, (x, y) => x * y, 1.0);
        }



      

        static void Main(string[] args)
        {
            Adder();
            Greatest();
            Console.WriteLine(MulAll(new List<double>() { 10, 10 }));
            Console.Read();
        }
    }
}
