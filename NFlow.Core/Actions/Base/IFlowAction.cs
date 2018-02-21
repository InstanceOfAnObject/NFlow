using NFlow.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions.Base
{
    public interface IFlowAction<T> : IFlowActionDescriptor
    {
        void Execute(FlowContext<T> context);
    }
}
