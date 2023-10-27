using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Formatters;
using System.Diagnostics;

namespace PostSharpSample.Logging.FormattingLogRecord
{
    /// <summary>
    /// 直接排除敏感資訊不印Log
    /// </summary>
    [Log(AttributeExclude = true)]
    [DebuggerDisplay("{Value}")]
    public class Sensitive<T> : IFormattable
    {
        public T Value { get; }

        public Sensitive(T value)
        {
            Value = value;
        }

        public override string? ToString() => "(Confidential)";

        void IFormattable.Format(UnsafeStringBuilder stringBuilder, FormattingRole role)
        {
            stringBuilder.Append("(Confidential)");
        }
    }
}