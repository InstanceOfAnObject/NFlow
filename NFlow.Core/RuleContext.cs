using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public class RuleContext
    {
        Dictionary<string, object> variables = new Dictionary<string, object>();

        public RuleContext()
        {
        }

        public object this[string name]
        {
            get
            {
                if (variables.ContainsKey(name))
                    return variables[name];
                else
                    return null;
            }
            set
            {
                if (variables.ContainsKey(name))
                    variables[name] = value;
                else
                    variables.Add(name, value);
            }
        }

        /// <summary>
        /// Description of the tasks executed
        /// </summary>
        List<string> Audit = new List<string>();
    }
}
