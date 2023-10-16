using System;
using System.Collections.Generic;
using System.Text;

namespace PostSharpSample.Logging.Audit
{
    public class BusinessObject
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
