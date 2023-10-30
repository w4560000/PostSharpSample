using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;
using PostSharp.Reflection;
using System;

namespace PostSharpSample.Contracts.CustomContracts
{
    public class NewNonZeroAttribute : LocationContractAttribute, ILocationValidationAspect<int>
    {
        public NewNonZeroAttribute()
        {
        }

        public Exception ValidateValue(int value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value == 0)
            {
                object[] additionalArguments = Array.Empty<object>();
                const string messageId = ContractLocalizedTextProvider.LocationContractErrorMessage;
                ContractExceptionInfo exceptionInfo = new ContractExceptionInfo(typeof(ArgumentOutOfRangeException), this, value, locationName,
                                                                                 locationKind, context, messageId, additionalArguments);
                return ContractServices.ExceptionFactory.CreateException(exceptionInfo);
            }
            else
            {
                return null;
            }
        }
    }
}