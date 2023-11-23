using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.ComponentModel;

namespace PostSharpSample.SimpleAspect
{
    /// <summary>
    /// 攔截 Property and Field Getter & Setter
    /// 
    /// https://doc.postsharp.net/custompatterns/aspects/tutorials/location-interception
    /// </summary>
    public class LocationInterceptionAspectSample
    {
        public void Main()
        {
            //TestPropertyFiledGetSet();
            //TestPropertyChanged();
            TestReport();
        }

        private void TestPropertyFiledGetSet()
        {
            var customer = new Customer("123 Main Street");
            customer.Name = null;
            //customer.Name = "NewName";

            //customer.SetAddress("456 Main Streat");

            Console.WriteLine("Address: {0}", customer.Address);
            Console.WriteLine("Name: {0}", customer.Name);
        }

        private void TestPropertyChanged()
        {
            var custom = new CustomerTest();
            custom.PropertyChanged += (sender, e) => { Console.WriteLine($"{e.PropertyName}已變更1"); };

            custom.Name = "1";
            custom.Name = "2";
            custom.Name = "3";
        }

        private void TestReport()
        {
            AuctionItem item = new AuctionItem();
            item.Price += 1000;
            item.Price += 2000;
            item.Price += 3000;
        }
    }

    #region TestPropertyFiledGetSet

    public class Customer
    {
        [StringChecker]
        private string _address;

        public Customer(string address)
        {
            _address = address;
        }

        [StringChecker]
        public string Name { get; set; }

        public string Address
        { get { return _address; } }

        public void SetAddress(string address) => this._address = address;
    }

    [PSerializable]
    public class StringCheckerAttribute : LocationInterceptionAspect
    {
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            base.OnGetValue(args); // 取完當前數值 args.Value 才有值
            object o = args.GetCurrentValue();
            if (o == null)
            {
                args.SetNewValue("value not set");
                //args.Value = "foo";
            }
            if (args.Value == null)
            {
                //args.Value = "foo";
            }

            base.OnGetValue(args); // 取完當前數值 SetNewValue後 最後輸出的才有值出去
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            // GetCurrentValue = 目前數值
            // Value = 欲設定的數值
            string existingValue = (string)args.GetCurrentValue();

            //if (args.Value == null)
            //{
            //    args.Value = "Empty String";
            //}

            args.ProceedSetValue();
        }
    }

    #endregion TestPropertyFiledGetSet

    #region TestPropertyChanged

    internal class Entity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [NotifyPropertyChanged]
    internal class CustomerTest : Entity
    {
        public string Name { get; set; }
    }

    [PSerializable]
    public class NotifyPropertyChanged : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            if (args.Value != args.GetCurrentValue())
            {
                args.Value = args.Value;
                args.ProceedSetValue();
                ((Entity)args.Instance).OnPropertyChanged(args.Binding.LocationInfo.Name);
            }
        }
    }

    #endregion TestPropertyChanged

    #region TestReport

    internal class AuctionItem
    {
        [Report]
        public int Price { get; set; } = 40000;
    }

    [Serializable]
    internal class ReportAttribute : LocationInterceptionAspect
    {
        public void OnSetValue(LocationInterceptionArgs args)
        {
            args.ProceedSetValue();
            Console.WriteLine("The " + args.LocationName + " of the item changed to " + args.Value);
        }

        /// <summary>
        /// 初始化 只執行一次
        /// </summary>
        public void OnInstanceLocationInitialized(LocationInitializationArgs args)
        {
            Console.WriteLine("The " + args.LocationName + " of the item starts out at " + args.Value);
        }
    }

    #endregion TestReport
}