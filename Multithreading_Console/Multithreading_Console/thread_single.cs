using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    internal class thread_single
    {

        public thread_single()
        {
            Method1();
            Method2();
            Method3();
            Console.Read();

        }


        static void Method1()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Method1 :" + i);
            }
        }

        static void Method2()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Method2 :" + i);
                if (i == 3)
                {
                    Console.WriteLine("Performing the Database Operation Started");
                    //Sleep for 3 seconds
                    Thread.Sleep(3000);
                    Console.WriteLine("Performing the Database Operation Completed");
                }
            }
        }
        static void Method3()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Method3 :" + i);
            }
        }
    }
}
