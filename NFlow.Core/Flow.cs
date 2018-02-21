using NFlow.Core.Global;
using NFlow.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class Flow<T>
    {
        private Boolean isSubFlow = false;
        public FlowContext<T> Context { get; } = new FlowContext<T>();
        public FlowActions<T> Actions { get; }

        public Flow()
        {
            Actions = new FlowActions<T>(Context);
        }
        public Flow(FlowContext<T> context) : this()
        {
            Context = context;
            Actions = new FlowActions<T>(context);
        }

        public Boolean IsSubFlow() => isSubFlow;

        public Flow<T> IsSubFlow(Boolean value)
        {
            isSubFlow = value;
            return this;
        }

        public void Execute()
        {
            foreach (var task in Actions)
            {
                // trace the execution
                Context.ExecutionTrace.Add(task.ToString());

                task.Execute(Context);
            }
        }
    }
}
