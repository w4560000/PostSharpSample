using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;
using PostSharp.Reflection;
using System;

namespace PostSharpSample.Contracts.CustomContracts
{
    /// <summary>
    /// 客製化約束 Attribute
    /// </summary>
    public class NonZeroAttribute : LocationContractAttribute, ILocationValidationAspect<int>, ILocationValidationAspect<uint>
    {
        public NonZeroAttribute()
            : base()
        {
        }

        public Exception ValidateValue(int value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value == 0)
                return new ArgumentOutOfRangeException($"The value of {locationName} cannot be 0.");
            else
                return null;
        }

        public Exception ValidateValue(uint value, string locationName, LocationKind locationKind, LocationValidationContext context)
        {
            if (value == 0)
                return new ArgumentOutOfRangeException($"The value of {locationName} cannot be 0.");
            else
                return null;
        }
    }
}