using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PostSharpSample.SimpleAspect
{
    //[PrintExceptionV1]
    //[PrintExceptionV2(typeof(IOException))] // 可只指定攔截某個 Exception

    public class OnExceptionAspectSample
    {
        [PrintExceptionV1]
        public int Main()
        {
            throw new Exception("test");
            //throw new IOException("qqq");
        }

        [PrintException3]
        public int MainV2()
        {
            throw new Exception("test");
            //throw new IOException("qqq");
        }

        /// <summary>
        /// PrintException4 會將所有類型的Exception 轉換成 Exception 來統一輸出
        /// </summary>
        [PrintException4]
        public int MainV3()
        {
            throw new IOException("qqq");
        }
    }

    /// <summary>
    /// 所有異常攔截
    /// </summary>
    [PSerializable]
    public class PrintExceptionV1 : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine(args.Exception.Message);
        }
    }

    /// <summary>
    /// 特定異常攔截
    /// </summary>
    [PSerializable]
    public class PrintExceptionV2 : OnExceptionAspect
    {
        private Type type;

        public PrintExceptionV2(Type type)
        {
            this.type = type;
        }

        // Method invoked at build time.
        // Should return the type of exceptions to be handled.
        public override Type GetExceptionType(MethodBase method)
        {
            return this.type;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine(args.Exception.Message);
        }
    }

    /// <summary>
    /// 攔截異常後 直接回傳結果
    /// 這樣會隱藏異常 不建議
    /// </summary>
    [PSerializable]
    public class PrintException3 : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine(args.Exception.Message);
            args.FlowBehavior = FlowBehavior.Return;
            args.ReturnValue = -1;
        }
    }

    /// <summary>
    /// 處理Exception
    /// 將所有類型的Exception 轉換成 Exception 來統一輸出
    /// </summary>
    [PSerializable]
    public class PrintException4 : OnExceptionAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            Guid guid = Guid.NewGuid();

            // In a real-world app, we would file the exception in the QA database.
            Trace.WriteLine($"Exception {guid}:");
            Trace.WriteLine(args.Exception.ToString());

            args.FlowBehavior = FlowBehavior.ThrowException;
            args.Exception = new Exception($"The service failed unexpectedly. Please report the incident to the QA team with the id #{guid}.");
        }
    }
}