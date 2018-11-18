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

        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, String name, String value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }
        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, String name, Decimal value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }
        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, String name, Boolean value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }
        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, String name, DateTime value)
        {
            return tasks.Add(new SetVariableAction<T>(name, value));
        }

        //public static FlowActions<T> Set<T>(this FlowActions<T> tasks, Action<T> model)
        //{
        //    return tasks.Add(new SetVariableAction<T>(model));
        //}

        public static FlowActions<T> Set<T>(this FlowActions<T> tasks, Action<FlowContext<T>> context)
        {
            return tasks.Add(new SetVariableAction<T>(context));
        }

    }
}
