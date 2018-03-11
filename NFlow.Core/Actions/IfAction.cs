using NFlow.Core.Actions.Base;
using NFlow.Core.Global;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions.Core
{
    public class IfAction<T> : BaseFlowAction<T>, IDefineSplitNotation<T>
    {
        Guid _id = Guid.NewGuid();

        private List<ConditionFlow<T>> ConditionFlows { get; set; } = new List<ConditionFlow<T>>();

        public IfAction() { }
        public IfAction(Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> action, String text = Defaults.ConditionText, String actionText = Defaults.ConditionActionText)
        {
            this.Text = text;
            ConditionFlows.Add(new ConditionFlow<T>() { Condition = condition, Action = action, Text = actionText });
        }

        public IfAction<T> ElseIf(Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> action, String actionText = Defaults.ConditionActionText)
        {
            ConditionFlows.Add(new ConditionFlow<T>() { Condition = condition, Action = action, Text = actionText });
            return this;
        }

        public IFlowAction<T> Else(Action<FlowActions<T>> action, String actionText = Defaults.ConditionActionText)
        {
            ConditionFlows.Add(new ConditionFlow<T>() { Condition = null, Action = action, Text = actionText });
            return this;
        }

        public override void Execute(FlowContext<T> context)
        {
            Flow<T> conditionFlow = new Flow<T>();

            foreach (var cflow in ConditionFlows)
            {
                if (cflow.Condition == null) // ELSE condition (no condition)
                {
                    cflow.Action(conditionFlow.Actions);
                    break;
                }
                if (cflow.Condition.Invoke(context))    // evaluate condition
                {
                    cflow.Action(conditionFlow.Actions);
                    break;
                }
            }

            conditionFlow.Execute(context);
        }

        List<(Flow<T> Flow, String Text)> dotFlows;
        public override string Text { get; set; } = Defaults.ConditionText;
        public override NotationObjectTypes NotationObjectType { get; set; } = NotationObjectTypes.Decision;

        public (IDefineSimpleNotation Action, Dictionary<FlowActions<T>, String> InnerFlows) ToNotation()
        {
            // Make sure the inner flows are only generated once in order to retain the actions IDs
            if (dotFlows == null)
            {
                dotFlows = new List<(Flow<T> flow, string text)>();

                foreach (var cflow in ConditionFlows)
                {
                    Flow<T> flow = new Flow<T>();
                    cflow.Action(flow.Actions);
                    dotFlows.Add((flow, cflow.Text));
                }
            }

            // create the inner flows collection
            Dictionary<FlowActions<T>, String> innerFlows = new Dictionary<FlowActions<T>, String>();
            foreach (var dflow in dotFlows)
            {
                innerFlows.Add(dflow.Flow.Actions, dflow.Text);
            }

            return (this, innerFlows);
        }

    }

    //public class IfActionMetadata
    //{
    //    public String Text { get; set; }
    //    public String TrueLinkText { get; set; }
    //    public String FalseLinkText { get; set; }
    //}

    public class ConditionFlow<T>
    {
        public Func<FlowContext<T>, Boolean> Condition { get; set; }

        public Action<FlowActions<T>> Action { get; set; }

        public String Text { get; set; } = String.Empty;
    }
}
