using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NFlow.Core.Serialization;

namespace NFlow.Core
{
    public static class RuleSerializer
    {
        public static string ToJson(this IRule rule)
        {
            JsonRule jsonRule = new JsonRule() { Name = rule.Name };
            if(rule.Continuations?.Count > 0)
            {
                jsonRule.Operations = new List<JsonOperation>();
                foreach (var op in rule.Continuations)
                {
                    jsonRule.Operations.Add(new JsonOperation() { Type = op.GetType().AssemblyQualifiedName, Config = op.Config });
                }
            }

            return JsonConvert.SerializeObject(jsonRule, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
        }

        public static IRule FromJson(string json)
        {
            var jsonRule = JsonConvert.DeserializeObject<JsonRule>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            Rule rule = new Rule(jsonRule.Name);

            foreach (var op in jsonRule.Operations)
            {
                Type type = Type.GetType(op.Type);

                IContinuation continuation = Activator.CreateInstance(type) as IContinuation;
                continuation.Config = op.Config;
                    
                rule.AddContinuation(continuation);
            }

            return rule;
        }
    }
}
