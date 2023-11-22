using System;
using System.Threading.Tasks;

namespace PostSharpSample.SimpleAspect
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            new OnMethodBoundaryAspectSample().Main();

            Console.ReadKey();
        }
    }
}