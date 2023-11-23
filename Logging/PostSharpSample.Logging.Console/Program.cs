using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.Console;
using PostSharpSample.Logging.BusinessLogic;

// Add logging to all methods of the current project.
[assembly: Log]

namespace PostSharpSample.Logging.Console
{
    /// <summary>
    /// https://doc.postsharp.net/logging
    /// </summary>
    [Log(AttributeExclude = false)]   // Removes logging from the Program class itself.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configure PostSharp Logging to output logs to the console.
            LoggingServices.DefaultBackend = new ConsoleLoggingBackend();

            Test("999");
            QQTest.Test(new Test() { A = "123", B = "456" });

            // Simulate some business logic.
            QueueProcessor.ProcessQueue(@".\Private$\SyncRequestQueue");
        }

        public static string Test(string param)
        {
            return param + "456";
        }
    }
}