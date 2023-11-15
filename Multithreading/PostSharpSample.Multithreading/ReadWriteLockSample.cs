using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.Multithreading
{
    /// <summary>
    /// todo
    /// </summary>
    internal class ReadWriteLockSample
    {
        public void Main()
        {
            List<Task> tasks = new List<Task>();
            var test = new ReadWriteLock();
            for (var i = 0; i < 10; i++)
            {
                //tasks.Add(Task.Run(() =>
                //{
                //    for (var i = 0; i < 10000; i++)
                //    {
                //        m.AddBalance();
                //    }
                //}));
            }
            Task.WaitAll(tasks.ToArray());
        }
    }

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
            this.Amount = amount;
            this.Discount = discount;
            orderLock.ExitWriteLock();
        }
    }
}