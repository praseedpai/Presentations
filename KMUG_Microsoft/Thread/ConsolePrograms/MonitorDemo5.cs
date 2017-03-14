using System;
using System.Threading;


namespace MonitorDemo
{



class Test {

  private static Object test = new Object();
  public static void ThreadRoutine(object param )
  {

      Monitor.Enter(test);
      try 
      {
           Console.WriteLine((param == null ) ?
		 "Empty":"My string is "+param);
           Console.WriteLine("Hello World");
           throw new Exception("dsdfdf");

      }
     // catch(Exception e ) {

//      }
      //finally {
//     Monitor.Exit(test);
    // }
  }


  public static void Main()
  {
     try { 
      for(int i=0;i <10; ++i )
      {

          new Thread(ThreadRoutine).Start(i);
      }
     }
     catch(Exception e ) {

       Console.WriteLine("Exception caught");
     }

  }
}

}
