using PostSharp.Patterns.Diagnostics;

namespace PostSharpSample.Logging.BusinessLogic
{
    public class QQTest
    {
        [return: NotLogged]
        public static string Test([NotLogged] Test param)
        {
            return param.A + param.B;
        }
    }

    public class Test
    {
        public string A { get; set; }
        public string B { get; set; }
    }
}