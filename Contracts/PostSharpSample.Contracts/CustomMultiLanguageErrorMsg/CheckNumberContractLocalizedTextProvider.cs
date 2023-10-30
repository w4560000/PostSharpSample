using PostSharp.Patterns.Contracts;
using System;
using System.Globalization;
using System.Resources;

namespace PostSharpSample.Contracts.CustomMultiLanguageErrorMsg
{
    public class CheckNumberContractLocalizedTextProvider : ContractLocalizedTextProvider
    {
        public CheckNumberContractLocalizedTextProvider(ContractLocalizedTextProvider next)
          : base(next)
        {
        }

        public override string GetMessage(string messageId)
        {
            if (string.IsNullOrEmpty(messageId))
                throw new ArgumentNullException("messageId");

            string culture = true ? "zh-TW" : "en-US";// CultureInfo.CurrentUICulture.Name;
            
            ResourceManager rm = new ResourceManager("PostSharpSample.Contracts.CustomMultiLanguageErrorMsg.ErrorMsgResource", typeof(Program).Assembly);
            string msg = rm.GetString("CheckNumberErrorMsg", new CultureInfo(culture));

            if (!string.IsNullOrWhiteSpace(msg))
            {
                return msg;
            }
            else
            {
                // Fall back to the default provider.
                return base.GetMessage(messageId);
            }
        }
    }
}