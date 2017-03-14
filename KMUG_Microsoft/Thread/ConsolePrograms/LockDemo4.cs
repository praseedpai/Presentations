using System;
using System.Threading;
using System.Runtime.CompilerServices;


namespace LockDemo
{



class Test {

  private static Object test = new Object();
  public static void ThreadRoutine(object param )
  {

      lock(test)
      {
           Console.WriteLine((param == null ) ?
		 "Empty":"My string is "+param);
           Console.WriteLine("Hello World");

      }
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public static void ThreadRoutine2(object param )
  {

      
                 Console.WriteLine((param == null ) ?
		 "Empty":"My string is "+param);
           Console.WriteLine("Hello World");

        }


  public static void Main()
  {
      for(int i=0;i <10; ++i )
      {

          new Thread(ThreadRoutine2).Start(i);
      }

  }
}

}
