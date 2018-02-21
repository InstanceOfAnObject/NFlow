using NFlow.Core.Actions.Base;
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
        private List<IFlowAction<T>> tasks = new List<IFlowAction<T>>();
        private FlowContext<T> context = null;

        public FlowActions(FlowContext<T> context) { this.context = context; }

        public int Count { get => tasks.Count; }

        public IEnumerator<IFlowAction<T>> GetEnumerator()
        {
            return tasks.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        public FlowActions<T> Add(IFlowAction<T> action)
        {
            if (action != null)
                tasks.Add(action);

            return this;
        }

    }
}
