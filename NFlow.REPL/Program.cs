using System;
using NFlow.Core;

namespace NFlow.REPL
{
    class Program
    {
        static void Main(string[] args)
        {
            var rule = Rule.Define("Simple").ConsoleOut("Alex");

            var jsonRule = rule.ToJson();
            var rule2 = Rule.FromJson(jsonRule);

            rule2.Execute();
        }
    }
}
