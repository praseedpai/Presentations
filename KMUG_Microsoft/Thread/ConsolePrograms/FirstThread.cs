using System;
using System.Threading;


namespace Threads
{
  class FirstThread {

   public static void Main( String[] args ) {

	Thread t = new Thread(ThreadRoutine);
        t.Start();
        t.IsBackground = true; 
        for( int i=0; i <10; ++i ) {

             Console.WriteLine("Main Thread");

        }


   }

   static void ThreadRoutine() {

         for( int i=0; i <10; ++i ) {

             Console.WriteLine("Secondary Thread");

        }

   }

  }





}