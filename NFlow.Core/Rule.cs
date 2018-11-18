using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public class Rule : IRule
    {
        private List<IOperation> operations;
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
            operations = new List<IOperation>();
            context = new RuleContext();

            Name = name;
        }

        public string Name
        {
            get; set;
        }

        public IReadOnlyList<IOperation> Operations => operations.AsReadOnly();

        public void AddOperation(IOperation operation)
        {
            operations.Add(operation);
        }

        public void Execute()
        {
            foreach (var operation in Operations)
            {
                operation.Execute();
            }
        }
    }
}
