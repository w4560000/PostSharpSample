using PostSharp.Patterns.Contracts;
using System;

namespace PostSharpSample.Contracts.CustomContracts
{
    public class MyContractException2Factory : ContractExceptionFactory
    {
        public MyContractException2Factory(ContractExceptionFactory next)
          : base(next)
        {
        }

        public override Exception CreateException(ContractExceptionInfo exceptionInfo)
        {
            if (exceptionInfo.ExceptionType == typeof(NullReferenceException))
            {
                return new ArgumentNullException($"2 Argument {exceptionInfo.LocationName} was null, but with a custom exception.");
            }
            else
            {
                // Call the next node in the chain of invocation.
                return base.CreateException(exceptionInfo);
            }
        }
    }
}