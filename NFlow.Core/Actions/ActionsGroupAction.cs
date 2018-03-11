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
    public class ActionsGroupAction<T> : BaseFlowAction<T>, IDefineSplitNotation<T>
    {
        Guid _id = Guid.NewGuid();

        private Action<FlowActions<T>> GroupActions { get; set; }

        public ActionsGroupAction() { }
        public ActionsGroupAction(Action<FlowActions<T>> groupActions)
        {
            this.GroupActions = groupActions;
        }

        public override void Execute(FlowContext<T> context)
        {
            Flow<T> conditionFlow = new Flow<T>();

            GroupActions(conditionFlow.Actions);

            conditionFlow.Execute(context);
        }

        Flow<T> actionsFlow = null;
        public override string Text { get; set; } = Defaults.ActionsGroupText;
        public override NotationObjectTypes NotationObjectType { get; set; } = NotationObjectTypes.Action;

        public (IDefineSimpleNotation Action, Dictionary<FlowActions<T>, String> InnerFlows) ToNotation()
        {
            // Make sure the inner flows are only generated once in order to retain the actions IDs
            if (actionsFlow == null)
            {
                actionsFlow = new Flow<T>();
                GroupActions(actionsFlow.Actions);
            }

            //var action = new ActionDotRepresentation<T>(Id, DotCaption, DotShape);
            Dictionary<FlowActions<T>, String> innerFlows =
                new Dictionary<FlowActions<T>, String>()
                {
                        { actionsFlow.Actions, String.Empty }
                };

            return (this, innerFlows);
        }

    }
}
