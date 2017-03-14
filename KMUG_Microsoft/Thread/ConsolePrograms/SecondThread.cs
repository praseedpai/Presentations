using System;
using System.Threading;


namespace Threads
{
  class SecondThread {

   public static void Main( String[] args ) {

	ThreadPool.QueueUserWorkItem(ThreadRoutine);
        
        ThreadPool.QueueUserWorkItem(ThreadRoutine,10);  
        Console.ReadLine();

   }

   static void ThreadRoutine(object obj) {

         for( int i=0; i <10; ++i ) {

             Console.WriteLine("Secondary Thread"+obj);

        }

   }

  }


}

