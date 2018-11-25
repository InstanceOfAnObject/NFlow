using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public class RuleContext
    {
        public RuleContext()
        {
        }

        public Variables Variables { get; } = new Variables();

        /// <summary>
        /// Description of the tasks executed
        /// </summary>
        List<string> Audit = new List<string>();
    }

    public class Variables
    {
        Dictionary<string, object> variables = new Dictionary<string, object>();

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

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return variables.Keys;
            }
        }
    }
}
