using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class Rule : IRule
    {
        private Flow flow;
        public RuleContext context;

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

            this.flow = flow;
            context = new RuleContext();

            Name = name;
        }

        public string Name
        {
            get; set;
        }

        public Flow Flow => flow;

        public object this[string name]
        {
            get
            {
                return context[name];
            }
            set
            {
                context[name] = value;
            }
        }

        public Task Execute()
        {
            return Flow.Execute(context);
        }
    }
}
