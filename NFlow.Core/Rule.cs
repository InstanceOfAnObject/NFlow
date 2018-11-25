using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class Rule : IRule
    {
        public static Flow Define(string name = null, Flow definition = null)
        {
            Rule result = new Rule(name, definition);
            return result.Flow;
        }

        public static IRule FromJson(string json)
        {
            return RuleSerializer.FromJson(json);
        }

        public Rule(string name = null, Flow flow = null)
        {
            if (flow == null)
                flow = new Flow();
            flow.Rule = this;

            Flow = flow;
            Context = new RuleContext();

            Name = name;
        }

        public string Name
        {
            get; set;
        }

        public Flow Flow { get; }
        public RuleContext Context { get; }

        public object this[string name]
        {
            get
            {
                return Context.Variables[name];
            }
            set
            {
                Context.Variables[name] = value;
            }
        }

        public Task Execute()
        {
            return Flow.Execute(Context);
        }
    }
}
