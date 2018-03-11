using NFlow.Core.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Tracing
{
    public interface ITracingProvider<T>
    {
        void OnFlowStarting(Flow<T> flow);
        void OnFlowFinished(Flow<T> flow);

        void OnActionExecuting(IFlowAction<T> action);
        void OnActionExecuted(IFlowAction<T> action);
    }
}
