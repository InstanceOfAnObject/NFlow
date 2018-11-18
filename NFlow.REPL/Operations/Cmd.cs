using System;
using NFlow.Core;

namespace NFlow.REPL
{
    public static class Cmd
    {
        public static IRule ConsoleOut(this IRule operation, string text)
        {
            operation.AddOperation(new WriteLine(text));

            return operation;
        }

        public class WriteLine : IOperation
        {
            public WriteLine() { }
            public WriteLine(string text)
            {
                Config = new WriteLineConfig() { Text = text };
            }

            public IOperationConfig Config
            {
                get;
                set;
            }

            public void Execute()
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