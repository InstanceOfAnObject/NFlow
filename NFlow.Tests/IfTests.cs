using NFlow.Core;
using System;
using Xunit;

namespace NFlow.Tests
{
    public class IfTests
    {
        [Fact]
        public async void IfTest_01()
        {
           var rule = Rule.Define("Simple")
                       .If("1 == 1", Flow.New().SetVar("value", 1).SetVar("value", 2))
                           .ElseIf("1 == 2", Flow.New().SetVar("value", 3))
                           .Else(Flow.New().SetVar("value", 4))
                       .End();

            await rule.Execute();

            Assert.True((int)rule["value"] == 2);
        }

        [Fact]
        public async void IfTest_02()
        {
            var rule = Rule.Define("With context values")
                        .If("Input == 1", Flow.New().SetVar("value", 1))
                            .ElseIf("Input == 2", Flow.New().SetVar("value", 2))
                            .Else(Flow.New().SetVar("value", 10))
                        .End();


            rule["input"] = 1;
            await rule.Execute();
            Assert.True((int)rule["value"] == 1);

            //rule["input"] = 2;
            //await rule.Execute();
            //Assert.True((int)rule["value"] == 2);

            //rule["input"] = 999;
            //await rule.Execute();
            //Assert.True((int)rule["value"] == 10);
        }
    }
}
