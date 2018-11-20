using System;
using NFlow.Core;

namespace NFlow.REPL
{
    public static class CommandLine
    {
        public static IRule ConsoleOut(this IRule prev, string text)
        {
            prev.AddContinuation(new WriteLineOperation(text));

            return prev;
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

            public void Invoke(RuleContext context)
            {
                Console.WriteLine((Config as WriteLineConfig).Text);
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