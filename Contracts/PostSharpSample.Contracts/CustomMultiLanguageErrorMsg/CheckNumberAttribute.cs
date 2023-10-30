using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;
using PostSharp.Reflection;
using System;

namespace PostSharpSample.Contracts.CustomMultiLanguageErrorMsg
{
    public class CheckNumberAttribute : LocationContractAttribute, ILocationValidationAspect<int>
    {
        public CheckNumberAttribute()
        {
        }

        public Exception ValidateValue(int value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value > 100)
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