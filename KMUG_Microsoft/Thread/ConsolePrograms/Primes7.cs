using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Primes
{

  class BigInteger {
    public long ProbablePrime() {
        Thread.Sleep(10);
        return new Random().Next(0,10000000); 
    }


  }
 
  class PrimeGenerator {

   private volatile bool _cancel = false;
   List<long> lst = new List<long>();

   public bool Cancel {


     get {

        return _cancel;

     } 
     set {

        _cancel = value;
     }

   }

   public List<long> get() {

        return lst;
   }

   public PrimeGenerator() {  }

   public void Run() { 
        new Thread(ComputePrimes).Start(); 
   }

   public void ComputePrimes() {
        BigInteger bi = new BigInteger();
        while (!_cancel ) {
           lock(lst) {
               lst.Add(bi.ProbablePrime());
           }
           
           

       }
       Thread.CurrentThread.Abort();


   }


   public static void Main() {

       
      PrimeGenerator p = new PrimeGenerator();
      p.Run();
      Thread.Sleep(1000);
      p.Cancel = true;
      Thread.Sleep(100);
      IList<long> s = p.get();
      foreach( long x in s ) {

           Console.WriteLine(x);
      } 

      Console.ReadLine();
   }


  }













}