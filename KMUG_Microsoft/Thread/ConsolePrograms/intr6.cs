using System;
using System.Threading;


class Test {
  static void Main() {
     Thread t = new Thread( delegate() {
            try {
                Thread.Sleep(Timeout.Infinite);
                

            }
            catch( ThreadInterruptedException e )
            {

                Console.WriteLine("forced termination");
                

            }

         Console.WriteLine("!!!!!!!!!!!!!!");

     } );

    t.Start();
    t.Interrupt();

  }


}
