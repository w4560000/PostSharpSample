using PostSharp.Patterns.Threading;
using System;
using System.Collections.Generic;

namespace PostSharpSample.Immutable
{
    /// <summary>
    /// /https://doc.postsharp.net/threading/immutable
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = new InvoiceV1(100);
            var b = new InvoiceV2();
            var c = new InvoiceV3();
            var d = new InvoiceV4();

            b.Refresh();
            c.Refresh();
            d.Refresh();

            Console.ReadKey();
        }
    }

    public class InvoiceV1
    {
        public readonly long _id;

        public InvoiceV1(long id)
        {
            SetIdentifier(id);
        }

        private void SetIdentifier(long id)
        {
            // Will cause compilation error.
            //_id = id;
        }
    }

    public class Test
    {
        public string A { get; set; }
        public string B { get; set; }
    }

    public class InvoiceV2
    {
        public readonly Test _test;

        public InvoiceV2()
        {
            _test = new Test();
            _test.A = "A";
            _test.B = "B";
        }

        public void Refresh()
        {
            // 雖然宣告為 readonly 但其 Property 其實仍然可以修改，只是不能異動物件本身而已
            _test.A = "1";
            _test.B = "2";
        }
    }

    public class InvoiceV3
    {
        public readonly IList<Test> _testList;

        public InvoiceV3()
        {
            _testList = new List<Test>
            {
                new Test()
            };
        }

        public void Refresh()
        {
            // 雖然宣告為 readonly 但其 Property 其實仍然可以修改，只是不能異動物件本身而已
            _testList.Add(new Test());
        }
    }

    //[Immutable]
    public class TestV2
    {
        public string A { get; set; }
        public string B { get; set; }
    }

    [Immutable]
    public class InvoiceV4
    {
        public long _id;

        public TestV2 _test { get; set; }

        public InvoiceV4()
        {
            _id = 100;
            _test = new TestV2();
        }

        public void Refresh()
        {
            // 有宣告 Immutable 後，只能在建構子內調整，在其他地方則變為不可變更物件，包含物件本身、其內部屬性
            // 在此會噴錯 PostSharp.Patterns.Threading.ObjectReadOnlyException: 'The object is frozen and cannot be modified.'
            //_id = 1;
            //_test = new TestV2();
            //// 雖然宣告為 readonly 但其 Property 其實仍然可以修改，只是不能異動物件本身而已
            //_test.A = "1";
            //_test.B = "2";
        }
    }
}