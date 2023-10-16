using PostSharp.Patterns.Diagnostics.Audit;

namespace PostSharpSample.Logging.Audit
{
    public class PurchaseOrder : BusinessObject
    {
        // Not the Audit aspect on this method.
        [Audit]
        public void Approve(string comment = null)
        {
            // Details skipped.
            var a = 1;
        }

        // Not the Audit aspect on this method.
        [Audit]
        public string Approve(Test qqq)
        {
            return "123";
        }
    }

    public class Test
    {
        public int QQQ
        {
            get; set;
        }
    }
}