using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using NFlow.Core;
using NFlow.Core.Utils;
using System;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlow.Tests
{
    public class ConditionTests
    {
        [Fact]
        public void StringCondition_eq()
        {
            ConditionEvaluator c1 = new ConditionEvaluator("1 == 1");
            var result = c1.Evaluate(null).Result;

            Assert.True(result);
        }

        [Fact]
        public void StringCondition_gt()
        {
            ConditionEvaluator c1 = new ConditionEvaluator("2 > 1");
            var result = c1.Evaluate(null).Result;

            Assert.True(result);
        }

        [Fact]
        public async Task UsingContext()
        {
            RuleContext ctx = new RuleContext();
            ctx.Variables["Value"] = 10;

            ConditionEvaluator c1 = new ConditionEvaluator("Value == 10");
            var result = await c1.Evaluate(ctx);

            Assert.True(result);
        }

        [Fact]
        public async Task UsingContext_ChildObject()
        {
            RuleContext ctx = new RuleContext();
            ctx.Variables["Stuff"] = new CustomContextPropertyType() { Value = 41 };

            ConditionEvaluator c1 = new ConditionEvaluator("Stuff.Value == 41");
            c1.AddReferences(typeof(CustomContextPropertyType).Assembly);
            c1.AddImports("NFlow.Tests");

            var result = await c1.Evaluate(ctx);

            Assert.True(result);
        }
    }

    public class CustomContextPropertyType
    {
        public int Value { get; set; }
    }
}
