using NFlow.Core.Actions.Base;
using NFlow.Core.Actions.Core;
using NFlow.Core.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions
{
    public partial class FlowActions<T> : IEnumerable<IFlowAction<T>>
    {
        protected internal List<IFlowAction<T>> actions = new List<IFlowAction<T>>();
        //protected internal FlowContext<T> context = null;

        public FlowActions() {}
        public FlowActions(IfFlowActions<T> ifFlowActions)
        {
            this.actions = ifFlowActions.actions;
        }

        public int Count { get => actions.Count; }

        public IEnumerator<IFlowAction<T>> GetEnumerator()
        {
            return actions.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return actions.GetEnumerator();
        }

        public FlowActions<T> Add(IFlowAction<T> action)
        {
            if (action != null)
                actions.Add(action);

            return this;
        }

        public FlowActions<T> Add(Flow<T> flow)
        {
            if (flow != null)
                actions.AddRange(flow.Actions);

            return this;
        }

    }

    /// <summary>
    /// When an expression starts an If condition, the amount of available methods is limited.
    /// This also allows to target certain extension methods specifically to If blocks, like ElseIf and Else.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IfFlowActions<T>
    {
        protected internal List<IFlowAction<T>> actions = new List<IFlowAction<T>>();
        protected internal FlowContext<T> context = null;

        public IfFlowActions(FlowActions<T> flowActions, IfAction<T> ifAction)
        {
            actions = flowActions.actions;
            IfAction = ifAction;
        }

        public IfAction<T> IfAction { get; }
    }
}
