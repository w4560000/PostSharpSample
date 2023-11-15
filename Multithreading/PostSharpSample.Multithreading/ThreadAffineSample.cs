using PostSharp.Patterns.Threading;
using System;
using System.ComponentModel;
using System.Threading;

namespace PostSharpSample.Multithreading
{
    public class ThreadAffineSample
    {
        public void Main()
        {
            var test = new ThreadAffineTest();

            test.Process(1);

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (sender, args) =>
            {
                try
                {
                    // 拋出異常，因與建立 ThreadAffineTest 的 Thread 不同
                    // 只有當初建立 ThreadAffineTest 的 Thread 才能存取
                    test.Process(2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };
            backgroundWorker.RunWorkerAsync();
        }
    }

    [ThreadAffine]
    public class ThreadAffineTest
    {
        public void Process(int sequence)
        {
            Console.WriteLine("sequence {0}", sequence);
            Console.WriteLine("sleeping for 5s");

            Thread.Sleep(new TimeSpan(0, 0, 5));
        }
    }
}