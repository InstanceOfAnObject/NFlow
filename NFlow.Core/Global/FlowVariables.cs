using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Global
{
    public class FlowVariables
    {
        Dictionary<String, Object> vars = new Dictionary<String, Object>();

        public FlowVariables Set(String name, Object value)
        {
            if (vars.ContainsKey(name))
                vars[name] = value;
            else
                vars.Add(name, value);

            return this;
        }

        public Object Get(String name)
        {
            return vars[name];
        }

        public T Get<T>(String name)
        {
            var value = vars[name];
            if (value is T)
                return (T)value;
            else
                throw new InvalidCastException($"Cannot convert value to reuested type");
        }
    }
}
