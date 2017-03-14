using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Parellelism
{

    public class CountDownLatch
    {
        private int m_remain;
        private EventWaitHandle m_event;

        public CountDownLatch(int count)
        {
            m_remain = count;
            m_event = new ManualResetEvent(false);



        }

        public void Signal()
        {
            if (Interlocked.Decrement(ref m_remain) == 0)
            {
                
                m_event.Set();
            }

        }

        public void Wait()
        {

            m_event.WaitOne();

        }

    }

    class CustomParellel
    {
        public static void ForAll<T>(IList<T> data, Action<T> a, int p)
        {
            ForAll<T>(data, 0, data.Count, a, null, p);

        }

        public static void ForAll(int from, int to, Action<int> a, int p)
        {
            ForAll<int>(null, from, to, null, a, p);
        }

        private static void ForAll<T>(IList<T> data, int from, int to,
            Action<T> a0, Action<int> a1, int p)
        {
            int size = to - from;
            int stride = (size + p - 1) / p;
            CountDownLatch latch = new CountDownLatch(p);

            for (int i = 0; i < p; i++)
            {
                int idx = i;
                
                ThreadPool.QueueUserWorkItem (delegate {
                    int end = Math.Min(size, stride * (idx + 1));
                    for (int j = stride * idx; j < end; j++)
                    {
                        if (data != null)
                            a0(data[j]);
                        else
                            a1(j);

                    }
                    latch.Signal();
                } );


            }

            latch.Wait();   // Wait for Parellel threads to finsih
        }




    }
    class Program
    {
        static void Main(string[] args)
        {
            CustomParellel.ForAll(0, 10, delegate(int i) { Console.WriteLine(i); },
                Environment.ProcessorCount);
            Console.ReadLine();
        }
    }
}
