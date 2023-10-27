using System;

namespace PostSharpSample.Contracts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var customer = new CustomerModel();
                customer.SetFullName("Leo", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}