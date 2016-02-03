using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LambdaDemo
{
    class Program
    {
        delegate int Binop(int x, int y);

        static void Main(string[] args)
        {
            Binop adder = ( int  x, int  y) => (x + y);
            Binop mul   = ( int x , int y ) => (x*y);
            int rs = adder(1,2);

            Console.WriteLine(rs);
            rs = mul(4, 5);
            Console.WriteLine(rs);

                        Binop test =  delegate(int x, int y)
            {
                return x + y;
            };
            Console.WriteLine( test(2,3));

            Expression<Func<int, int, int>> f = (x, y) => x + y;
            Console.WriteLine(f.Compile().Invoke (2, 3));
            Console.WriteLine(f.Body.ToString());
            Console.WriteLine(f.Parameters[0].ToString());
            Console.Read();



        }
    }
}
