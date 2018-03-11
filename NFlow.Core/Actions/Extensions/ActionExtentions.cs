using NFlow.Core.Actions;
using NFlow.Core.Actions.Base;
using NFlow.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class ActionExtentions
    {

        public static FlowActions<T> Action<T>(this FlowActions<T> tasks, String text, Action<FlowContext<T>> action)
        {
            return tasks.Add(new AdHocAction<T>(text, action));
        }

    }
}
