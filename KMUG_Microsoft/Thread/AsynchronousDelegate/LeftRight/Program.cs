using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LeftRight
{
    class LeftRightDeadLock
    {
        private Object left = new Object();
        private Object right = new Object();

        public void leftRight()
        {

            lock (left)
            {
                Thread.Sleep(0);
                lock (right)
                {

                    doSomething();

                }

            }

        }

        public void rightLeft()
        {

            lock (right)
            {
                lock (left)
                {

                    doSomething2();

                }

            }

        }

        void doSomething2()
        {

            Console.WriteLine("Do something 2 ");

        }
        void doSomething()
        {

            Console.WriteLine("Do something 1 ");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            LeftRightDeadLock lrd = new LeftRightDeadLock();

            for (int i = 0; i < 10; ++i)
            {
                if (i % 2 == 0)
                    new Thread(lrd.leftRight).Start();
                else
                    new Thread(lrd.rightLeft).Start();


            }
        }
    }
}
