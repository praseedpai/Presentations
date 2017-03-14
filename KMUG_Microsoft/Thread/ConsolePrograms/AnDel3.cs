using System;
using System.Threading;
using System.Runtime.CompilerServices;


namespace AnonymousDelegateThread
{



  public class Test {


  public static void Main()
  {
       int  x=10;
       
       Thread s = new Thread( delegate() {
            Console.WriteLine(x);
       });

       x++;

       Thread s1 = new Thread( delegate(object r ) {
          Console.WriteLine(r);

       });

       s.Start(); s1.Start(x);
       Console.ReadLine();     


  }
}

}