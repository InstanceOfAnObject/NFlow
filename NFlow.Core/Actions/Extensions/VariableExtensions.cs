using NFlow.Core.Actions;
using NFlow.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class VariableExtensions
    {

        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, String name, Object value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }
        public static FlowActions<T> Set<T>(FlowActions<T> tasks, String name, Func<FlowContext<T>, Object> value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }

    }
}
