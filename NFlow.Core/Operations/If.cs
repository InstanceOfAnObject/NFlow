using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using NFlow.Core.Utils;
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
            public IfOperation() { }
            public IfOperation(Flow rootFlow, string condition, Flow conditionRuleDefinition)
            {
                RootFlow = rootFlow;

                Config = new IfOperationConfig();
                (Config as IfOperationConfig).EvaluationPaths.Add(new EvaluationPath() { Condition = new ConditionEvaluator(condition), Flow = conditionRuleDefinition });
            }

            public IOperationConfig Config { get; set; }

            public Flow RootFlow { get; }

            public IfOperation ElseIf(string condition, Flow conditionRuleDefinition)
            {
                (Config as IfOperationConfig).EvaluationPaths.Add(new EvaluationPath() { Condition = new ConditionEvaluator(condition), Flow = conditionRuleDefinition });
                return this;
            }

            public Flow Else(Flow conditionRuleDefinition)
            {
                (Config as IfOperationConfig).EvaluationPaths.Add(new EvaluationPath() { Condition = ConditionEvaluator.True(), Flow = conditionRuleDefinition });
                RootFlow.Add(this);
                return RootFlow;
            }

            public Flow EndIf()
            {
                RootFlow.Add(this);
                return RootFlow;
            }

            public async Task Invoke(RuleContext context)
            {
                var cfg = Config as IfOperationConfig;

                foreach (var path in cfg.EvaluationPaths)
                {
                    if (await path.Condition.Evaluate(context))
                    {
                        await path.Flow.Execute(context);
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
        public List<EvaluationPath> EvaluationPaths { get; set; } = new List<EvaluationPath>();
    }

    public class EvaluationPath
    {
        public ConditionEvaluator Condition { get; set; }
        public Flow Flow { get; set; }
    }
}
