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
            rule1.Execute();
            rule2.Execute();

            // get the value from both rules
            Int32.TryParse(rule1["year"].ToString(), out var value1);
            Int32.TryParse(rule2["year"].ToString(), out var value2);

            // assert if the values as the same and have the same value
            Assert.True(value1 == 2018);
            Assert.True(value2 == 2018);
        }
    }
}
