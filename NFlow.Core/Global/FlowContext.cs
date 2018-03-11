using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class FlowContext<T>
    {
        public FlowVariables Variables { get; } = new FlowVariables();

        public T Model { get; set; }
        
        public FlowContext(T model)
        {
            this.Model = model;
        }
    }
}
