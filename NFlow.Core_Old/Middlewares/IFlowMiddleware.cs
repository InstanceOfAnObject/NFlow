using NFlow.Core.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Middlewares
{
    public interface IFlowMiddleware<T>
    {
        void OnFlowStarting(Flow<T> flow);
        void OnFlowFinished(Flow<T> flow);

        void OnActionExecuting(IFlowAction<T> action);
        void OnActionExecuted(IFlowAction<T> action);

        void OnActionException(IFlowAction<T> action);
    }
}
