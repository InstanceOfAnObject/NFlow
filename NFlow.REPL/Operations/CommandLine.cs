using System;
using System.Threading.Tasks;
using NFlow.Core;

namespace NFlow.REPL
{
    public static class CommandLine
    {
        public static Flow ConsoleOut(this Flow definition, string text)
        {
            definition.Add(new WriteLineOperation(text));

            return definition;
        }

        public class WriteLineOperation : IContinuation
        {
            public WriteLineOperation() { }
            public WriteLineOperation(string text)
            {
                Config = new WriteLineConfig() { Text = text };
            }

            public IOperationConfig Config
            {
                get;
                set;
            }

            public async Task Invoke(RuleContext context)
            {
                await Console.Out.WriteLineAsync((Config as WriteLineConfig).Text);
            }
        }

        public class WriteLineConfig : IOperationConfig
        {
            public string Text
            {
                get;
                set;
            }
        }
    }
}