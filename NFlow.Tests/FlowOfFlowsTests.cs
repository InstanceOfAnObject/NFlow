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
    public class FlowOfFlowsTests
    {
        [TestMethod]
        public void FlowOfFlows_OneNestedFlow()
        {
            // initialize model
            var model = new FlowOfFlowsTestsModel() { Price = 5000 };

            // define flow1
            var flow1 = new Flow<FlowOfFlowsTestsModel>();
            flow1.Actions.WriteLine("Starting!").Set(m => m.Model.Price = 500);

            // define flow2
            var flow2 = new Flow<FlowOfFlowsTestsModel>();
            flow1.Actions.WriteLine("Setting another price!").Set(m => m.Model.Price = 2500);

            // define main flow
            var mainflow = new Flow<FlowOfFlowsTestsModel>();
            mainflow.Actions.Add(flow1).Add(flow2);

            // execute main flow
            mainflow.Execute(model);

            // assert result
            Assert.IsTrue(model.Price == 2500);
        }

        public class FlowOfFlowsTestsModel
        {
            public Decimal Price { get; set; }
        }

    }
}
