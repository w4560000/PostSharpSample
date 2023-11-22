using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Multithreading
{
    /// <summary>
    /// UnsafeTest
    /// </summary>
    public class ThreadUnSafe
    {
        public void Main()
        {
            MuiltpleThread();
        }

        /// <summary>
        /// 多執行續
        /// </summary>
        public void MuiltpleThread()
        {
            List<Task> tasks = new List<Task>();
            var test = new UnsafeTest();
            tasks.Add(Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{await test.GetAverage()}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        await test.AddSample(i, "T1");
                        //Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, T1修改後    , Time:{DateTime.Now:mm:ss.fff}");
                        //Thread.Sleep(10000);
                    }
                }
                catch(Exception ex)
                {
                    // 掛上 ThreadUnsafe 的 Class，被多執行續執行時 會噴異常錯誤
                }
            }));

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        await test.AddSample(i, "T2");
                        //Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, T2修改後    , Time:{DateTime.Now:mm:ss.fff}");
                        //Thread.Sleep(10000);
                    }
                }
                catch (Exception ex)
                {

                }
            }));

            Task.WaitAll(tasks.ToArray());
        }
    }

    [ThreadUnsafe]
    public class UnsafeTest
    {
        private float sum;
        private int count;

        public async Task AddSample(float n, string thread)
        {
            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, {thread} 修改中    , Time:{DateTime.Now:mm:ss.fff}");
            Thread.Sleep(3000);
            this.count++;
            this.sum += n;
        }

        public async Task<float> GetAverage()
        {
            return this.sum / this.count;
        }
    }
}