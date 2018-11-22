using NFlow.Core;
using System;
using Xunit;

namespace NFlow.Tests
{
    public class ConditionTests
    {
        [Fact]
        public async void StringCondition_eq()
        {
            Condition c1 = new Condition("1 == 1");
            var result = await c1.Evaluate(null);

            Assert.True(result == true);
        }

        [Fact]
        public async void StringCondition_gt()
        {
            Condition c1 = new Condition("2 > 1");
            var result = await c1.Evaluate(null);

            Assert.True(result == true);
        }
    }
}
