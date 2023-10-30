using PostSharp.Patterns.Contracts;
using System;

namespace PostSharpSample.Contracts.CustomContracts
{
    public class MyContractExceptionFactory : ContractExceptionFactory
    {
        public MyContractExceptionFactory(ContractExceptionFactory next)
          : base(next)
        {
        }

        public override Exception CreateException(ContractExceptionInfo exceptionInfo)
        {
            if (exceptionInfo.ExceptionType == typeof(ArgumentNullException))
            {
                return new NullReferenceException($"1 Argument {exceptionInfo.LocationName} was null, but with a custom exception.");
            }
            else
            {
                // Call the next node in the chain of invocation.
                return base.CreateException(exceptionInfo);
            }
        }
    }
}