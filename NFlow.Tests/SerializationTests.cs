using NFlow.Core;
using System;
using Xunit;

namespace NFlow.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void Serialization_SetVar()
        {
            // Set simple rule
            var rule1 = Rule.Define("Simple").SetVar("year", 2018).End();

            // serialize and deserialize it
            var jsonRule = rule1.ToJson();
            var rule2 = Rule.FromJson(jsonRule);

            // execute both rules
            rule2.Execute();

            // get the value from both rules
            Int32.TryParse(rule2["year"].ToString(), out var value2);

            // assert if the values as the same and have the same value
            Assert.True(value2 == 2018);
        }

        [Fact]
        public void Serialization_If()
        {
            // Set simple rule
            var rule1 = Rule.Define("Simple")
                        .If("1 == 1", Flow.New().SetVar("value", 1).SetVar("value", 2))
                            .ElseIf("1 == 2", Flow.New().SetVar("value", 3))
                            .Else(Flow.New().SetVar("value", 4))
                        .End();

            // serialize and deserialize it
            var jsonRule = rule1.ToJson();
            var rule2 = Rule.FromJson(jsonRule);

            // execute both rules
            rule2.Execute();

            // get the value from both rules
            Int32.TryParse(rule2["value"].ToString(), out var value);

            // assert if the values as the same and have the same value
            Assert.True(value == 2);
        }
    }
}
