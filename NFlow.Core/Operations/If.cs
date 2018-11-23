using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class Conditions
    {
        public static IfOperation If(this Flow rootDefinition, string condition, Flow conditionDefinition)
        {
            return new IfOperation(rootDefinition, condition, conditionDefinition);
        }

        public class IfOperation : IContinuation
        {
            public IfOperation(Flow rootFlow, string condition, Flow conditionRuleDefinition)
            {
                RootFlow = rootFlow;

                Config = new IfOperationConfig();
                (Config as IfOperationConfig).EvaluationPaths.Add(new Condition(condition), conditionRuleDefinition);
            }

            public IOperationConfig Config { get; set; }

            public Flow RootFlow { get; }

            public IfOperation ElseIf(string condition, Flow conditionRuleDefinition)
            {
                (Config as IfOperationConfig).EvaluationPaths.Add(new Condition(condition), conditionRuleDefinition);
                return this;
            }

            public Flow Else(Flow conditionRuleDefinition)
            {
                (Config as IfOperationConfig).EvaluationPaths.Add(Condition.True(), conditionRuleDefinition);
                RootFlow.AddContinuation(this);
                return RootFlow;
            }

            public Flow EndIf()
            {
                RootFlow.AddContinuation(this);
                return RootFlow;
            }

            public async Task Invoke(RuleContext context)
            {
                var cfg = Config as IfOperationConfig;

                foreach (var condition in cfg.EvaluationPaths.Keys)
                {
                    if (await condition.Evaluate(context))
                    {
                        await cfg.EvaluationPaths[condition].Execute(context);
                        break;
                    }
                }
            }
        }
    }

    public class IfOperationConfig : IOperationConfig
    {
        /// <summary>
        /// Gets the list of conditions and their execution continuations.
        /// An "if" can have multiple evaluation paths (if / else if / else if / ... / else), each represented by a dedicated flow.
        /// </summary>
        public Dictionary<Condition, Flow> EvaluationPaths { get; } = new Dictionary<Condition, Flow>();
    }

    public class Condition
    {
        string strCondition;

        public static Condition True()
        {
            return new Condition("true");
        }

        public Condition(string condition)
        {
            strCondition = condition;
        }

        public async Task<bool> Evaluate(RuleContext context)
        {
            ScriptOptions options = ScriptOptions.Default;
            options.WithImports("System");

            var result = await CSharpScript.RunAsync<bool>(strCondition, options, context);
            //var result = await CSharpScript.EvaluateAsync<bool>(strCondition, options, context);

            return result.ReturnValue;
        }

        public class GlobalsDemo
        {
            public int Input { get; set; }
        }
    }
}
