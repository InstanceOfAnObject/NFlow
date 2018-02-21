using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlow.Core.Global;
using NFlow.Core.NotationSupport;

namespace NFlow.Core.Actions.Base
{
    public class GenericAction<T> : BaseFlowAction<T>
    {
        public override string Text { get; set; }
        public override NotationObjectTypes NotationObjectType { get; set; }


        private Action<FlowContext<T>> action;
        public virtual void Action(Action<FlowContext<T>> action)
        {
            this.action = action;
        }

        public override void Execute(FlowContext<T> context)
        {
            if (action != null)
                this.action(context);
            else
                throw new NotImplementedException();
        }
    }
}
