using System;
using System.Threading.Tasks;

namespace PostSharpSample.Multithreading
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // 只有當初建立 Class 的執行續能夠呼叫
            //new ThreadAffineSample().Main();

            // 執行續同步，同一時間只會有一個執行續能夠執行該 Method
            //new ThreadSynchronizedSample().Main();

            // 讀寫鎖
            //new ReadWriteLockSample().Main();

            // 當多執行續併發執行時，被強制改為單一執行續執行
            //new ThreadActor().Main();

            // 當掛上該屬性，代表該 Class 無法被併發執行，若有則拋出異常
            new ThreadUnSafe().Main();

            Console.ReadKey();
        }
    }
}