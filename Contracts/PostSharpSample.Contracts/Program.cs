using PostSharp.Patterns.Contracts;
using PostSharpSample.Contracts.Contracts;
using PostSharpSample.Contracts.CustomContracts;
using PostSharpSample.Contracts.CustomMultiLanguageErrorMsg;
using System;

namespace PostSharpSample.Contracts
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // 測試約束
                // TestContracts();

                // 測試客製化約束
                // TestCustomContracts();

                // 測試客製化約束 Exception
                //TestCustomContractException();

                // 測試客製化約束 Exception 且有多語系版本
                 TestCustomMuiltLanguageErrorMsg();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// 測試約束
        /// </summary>
        private static void TestContracts()
        {
            var customer = new CustomerModel();
            customer.SetFullName("Leo", null);
        }

        /// <summary>
        /// 測試客製化約束
        /// </summary>
        private static void TestCustomContracts()
        {
            ContractServices.LocalizedTextProvider = new CzechContractLocalizedTextProvider(ContractServices.LocalizedTextProvider);

            //int number = 10, dividend = 2;
            int number = 10, dividend = 0;
            var result = Mod(number, dividend);
        }

        // NonZero 單純檢查數值併拋出 Exception
        //private static bool Mod([NonZero] int number, [NonZero] int dividend) => (number % dividend) == 0;

        /// <summary>
        /// NewNonZero 檢查數值異常後 呼叫客製化約束 Exception
        /// </summary>
        private static bool Mod([NewNonZero] int number, [NewNonZero] int dividend) => (number % dividend) == 0;

        /// <summary>
        /// 測試客製化約束 Exception
        /// </summary>
        private static void TestCustomContractException()
        {
            ContractServices.LocalizedTextProvider = new CzechContractLocalizedTextProvider(ContractServices.LocalizedTextProvider);
            ContractServices.ExceptionFactory = new MyContractException2Factory(new MyContractExceptionFactory(ContractServices.ExceptionFactory));

            Oops(null);  // Throws CustomException.
        }

        private static void Oops([Required] string p)
        {
        }

        private static void TestCustomMuiltLanguageErrorMsg()
        {
            ContractServices.LocalizedTextProvider = new CheckNumberContractLocalizedTextProvider(ContractServices.LocalizedTextProvider);

            Sum(101, 20);
        }

        private static int Sum([CheckNumber] int number1, int number2) => number1 + number2;
    }
}