using PostSharp.Patterns.Collections;
using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostSharpSample.Aggregatable
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var postSharp = new Invoice()
            {
                Customer = new Customer() { B = "B" },
                Lines = new AdvisableCollection<InvoiceLine>()
                {
                    new InvoiceLine() { Product = new Product() { C = "C1" }, Amount = 100 },
                    new InvoiceLine() { Product = new Product() { C = "C2" }, Amount = 200 }
                },
                DeliveryAddress = new Address() { A = "A" }
            };

            #region 傳統綁定父子關係，需自行綁定

            var parent_object = new ParentObject
            {
                id = 1,
                parent_object_name = "test name"
            };

            parent_object.child_objects = new List<ChildObject>
            {
                new ChildObject
                {
                    id = 1,
                    child_object_name = "test name",
                    parent_object = parent_object // 自行指定父物件
                }
            };

            Console.WriteLine(parent_object.child_objects.First().parent_object.parent_object_name);

            #endregion 傳統綁定父子關係，需自行綁定

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 掛上 Aggregatable 的 Class
    /// 若 Property 有物件型別，則會檢查有沒有掛上 Reference or Child or Parent
    /// 掛上 Reference 代表該物件 不需要自動綁定父子關係
    /// 掛上 Child 代表該物件為子物件，在其子物件中若有定義 Parent 的 Property，則會自動綁定父子關係
    /// </summary>
    [Aggregatable]
    public class Invoice
    {
        /// <summary>
        /// 不綁定父子關係
        /// </summary>
        [Reference]
        public Customer Customer { get; set; }

        /// <summary>
        /// 綁定子關係
        /// </summary>
        [Child]
        public IList<InvoiceLine> Lines { get; set; }

        /// <summary>
        /// 綁定子關係
        /// </summary>
        [Child]
        public Address DeliveryAddress { get; set; }
    }

    //[Aggregatable]
    public class InvoiceLine
    {
        /// <summary>
        /// 不綁定父子關係
        /// </summary>
        [Reference]
        public Product Product { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// 綁定父關係
        /// </summary>
        [Parent]
        public Invoice ParentInvoice { get; private set; }
    }

    [Aggregatable]
    public class Address
    {
        public string A { get; set; }

        /// <summary>
        /// 綁定父關係
        /// </summary>
        [Parent]
        public Invoice ParentInvoice;
    }

    [Aggregatable]
    public class Customer
    {
        public string B { get; set; }

        /// <summary>
        /// 綁定父關係
        /// </summary>
        [Parent]
        public Invoice ParentInvoice { get; private set; }
    }

    public class Product
    {
        public string C { get; set; }
    }
}