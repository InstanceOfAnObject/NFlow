using NFlow.Core.Actions.Base;
using NFlow.Core.Global;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions
{
    public class AdHocAction<T> : BaseFlowAction<T>
    {
        private Action<FlowContext<T>> action = null;

        public AdHocAction(String text, Action<FlowContext<T>> action)
        {
            this.Text = text;
            this.action = action;
        }

        public override string Text { get; set; }
        public override NotationObjectTypes NotationObjectType { get; set; } = NotationObjectTypes.Action;

        public override void Execute(FlowContext<T> context)
        {
            if (action != null)
                action.Invoke(context);
        }
    }
}
