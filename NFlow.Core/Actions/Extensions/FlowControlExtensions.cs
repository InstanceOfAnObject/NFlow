using NFlow.Core.Actions;
using NFlow.Core.Actions.Base;
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

        public static FlowActions<T> If<T>(
            this FlowActions<T> actions, 
            Func<FlowContext<T>, Boolean> condition, 
            Action<FlowActions<T>> trueActions, 
            Action<FlowActions<T>> falseActions)
        {
            IFlowAction<T> ifaction = new IfAction<T>(condition, trueActions).Else(falseActions);
            return actions.Add(ifaction);
        }

        public static IfFlowActions<T> If<T>(
            this FlowActions<T> actions,
            Func<FlowContext<T>, Boolean> condition,
            Action<FlowActions<T>> action, 
            String conditionText = Defaults.ConditionText,
            String actionText = Defaults.ConditionActionText)
        {
            IfAction<T> ifaction = new IfAction<T>(condition, action, conditionText, actionText);
            IfFlowActions<T> ifFlowActions = new IfFlowActions<T>(actions, ifaction);

            return ifFlowActions;
        }

        public static IfFlowActions<T> ElseIf<T>(
            this IfFlowActions<T> actions,
            Func<FlowContext<T>, Boolean> condition,
            Action<FlowActions<T>> action,
            String actionText = Defaults.ConditionActionText)
        {
            actions.IfAction.ElseIf(condition, action, actionText);
            return actions;
        }

        public static FlowActions<T> Else<T>(
            this IfFlowActions<T> ifactions,
            Action<FlowActions<T>> action,
            String actionText = Defaults.ConditionActionText)
        {
            ifactions.IfAction.Else(action, actionText);

            var actions = new FlowActions<T>(ifactions);
            actions.Add(ifactions.IfAction);

            return actions;
        }

    } 
}
