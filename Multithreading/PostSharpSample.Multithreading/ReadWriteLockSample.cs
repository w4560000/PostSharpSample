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
            // Original();

            PostSharpSample();
        }

        /// <summary>
        /// 微軟原生方法
        /// </summary>
        private void Original()
        {
            List<Task> tasks = new List<Task>();
            var test = new ReadWriteLock();
            test.Set(999, 0);
            var task1 = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{test.AmountAfterDiscount}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            });

            var task2 = Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改    , Time:{DateTime.Now:mm:ss.fff}");
                    test.Set(test.AmountAfterDiscount, i);
                    Thread.Sleep(10000);
                }
            });

            Task.WaitAll(tasks.ToArray());
        }

        private void PostSharpSample()
        {
            List<Task> tasks = new List<Task>();
            var test = new ReadWriteLockPostSharp();
            test.Set(999, 0);
            var task1 = Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 讀取:{test.AmountAfterDiscount()}, Time:{DateTime.Now:mm:ss.fff}");
                    Thread.Sleep(100);
                }
            });

            var task2 = Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Console.WriteLine($"ThreadID: {Thread.CurrentThread.ManagedThreadId}, 修改    , Time:{DateTime.Now:mm:ss.fff}");
                    test.Set(test.AmountAfterDiscount(), i);
                    Thread.Sleep(10000);
                }
            });

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

    [ReaderWriterSynchronized]
    public class ReadWriteLockPostSharp
    {
        public decimal Amount { get; private set; }
        public decimal Discount { get; private set; }

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
}