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
    internal class SetVariableAction<T> : BaseFlowAction<T>
    {
        Guid _id = Guid.NewGuid();

        public String Name { get; set; }
        public Object Value { get; set; } = null;
        public Func<FlowContext<T>, Object> Function { get; set; } = null;

        public SetVariableAction() { }
        public SetVariableAction(String name, Object value)
        {
            Name = name;
            Value = value;
        }

        public SetVariableAction(String name, Func<FlowContext<T>, Object> value)
        {
            Name = name;
            Function = value;
        }

        public override void Execute(FlowContext<T> context)
        {
            if(Value != null)
                context.Variables.Set(Name, Value);
            else if (Function != null)
                context.Variables.Set(Name, Function.Invoke(context));

        }

        public override string Text { get; set; }
        public override NotationObjectTypes NotationObjectType { get; set; }

    }
}
