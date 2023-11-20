using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Multithreading
{
    /// <summary>
    /// 微軟原生 讀取鎖
    ///
    /// 讀鎖
    /// EnterReadLock
    /// ExitReadLock
    ///
    /// 寫鎖
    /// EnterWriteLock
    /// ExitWriteLock
    ///
    /// 允許多執行續獲取讀鎖，但限定單一執行續獲得寫鎖
    /// 當只有寫鎖被鎖定時，讀鎖範圍也會被鎖定，當寫入完成，寫鎖解開後，讀鎖範圍才可以讀取
    /// </summary>
    internal class ReadWriteLockSample
    {
        public void Main()
        {
            //Original();

            // 僅有單一執行續獲得寫鎖，佔據寫鎖時，無法讀取
            //PostSharpSample();

            // UpgradeableReader 能確保只有當前執行續能取得寫鎖
            // 且無讀鎖限制，用在需長時間執行寫入方法，會佔據寫鎖，但仍然可以讀
            PostSharpSampleV2();
        }

        /// <summary>
        /// 微軟原生方法
        /// </summary>
        private void Original()
        {
            List<Task> tasks = new List<Task>();
            var test = new ReadWriteLock();
            test.Set(999, 0);
            tasks.Add(Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{test.AmountAfterDiscount}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            }));

            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改    , Time:{DateTime.Now:mm:ss.fff}");
                    test.Set(test.AmountAfterDiscount, i);
                    //Thread.Sleep(10000);
                }
            }));

            Task.WaitAll(tasks.ToArray());
        }

        private void PostSharpSample()
        {
            List<Task> tasks = new List<Task>();
            var test = new ReadWriteLockPostSharp(999, 0);
            tasks.Add(Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{test.AmountAfterDiscount()}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            }));

            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    test.Set(test.AmountAfterDiscount(), i);
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改後    , Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(10000);
                }
            }));

            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    test.Set(test.AmountAfterDiscount(), i);
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改後    , Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(10000);
                }
            }));

            Task.WaitAll(tasks.ToArray());
        }

        private void PostSharpSampleV2()
        {
            List<Task> tasks = new List<Task>();
            var test = new Order();
            var task1 = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{test.Amount}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            });

            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    //Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改前    , Time:{DateTime.Now:mm:ss.fff}");
                    test.Recalculate();
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改後    , Time:{DateTime.Now:mm:ss.fff}");
                    //Thread.Sleep(10000);
                }
            }));

            //Thread.Sleep(5000);

            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    //Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改前    , Time:{DateTime.Now:mm:ss.fff}");
                    test.Recalculate();
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改後    , Time:{DateTime.Now:mm:ss.fff}");
                    //Thread.Sleep(10000);
                }
            }));

            Task.WaitAll(tasks.ToArray());
        }
    }

    #region Original

    public class ReadWriteLock
    {
        private ReaderWriterLockSlim orderLock = new ReaderWriterLockSlim();

        public decimal Amount { get; private set; }
        public decimal Discount { get; private set; }

        public decimal AmountAfterDiscount
        {
            get
            {
                orderLock.EnterReadLock();
                decimal result = this.Amount - this.Discount;
                orderLock.ExitReadLock();
                return result;
            }
        }

        public void Set(decimal amount, decimal discount)
        {
            if (amount < discount)
            {
                throw new InvalidOperationException();
            }

            orderLock.EnterWriteLock();
            Thread.Sleep(3000);
            this.Amount = amount;
            this.Discount = discount;
            orderLock.ExitWriteLock();
        }
    }

    #endregion Original

    #region PostSharpSample

    [ReaderWriterSynchronized]
    public class ReadWriteLockPostSharp
    {
        public decimal Amount { get; private set; }
        public decimal Discount { get; private set; }

        public ReadWriteLockPostSharp(decimal amount, decimal discount)
        {
            this.Amount = amount;
            this.Discount = discount;
        }

        [Reader]
        public decimal AmountAfterDiscount()
        {
            return this.Amount - this.Discount;
        }

        [Writer]
        public void Set(decimal amount, decimal discount)
        {
            if (amount < discount)
            {
                throw new InvalidOperationException();
            }

            Thread.Sleep(3000);
            this.Amount = amount;
            this.Discount = discount;
        }
    }

    #endregion PostSharpSample

    #region PostSharpSampleV2

    [ReaderWriterSynchronized]
    internal class Order
    {
        // Other details skipped for brevity.

        public decimal Amount
        {
            // The [Reader] attribute optional here is optional because the method is a public getter.
            get;

            // The [Writer] attribute is required because, although the method is a setter, this setter is private,
            // therefore is does not acquire write access by default.
            [Writer]
            private set;
        }

        [UpgradeableReader]
        public void Recalculate()
        {
            decimal total = 0;

            for (int i = 1; i < 100; i++)
            {
                total += i;
            }

            Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改中    , Time:{DateTime.Now:mm:ss.fff}");
            Thread.Sleep(3000);
            this.Amount = total;
        }
    }

    #endregion PostSharpSampleV2
} 