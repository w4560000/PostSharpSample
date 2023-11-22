using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PostSharpSample.SimpleAspect
{
    public class OnMethodBoundaryAspectSample
    {
        public void Main()
        {
            //var txt = new OnMethodBoundaryAspectSample().Main(null);
            //var txt = new OnMethodBoundaryAspectSample().Main("Test");

            //var task = new OnMethodBoundaryAspectSample().TestProfiling();
            //var task1 = Task.Run(() => Thread.Sleep(1000));
            //Task.WaitAll(task, task1);

            foreach (var i in Foo())
            {
                Console.WriteLine($"# received {i}");
            }
        }

        //[LoggingAspect]
        //[LoggingAspectV2]
        //[LoggingAspectV3]
        //[LoggingAspectV4]
        [LoggingAspectV5]
        public string Test(string txt)
        {
            Console.WriteLine(txt);
            //throw new Exception("error"); 模擬異常流程
            return "123";
        }

        [LoggingAspectV6]
        public async Task TestProfiling()
        {
            await Task.Delay(3000);
            Thread.Sleep(1000);
        }

        [LoggingAspectV7]
        private static IEnumerable<int> Foo()
        {
            Console.WriteLine("@ part 1");
            yield return 1;
            Console.WriteLine("@ part 2");
            yield return 2;
            Console.WriteLine("@ part 3");
            yield return 3;
            Console.WriteLine("@ part 4");
        }
    }

    /// <summary>
    /// 標準用法
    /// </summary>
    [PSerializable]
    public class LoggingAspect : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Thread.Sleep(1000);
            Console.WriteLine("The {0} method has been entered.", args.Method.Name);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("The {0} method executed successfully.", args.Method.Name);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("The {0} method has exited.", args.Method.Name);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            Console.WriteLine("An exception was thrown in {0}.", args.Method.Name);
        }
    }

    /// <summary>
    /// 取得參數
    /// </summary>
    [PSerializable]
    public class LoggingAspectV2 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("Method {0}({1}) started.", args.Method.Name, string.Join(", ", args.Arguments));
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("Method {0}({1}) returned {2}.", args.Method.Name, string.Join(", ", args.Arguments), args.ReturnValue);
        }
    }

    /// <summary>
    /// 防呆判斷 並直接中斷流程回傳資料
    /// </summary>
    [PSerializable]
    public class LoggingAspectV3 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (args.Arguments.Count > 0 && args.Arguments[0] == null)
            {
                args.FlowBehavior = FlowBehavior.Return;
                args.ReturnValue = "999";
            }

            Console.WriteLine("The {0} method was entered with the parameter values: {1}",
                              args.Method.Name, string.Join(", ", args.Arguments));
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("Method {0}({1}) returned {2}.", args.Method.Name, string.Join(", ", args.Arguments), args.ReturnValue);
        }
    }

    /// <summary>
    /// 攔截異常流程
    /// </summary>
    [PSerializable]
    public class LoggingAspectV4 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            var ex = args.Exception;
            Console.WriteLine("An exception was thrown in {0}.", args.Method.Name);
        }
    }

    /// <summary>
    /// 共享資訊 可用來計算方法前後花費時間
    /// </summary>
    [PSerializable]
    public class LoggingAspectV5 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();

            Console.WriteLine($"{args.Method.Name} executed in {sw.ElapsedMilliseconds} mileseconds");
        }
    }

    /// <summary>
    /// 非同步
    ///
    /// OnYield = await 未完成時
    /// OnResume = await 完成時
    /// </summary>
    [PSerializable]
    public class LoggingAspectV6 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();

            Console.WriteLine("{0} executed in {1} mileseconds", args.Method.Name, sw.ElapsedMilliseconds);
        }

        public override void OnYield(MethodExecutionArgs args)
        {
            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
        }

        public override void OnResume(MethodExecutionArgs args)
        {
            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
            sw.Start();
        }
    }

    /// <summary>
    /// 疊代器測試
    /// </summary>
    [PSerializable]
    internal class LoggingAspectV7 : PostSharp.Aspects.OnMethodBoundaryAspect
    {
        /// <summary>
        /// 方法開始前
        /// </summary>
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine("! entry");
        }

        /// <summary>
        /// MoveNext() 之後
        /// </summary>
        /// <param name="args"></param>
        public override void OnResume(MethodExecutionArgs args)
        {
            Console.WriteLine("! resume");
        }

        /// <summary>
        /// yield return 之前
        /// </summary>
        public override void OnYield(MethodExecutionArgs args)
        {
            Console.WriteLine($"! yield return {args.YieldValue}");
        }

        /// <summary>
        /// 執行成功
        /// </summary>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            Console.WriteLine("! success");
        }

        /// <summary>
        /// 執行結束時 (不論成功和失敗)
        /// </summary>
        /// <param name="args"></param>
        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine("! exit");
        }
    }
}