using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Contracts
{
    public abstract class ContinuationBase : IContinuation
    {
        public virtual IOperationConfig Config { get; set; } = null;

        public abstract Task Invoke(RuleContext context);
    }
}
