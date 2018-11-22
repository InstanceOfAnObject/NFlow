using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class Varialbles
    {
        public static Flow SetVar(this Flow rule, string name, object value)
        {
            rule.AddContinuation(new SetVarOperation(name, value));
            return rule;
        }

        public class SetVarOperation : IContinuation
        {
            public SetVarOperation() { }
            public SetVarOperation(string name, object value)
            {
                Config = new VarConfig() { Name = name, Value = value };
            }

            public IOperationConfig Config { get; set; }

            public async Task Invoke(RuleContext context)
            {
                await Task.Factory.StartNew(() => {
                    var cfg = Config as VarConfig;
                    context[cfg.Name] = cfg.Value;
                });
            }
        }

        /// <summary>
        /// Reusable config for all var operation
        /// </summary>
        public class VarConfig : IOperationConfig
        {
            public string Name { get; set; }

            public object Value { get; set; }
        }
    }
}
