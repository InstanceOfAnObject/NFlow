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
        public Action<FlowContext<T>> Method { get; set; } = null;

        public SetVariableAction() { }
        public SetVariableAction(String name, String value)
        {
            Name = name;
            Value = value;
        }
        public SetVariableAction(String name, Decimal value)
        {
            Name = name;
            Value = value;
        }

        public SetVariableAction(String name, Boolean value)
        {
            Name = name;
            Value = value;
        }

        public SetVariableAction(String name, DateTime value)
        {
            Name = name;
            Value = value;
        }

        public SetVariableAction(Action<FlowContext<T>> method)
        {
            Method = method;
        }

        public override void Execute(FlowContext<T> context)
        {
            if(Value != null)
                context.Variables.Set(Name, Value);
            else if (Method != null)
                Method.Invoke(context);

        }

        public override string Text { get; set; } = "Set Value";
        public override NotationObjectTypes NotationObjectType { get; set; }

    }
}
