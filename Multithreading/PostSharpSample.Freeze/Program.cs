using PostSharp.Patterns.Threading;
using System;

namespace PostSharpSample.Freeze
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var invoice = new Invoice();
            invoice.Id = 123456;

            ((IFreezable)invoice).Freeze();
            invoice.Id = 123;

            Console.ReadKey();
        }
    }

    [Freezable]
    public class Invoice
    {
        public long Id { get; set; }
    }
}
