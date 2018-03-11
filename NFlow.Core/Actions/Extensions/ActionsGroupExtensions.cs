using NFlow.Core.Actions;
using NFlow.Core.Actions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class ActionsGroupExtensions
    {

        public static FlowActions<T> StartActionsGroup<T>(
            this FlowActions<T> actions, 
            Action<FlowActions<T>> groupActions)
        {
            return actions.Add(new ActionsGroupAction<T>(groupActions));
        }

        public static FlowActions<T> StartActionsGroup<T>(
            this FlowActions<T> actions,
            String text,
            Action<FlowActions<T>> groupActions)
        {
            return actions.Add(new ActionsGroupAction<T>(groupActions) { Text = text });
        }

    }
}
