using PostSharp.Patterns.Caching.Backends;
using PostSharp.Patterns.Caching;
using System;
using System.Threading;
using System.Diagnostics;

namespace PostSharpSample.Caching
{
    /// <summary>
    /// 快取
    /// 
    /// https://doc.postsharp.net/caching/caching-getting-started
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            CachingServices.DefaultBackend = new MemoryCachingBackend();

            // 可禁用緩存
            //CachingServices.Profiles.Default.IsEnabled = false;

            // 控制快取時間
            //CachingServices.Profiles["Test"].AbsoluteExpiration = TimeSpan.FromSeconds(10);

            var sw = new Stopwatch();
            sw.Start();
            while(true)
            {
                Console.WriteLine("Retrieved: " + GetNumber(1) + $" ,{sw.ElapsedMilliseconds / 1000}");
                Console.WriteLine("Retrieved: " + Test.GetString("1") + $" ,{sw.ElapsedMilliseconds / 1000}");
                Thread.Sleep(1000);

                if (sw.ElapsedMilliseconds / 1000 > 10)
                    ClearCache(1);
            }
            Console.WriteLine("Retrieving value of 1 for the 1st time should hit the database.");
            Console.WriteLine("Retrieved: " + GetNumber(1));

            Console.WriteLine("Retrieving value of 1 for the 2nd time should NOT hit the database.");
            Console.WriteLine("Retrieved: " + GetNumber(1));

            Console.WriteLine("Retrieving value of 2 for the 1st time should hit the database.");
            Console.WriteLine("Retrieved: " + GetNumber(2));

            Console.WriteLine("Retrieving value of 2 for the 2nd time should NOT hit the database.");
            Console.WriteLine("Retrieved: " + GetNumber(2));

            Console.ReadKey();
        }

        [Cache(AbsoluteExpiration = 1)]
        static int GetNumber(int id)
        {
            Console.WriteLine($">> Retrieving {id} from the database...");
            Thread.Sleep(1000);
            return id;
        }

        [InvalidateCache(nameof(GetNumber))]
        static void ClearCache(int id)
        {

        }

    }

    [CacheConfiguration(ProfileName = "Test")]
    public static class Test
    {
        [Cache]
        public static string GetString(string id)
        {
            Console.WriteLine($">> Retrieving {id} from the database... GetString");
            Thread.Sleep(1000);
            return id;
        }
    }
}
