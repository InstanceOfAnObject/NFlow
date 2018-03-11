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
    public class VariablesTests
    {
        [TestMethod]
        public void VariablesTests_SetModelVariable()
        {
            var model = new VariablesTestsModel();
            
            var flow = new Flow<VariablesTestsModel>();
            flow.Actions.Set(m => m.Model.Price = 500);

            flow.Execute(model);

            Assert.IsTrue(model.Price == 500);
        }

        [TestMethod]
        public void VariablesTests_ManipulateModelVariable()
        {
            var model = new VariablesTestsModel();
            
            var flow = new Flow<VariablesTestsModel>();
            flow.Actions.Set(m => m.Model.Price = 500).Set(m => m.Model.Price += 10.5M);

            flow.Execute(model);

            Assert.IsTrue(model.Price == 510.5M);
        }

        [TestMethod]
        public void VariablesTests_SetGlobalVariable()
        {
            var model = new VariablesTestsModel();
            
            var flow = new Flow<VariablesTestsModel>();
            flow.Actions.Set("MyVar1", 200).Set(m => m.Model.Price = m.Variables.Get<Decimal>("MyVar1"));

            flow.Execute(model);

            Assert.IsTrue(model.Price == 200);
        }

        [TestMethod]
        public void VariablesTests_SetDefaultGlobalVariable()
        {
            var model = new VariablesTestsModel();
            
            var flow = new Flow<VariablesTestsModel>();
            flow.DefaultVariables.Set("MyVar1", 200);

            flow.Actions.Set(m => m.Model.Price = m.Variables.Get<Decimal>("MyVar1"));

            flow.Execute(model);

            Assert.IsTrue(model.Price == 200);
        }

        public class VariablesTestsModel
        {
            public Decimal Price { get; set; }
        }
    }
}
