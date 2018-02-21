using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Global
{
    public class FlowContext<T>
    {
        public List<String> ExecutionTrace { get; } = new List<string>();

        public FlowVariables Variables { get; } = new FlowVariables();

        public T Model { get; set; }

        public FlowContext() { }
    }
}
