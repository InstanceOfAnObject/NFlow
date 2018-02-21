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

        private Func<FlowContext<T>, Boolean> Condition { get; set; }
        private Action<FlowActions<T>> TrueActions { get; set; }
        private Action<FlowActions<T>> FalseActions { get; set; }

        String trueLinkText = "True";
        String falseLinkText = "False";

        public IfAction() { }
        public IfAction(Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> trueActions, Action<FlowActions<T>> falseActions)
        {
            this.Condition = condition;
            this.TrueActions = trueActions;
            this.FalseActions = falseActions;
        }

        public IfAction(IfActionMetadata metadata, Func<FlowContext<T>, Boolean> condition, Action<FlowActions<T>> trueActions, Action<FlowActions<T>> falseActions)
        {
            this.Text = metadata.Text;
            this.trueLinkText = metadata.TrueLinkText;
            this.falseLinkText = metadata.FalseLinkText;

            this.Condition = condition;
            this.TrueActions = trueActions;
            this.FalseActions = falseActions;
        }

        public IfAction(
            Func<FlowContext<T>, Boolean> condition,
            (String linkName, Action<FlowActions<T>> actions) trueActions,
            (String linkName, Action<FlowActions<T>> actions) falseActions) : this(condition, trueActions.actions, falseActions.actions)
        {
            trueLinkText = trueActions.linkName;
            falseLinkText = falseActions.linkName;
        }

        public override void Execute(FlowContext<T> context)
        {
            Flow<T> conditionFlow = new Flow<T>(context);

            if (Condition.Invoke(context))
                TrueActions(conditionFlow.Actions);
            else
                FalseActions(conditionFlow.Actions);

            conditionFlow.IsSubFlow(true).Execute();
        }

        Flow<T> dotTrueFlow;
        Flow<T> dotFalseFlow;
        public override string Text { get; set; } = "Condition";
        public override NotationObjectTypes NotationObjectType { get; set; } = NotationObjectTypes.Decision;

        public (IDefineSimpleNotation Action, Dictionary<string, FlowActions<T>> InnerFlows) ToNotation()
        {
            // Make sure the inner flows are only generated once in order to retain the actions IDs
            if (dotTrueFlow == null && dotFalseFlow == null)
            {
                dotTrueFlow = new Flow<T>();
                dotFalseFlow = new Flow<T>();

                TrueActions(dotTrueFlow.Actions);
                FalseActions(dotFalseFlow.Actions);
            }

            //var action = new ActionDotRepresentation<T>(Id, DotCaption, DotShape);
            Dictionary<String, FlowActions<T>> innerFlows =
                new Dictionary<string, FlowActions<T>>()
                {
                        { this.trueLinkText, dotTrueFlow.Actions },
                        { this.falseLinkText, dotFalseFlow.Actions }
                };

            return (this, innerFlows);
        }

    }

    public class IfActionMetadata
    {
        public String Text { get; set; }
        public String TrueLinkText { get; set; }
        public String FalseLinkText { get; set; }
    }
}
