using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQFirst
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        static void LambdaSyntax()
        {
            ///
            ///  A character array to store the string Hello World
            ///
            char [] msg = { 'H' , 'e' ,'l','l' ,'o',' ',
                         'W' , 'o' ,'r','l','d' };

            ////
            /// Call the extension method where to filter the array
            ///
            IEnumerable<char> temp =
                msg.Where(( n) => (n !='l'));
            

            //
            // Navigate the temp variable
            // This will spit the string Hello World
            foreach (char ch in temp)
            {
                Console.Write(ch);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        static void ComprehensionSyntax()
        {
            ///
            ///  A character array to store the string Hello World
            ///
            char[] msg = { 'H' , 'e' ,'l','l' ,'o',' ',
                         'W' , 'o' ,'r','l','d' };

            ////
            /// Use the SQL look alike Comprehension Syntax
            /// to Query the character array 
            IEnumerable<char> temp = from n in msg
                                     where n != 'H'
                                     select n.ToString().ToUpper()[0];



            //
            // Navigate the temp variable
            // This will spit the string Hello World
            foreach (char ch in temp)
            {
                Console.Write(ch);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        static void MixedModeSyntax()
        {
            ///
            ///  A character array to store the string Hello World
            ///
            char[] msg = { 'H' , 'e' ,'l','l' ,'o',' ',
                         'W' , 'o' ,'r','l','d' };

            ////
            /// Use the SQL look alike Comprehension Syntax
            /// to Query the character array 
            IEnumerable<char> temp = (from n in msg
                                     where n >= 32
                                          select n).Select((n) => n.ToString().ToUpper()[0]);
   
            



            //
            // Navigate the temp variable
            // This will spit the string Hello World
            foreach (char ch in temp)
            {
                Console.Write(ch);
            }


        }
        static void Main(string[] args)
        {
           // LambdaSyntax();
           ComprehensionSyntax();
           // MixedModeSyntax();
            Console.Read();

        }
    }
}
