using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace PostSharpSample.Multithreading
{
    public class ThreadSynchronizedSample
    {
        public void Main()
        {
            var test = new ThreadSynchronizedTest();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (sender, args) => test.Process(1);
            backgroundWorker.RunWorkerAsync();

            test.Process(2);
        }
    }

    [Synchronized]
    public class ThreadSynchronizedTest
    {
        public void Process(int sequence)
        {
            Console.WriteLine("sequence {0}", sequence);
            Console.WriteLine("sleeping for 5s");

            Thread.Sleep(new TimeSpan(0, 0, 5));
        }
    }
}
