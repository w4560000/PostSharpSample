using PostSharp.Patterns.Contracts;
using System;
using System.Collections.Generic;

namespace PostSharpSample.Contracts.CustomContracts
{
    public class CzechContractLocalizedTextProvider : ContractLocalizedTextProvider
    {
        private readonly Dictionary<string, string> messages = new Dictionary<string, string>
        {
           {RegularExpressionErrorMessage, "Hodnota {2} neodpovídá regulárnímu výrazu '{4}'."},
           {"NonZeroErrorMessage", "Hodnota {2} nesmí být 0."},
           {"LocationContractErrorMessage", "{2} 不能為 0."}
        };

        public CzechContractLocalizedTextProvider(ContractLocalizedTextProvider next)
          : base(next)
        {
        }

        public override string GetMessage(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                throw new ArgumentNullException("messageId");

            string message;
            if (messages.TryGetValue(messageId, out message))
            {
                return message;
            }
            else
            {
                // Fall back to the default provider.
                return base.GetMessage(messageId);
            }
        }
    }
}