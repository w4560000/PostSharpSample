using System;
using System.Threading.Tasks;

namespace PostSharpSample.SimpleAspect
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //new OnMethodBoundaryAspectSample().Main();

            var number = new OnExceptionAspectSample().Main();

            Console.ReadKey();
        }
    }
}