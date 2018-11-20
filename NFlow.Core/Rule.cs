using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public class Rule : IRule
    {
        private List<IContinuation> continuations;
        public RuleContext context;

        public static IRule Define(string name)
        {
            Rule result = new Rule(name);
            return result;
        }

        public static IRule FromJson(string json)
        {
            return RuleSerializer.FromJson(json);
        }


        public Rule(string name)
        {
            continuations = new List<IContinuation>();
            context = new RuleContext();

            Name = name;
        }

        public string Name
        {
            get; set;
        }

        public IReadOnlyList<IContinuation> Continuations => continuations.AsReadOnly();

        public void AddContinuation(IContinuation continuation)
        {
            continuations.Add(continuation);
        }

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

        public void Execute()
        {
            foreach (var continuation in continuations)
            {
                continuation.Invoke(context);
            }
        }
    }
}
