using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapReduce
{
    static class Extension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static IEnumerable<T>
            Maps<T>(this IEnumerable<T> x, Func<T, T> f)
        {
            List<T> n = new List<T>();

            foreach (T t in x)
            {
                n.Add(f(t));

            }

            return n;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="f"></param>
        /// <param name="init"></param>
        /// <returns></returns>
        public static
            T Reduce<T>(this IEnumerable<T> x,
            Func<T, T, T> f, T init)
        {
            T s = init;
            foreach (T t in x)
            {
                s = f(s, t);

            }

            return s;

        }
    }

    class Program
    {
       
        static void Main(string[] args)
        {

            //
            //
            //
            Func<double, double, double> 
                adder = (x, y) => x + y;

            // Declare a data element
            double[] arr = { 10, 20, 30 };

            IEnumerable<double> mapped = 
                arr.Maps<double>( (x) => x * x);

            double n = mapped.Reduce<double>( adder, 0);
            n = mapped.Reduce<double>( (x, y) => x + y,0);
            Console.WriteLine(n);
            Console.Read();
        }
    }
}
