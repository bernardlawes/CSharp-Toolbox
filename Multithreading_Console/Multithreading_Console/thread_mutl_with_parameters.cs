using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    internal class thread_mutl_with_parameters
    {
        public thread_mutl_with_parameters()
        {
            Console.WriteLine("Main Thread Started");
            //Creating Threads

            Thread t1 = new Thread(() =>
                Method1(5)
            )
            { Name = "Thread1" };
            Thread t2 = new Thread(() =>
                Method2(5, 3000)
            )
            { Name = "Thread2" };
            Thread t3 = new Thread(() =>
                Method3(5)
            )
            { Name = "Thread3" };

            //Executing the methods
            t1.Start();
            t2.Start();
            t3.Start();
            Console.WriteLine("Main Thread Ended");
            Console.Read();
        }
        static void Method1(int count)
        {
            Console.WriteLine("Method1 Started using " + Thread.CurrentThread.Name);
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine("Method1 :" + i);
            }
            Console.WriteLine("Method1 Ended using " + Thread.CurrentThread.Name);
        }
        static void Method2(int count, int millisecs)
        {
            Console.WriteLine("Method2 Started using " + Thread.CurrentThread.Name);
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine("Method2 :" + i);
                if (i == 3)
                {
                    Console.WriteLine("Performing the Database Operation Started");
                    //Sleep for 10 seconds
                    Thread.Sleep(millisecs);
                    Console.WriteLine("Performing the Database Operation Completed");
                }
            }
            Console.WriteLine("Method2 Ended using " + Thread.CurrentThread.Name);
        }
        static void Method3(int count)
        {
            Console.WriteLine("Method3 Started using " + Thread.CurrentThread.Name);
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine("Method3 :" + i);
            }
            Console.WriteLine("Method3 Ended using " + Thread.CurrentThread.Name);
        }
    }
}
