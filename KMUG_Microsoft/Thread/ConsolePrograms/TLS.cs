using System;
using System.Threading;

class Test {
  LocalDataStoreSlot secSlot = 
   Thread.GetNamedDataSlot("securitylevel");

  int SecurityLevel {
     get {
         object data = Thread.GetData(secSlot);
         return (data == null ) ? 0 : (int) data;

     }

     set {
         Thread.SetData(secSlot,value);

     }

  }


   public static void Main() {
      

   }

}
