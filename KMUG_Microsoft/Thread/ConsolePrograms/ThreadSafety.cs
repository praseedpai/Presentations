using System;
using System.Threading;


namespace MonitorDemo
{


class MutableInteger {

  private long value;
  public long get() { return value;}
  public void Set(long i ) { value=i;}
 
}

class SynchronizedInteger {

  private long value;
  private Object obj = new Object();
  public long get() 
   { 
     lock(obj) 
     {
       return value;
    } 
   }
  public void Set(long i ) { 
     lock(obj) 
     { 
       value=i;
     }
}

class Test {

  
  public static void ThreadFunc(object param )
  {

       
       SynchronizedInteger t = param as SynchronizedInteger;
      // lock(t) 
       {
        long x = t.get();
       t.Set(x+1);
       Console.WriteLine("Hello World  " + t.get());
       }
       
  }


  public static void Main()
  {
     try { 
      SynchronizedInteger ins = new SynchronizedInteger();
      for(int i=0;i <10; ++i )
      {

          new Thread(ThreadFunc).Start(ins);
      }
     }
     catch(Exception e ) {

       Console.WriteLine("Exception caught");
     }

  }
}

}
}

