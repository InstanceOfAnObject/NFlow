using NFlow.Core.Global;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions.Base
{
    /// <summary>
    /// Represents an actions that contains multiple actions inside.
    /// Useful to create a logical grouping of actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericActionGroup<T> : BaseFlowAction<T>
    {
        public override string Text { get; set; }
        public override NotationObjectTypes NotationObjectType { get; set; }

        public override void Execute(FlowContext<T> context)
        {
            throw new NotImplementedException();
        }
    }
}
