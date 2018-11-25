using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Utils
{
    public class ConditionEvaluator
    {
        public static ConditionEvaluator True()
        {
            return new ConditionEvaluator("true");
        }

        public ConditionEvaluator(string condition)
        {
            Condition = condition;
        }

        private string Condition { get; }
        private List<Assembly> References { get; } = new List<Assembly>();
        private List<string> Imports { get; } = new List<string>() { "System", "System.Collections", "System.Collections.Generic" };

        public void AddReferences(params Assembly[] references)
        {
            References.AddRange(references);
        }

        public void AddImports(params string[] imports)
        {
            Imports.AddRange(imports);
        }

        public Task<bool> Evaluate(RuleContext context)
        {
            ScriptOptions options =
                ScriptOptions.Default
                    .WithReferences(References)
                    .WithImports(Imports);

            StringBuilder code = new StringBuilder();
            if (context != null)
            {
                foreach (var v in context.Variables.Keys)
                {
                    var name = v;
                    var type = context.Variables[v].GetType().Name;
                    code.AppendLine($"{type} {name} = ({type})Variables[\"{name}\"];");
                } 
            }

            // add the condition
            code.AppendLine(Condition);

            // execute
            if(context != null)
                return CSharpScript.EvaluateAsync<bool>(code.ToString(), options, context, typeof(RuleContext));
            else
                return CSharpScript.EvaluateAsync<bool>(code.ToString(), options);
        }
    }
}
