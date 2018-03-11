using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
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
            try
            {
                var value = vars[name];

                if (value is T)
                {
                    return (T)value;
                }
                else
                {
                    // if the type requested is not the same as the stored, attempt a blind conversion
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    return (T)converter.ConvertFromInvariantString(value.ToString());
                }
            }
            catch (NotSupportedException)
            {
                throw new InvalidCastException($"Cannot convert value to reuested type");
            }
        }


        /// <summary>
        /// Merges two sets of variables.
        /// </summary>
        /// <param name="variables">List of variables to merge with</param>
        /// <param name="replaceExisting">Specify if existing variables should be replaced or not. By default, existing variables as not replaced.</param>
        /// <returns></returns>
        public FlowVariables Merge(FlowVariables variables, Boolean replaceExisting = false)
        {
            if (variables != null && variables.vars.Count > 0)
            {
                foreach (var varKey in variables.vars.Keys)
                {
                    if (!this.vars.ContainsKey(varKey))
                    {
                        this.vars.Add(varKey, variables.vars[varKey]);
                    }
                    else if (this.vars.ContainsKey(varKey) && replaceExisting)
                    {
                        this.vars[varKey] = variables.vars[varKey];
                    }
                }
            }

            return this;
        }
    }
}
