using NFlow.Core;
using System;
using Xunit;

namespace NFlow.Tests
{
    public class Serialization
    {
        [Fact]
        public void Test1()
        {
            var rule = Rule.Define("Simple").SetVar("year", 2018);

            var jsonRule = rule.ToJson();
            var rule2 = Rule.FromJson(jsonRule);

            rule2.Execute();

            Assert.True((long)rule2["year"] == 2018);
        }
    }
}
