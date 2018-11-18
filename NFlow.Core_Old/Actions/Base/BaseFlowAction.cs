using NFlow.Core.Actions;
using NFlow.Core.Global;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions.Base
{
    public abstract class BaseFlowAction<T> : IFlowAction<T>, IDefineSimpleNotation
    {
        Guid _id = Guid.NewGuid();

        public Guid Id => _id;

        public abstract string Text { get; set; }
        public abstract NotationObjectTypes NotationObjectType { get; set; }

        public abstract void Execute(FlowContext<T> context);
    }
}
