using System;
using System.Diagnostics;
using System.Net;

namespace PostSharpSample.AutoRetry
{
    internal class Program
    {
        private const double failureRate = 0.8;
        private static readonly Random random = new Random();
        private static readonly Stopwatch stopwatch = Stopwatch.StartNew();

        static void Main(string[] args)
        {
            DoSomething();
        }

        [AutoRetry(MaxRetries = 10)]
        public static void DoSomething()
        {
            WriteMessage("DoSomething start.");

            if (random.NextDouble() < failureRate)
            {
                WriteMessage("DoSomething failure.");
                throw new WebException();
            }

            WriteMessage("Success!");
        }

        private static void WriteMessage(string message)
        {
            Console.WriteLine("{0} ms - {1}", stopwatch.ElapsedMilliseconds, message);
        }
    }
}
