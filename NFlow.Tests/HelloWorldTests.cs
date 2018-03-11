using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Tests
{
    [TestClass]
    public class HelloWorldTests
    {
        [TestMethod]
        public void HelloWorld_SetModelValue()
        {
            var model = new ValueModel<int>();
            var flow = new Flow<ValueModel<int>>();

            flow.Actions.Set(m => m.Model.Value = 5);

            flow.Execute(model);

            Assert.IsTrue(model.Value == 5);

            var dot = flow.ToDotNotation();
        }

        [TestMethod]
public void HelloWorld_PairOrOdd()
{
    var model = new PairOddModel() { Value = 5 };
    var flow = new Flow<PairOddModel>();

    flow.Actions
        .If(ctx => ctx.Model.Value % 2 == 0, a => a.Set(m => m.Model.PairOrOdd = "Pair"), "Pair|Odd?", "Is Pair")
        .Else(a => a.Set(m => m.Model.PairOrOdd = "Odd"), "Is Odd");

    flow.Execute(model);

    Assert.IsTrue(model.PairOrOdd == "Odd");

    var dot = flow.ToDotNotation();
}

        public class PairOddModel
        {
            public int Value { get; set; }
            public String PairOrOdd { get; set; }
        }

    }
}
