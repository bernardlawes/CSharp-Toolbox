using System.Threading;
using System;
using System.Threading;
using MultiThreading;

namespace ThreadingDemo
{


    class Program
    {



        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Multi-Threading with Parameters");

            Thread t = Thread.CurrentThread;
            //By Default, the Thread does not have any name
            //if you want then you can provide the name explicitly
            t.Name = "Main Thread";
            Console.WriteLine("Current Executing Thread Name :" + t.Name);
            Console.WriteLine("Current Executing Thread Name :" + Thread.CurrentThread.Name);

            //single_thread st = new thread_single();
            //multi_thread mt = new thread_multi();
            thread_mutl_with_parameters mtp = new thread_mutl_with_parameters();
        }
    }
}