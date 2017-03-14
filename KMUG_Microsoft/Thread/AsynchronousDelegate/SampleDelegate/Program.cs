using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SampleDelegate
{
    delegate int ThreadWorkerRoutine( string  t );

    //=====================  9895703359 => hari prasad


    class Program
    {
        
        static int DoSomeWork(string t)
        {
            
            // perform a lengthy computation
            Thread.Sleep(1000);
            return t.Length;

        }

        static void CompletionRoutine(IAsyncResult p)
        {
            ThreadWorkerRoutine mtd = (ThreadWorkerRoutine)p.AsyncState;
            int result = mtd.EndInvoke(p);
            Console.WriteLine(" string length " + result);

        }

        static void Main(string[] args)
        {
            ThreadWorkerRoutine worker = new ThreadWorkerRoutine (DoSomeWork);
            IAsyncResult res = worker.BeginInvoke("Testeeeeeeee", null, null);

            for (int i = 0; i < 10; ++i)
            {
                Console.Write(".");
            }

            int result = worker.EndInvoke(res);
            Console.WriteLine("String length " + result);
            Console.ReadLine();


        }
    }
}
