using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Threading.Tasks;

namespace PostSharpSample.SimpleAspect
{
    public class MethodInterceptionAspectSample
    {
        [RetryOnException]
        public void Main()
        {
            throw new Exception("retry Test");
        }

        [RetryOnException]
        public async Task MainAsync()
        {
            await Task.Yield();

            throw new Exception("retry Test");
        }
    }

    [PSerializable]
    public class RetryOnExceptionAttribute : MethodInterceptionAspect
    {
        public RetryOnExceptionAttribute()
        {
            this.MaxRetries = 3;
        }

        public int MaxRetries { get; set; }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            int retriesCounter = 0;

            while (true)
            {
                try
                {
                    args.Proceed();
                    return;
                }
                catch (Exception e)
                {
                    retriesCounter++;
                    if (retriesCounter > this.MaxRetries) throw;

                    Console.WriteLine(
                        "Exception during attempt {0} of calling method {1}.{2}: {3}",
                        retriesCounter, args.Method.DeclaringType, args.Method.Name, e.Message);
                }
            }
        }

        /// <summary>
        /// 若有非同步需要實作非同步 OnInvokeAsync
        /// </summary>
        public override async Task OnInvokeAsync(MethodInterceptionArgs args)
        {
            int retriesCounter = 0;

            while (true)
            {
                try
                {
                    await args.ProceedAsync();
                    return;
                }
                catch (Exception e)
                {
                    retriesCounter++;
                    if (retriesCounter > this.MaxRetries) throw;

                    Console.WriteLine(
                        "Exception during attempt {0} of calling method {1}.{2}: {3}",
                        retriesCounter, args.Method.DeclaringType, args.Method.Name, e.Message);
                }
            }
        }
    }
}