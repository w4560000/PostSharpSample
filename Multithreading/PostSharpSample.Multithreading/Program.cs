using System;

namespace PostSharpSample.Multithreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new ThreadAffineSample().Main();

            new ThreadSynchronizedSample().Main();

            Console.ReadKey();
        }
    }
}
