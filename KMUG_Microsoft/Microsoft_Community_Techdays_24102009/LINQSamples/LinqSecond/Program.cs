using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSecond
{
    
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static void LambdaSyntax()
        {
            //
            //
            //
            //
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay","Al" };

            //
            //
            //
            IEnumerable<string> q = 
                names.Where(n => n.Length ==
               names.OrderBy(n2 => n2.Length).Select(n2 => n2.Length).Last()  );

            //
            // Iterate through the results
            //
            //
            foreach (string str in q)
                Console.WriteLine(str);
        }

        /// <summary>
        /// 
        /// </summary>
        static void ComprehensionSyntax()
        {
            //
            //
            //
            //
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            //
            //
            //
#if false
            IEnumerable<string> q =
                names.Where(n => n.Length ==
               names.OrderBy(n2 => n2.Length).Select(n2 => n2.Length).Last());
#else

            

            IEnumerable<string> q =
                from n in names 
                where n.Length == 
                 ( from n2 in names orderby n2.Length 
                  select n2.Length  ).Last()  
                select n;
 

#endif


            //
            // Iterate through the results
            //
            //
            foreach (string str in q)
                Console.WriteLine(str);
        }

        public static void MixedModeSyntax()
        {
            string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

            IEnumerable<string> q =
                from n in names
                where n.Length ==
                (names.OrderBy(n2 => n2.Length).Select(n2 => n2.Length).First())
                select n;

            foreach (string str in q)
                Console.WriteLine(str);
        }
        static void Main(string[] args)
        {
           // LambdaSyntax();
            //ComprehensionSyntax();
            MixedModeSyntax();
            Console.Read();
        }
    }
}
