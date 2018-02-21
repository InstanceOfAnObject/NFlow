using NFlow.Core.Actions;
using NFlow.Core.Actions.Core;
using NFlow.Core.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class FlowControlExtensions
    {

        public static FlowActions<T> If<T>(this FlowActions<T> actions, Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> trueActions, Action<FlowActions<T>> falseActions)
        {
            return actions.Add(new IfAction<T>(condition, trueActions, falseActions));
        }

        public static FlowActions<T> If<T>(this FlowActions<T> actions, (String Text, String TrueLinkName, String FalseLinkName) meta, Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> trueActions, Action<FlowActions<T>> falseActions)
        {
            return actions.Add(
                new IfAction<T>(
                    new IfActionMetadata() { Text = meta.Text, TrueLinkText = meta.TrueLinkName, FalseLinkText = meta.FalseLinkName }, 
                    condition, 
                    trueActions, 
                    falseActions));
        }

    } 
}
