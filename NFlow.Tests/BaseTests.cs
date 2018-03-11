using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFlow.Core;
using NFlow.Core.Global;

namespace NFlow.Tests
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void BaseTests_ApplyDiscount()
        {
            var flow = new Flow<BaseTestsModel>();

            flow.Actions
                    .If(c => c.Model.Type == "Gold", g => g.Action("20%", ctx => ctx.Model.Discount = 0.20M), "Type?", "Gold")
                    .ElseIf(c => c.Model.Type == "Silver", s => s.Action("10%", ctx => ctx.Model.Discount = .10M), "Silver")
                    .Else(e => e.Action("Other", ctx => ctx.Model.Discount = .30M));

            var context = new BaseTestsModel() { Type = "Gold" };
            flow.Execute(context);
            Assert.IsTrue(context.Discount == .20M, "Gold discount is 20%");

            context = new BaseTestsModel() { Type = "Silver" };
            flow.Execute(context);
            Assert.IsTrue(context.Discount == .10M, "Silver discount is 10%");

            context = new BaseTestsModel() { Type = "Steel" };
            flow.Execute(context);
            Assert.IsTrue(context.Discount == .30M, "Steel discount is 30%");
        }

        /// <summary>
        /// Executes the discount flow but making use of the context variable to set the discounts per type
        /// </summary>
        [TestMethod]
        public void BaseTests_ApplyDiscount_FromVariables()
        {
            var flow = new Flow<BaseTestsModel>();

            flow.Actions
                    .If(c => c.Model.Type == "Gold", g => g.Action("20%", ctx => ctx.Model.Discount = ctx.Variables.Get<Decimal>("GoldDiscount")), "Type?", "Gold")
                    .ElseIf(c => c.Model.Type == "Silver", s => s.Action("10%", ctx => ctx.Model.Discount = ctx.Variables.Get<Decimal>("SilverDiscount")), "Silver")
                    .Else(e => e.Action("Other", ctx => ctx.Model.Discount = ctx.Variables.Get<Decimal>("OtherDiscount")));

            var context = new FlowContext<BaseTestsModel>(new BaseTestsModel());
            context.Variables.Set("GoldDiscount", .20M);
            context.Variables.Set("SilverDiscount", .10M);
            context.Variables.Set("OtherDiscount", .30M);

            context.Model.Type = "Gold";
            flow.Execute(context);
            Assert.IsTrue(context.Model.Discount == .20M, "Gold discount is 20%");

            context.Model.Type = "Silver";
            flow.Execute(context);
            Assert.IsTrue(context.Model.Discount == .10M, "Silver discount is 10%");

            context.Model.Type = "Other";
            flow.Execute(context);
            Assert.IsTrue(context.Model.Discount == .30M, "Steel discount is 30%");
        }


        /// <summary>
        /// Model used for this set of tests
        /// </summary>
        public class BaseTestsModel
        {
            public String Type { get; set; }
            public Decimal Discount { get; set; }
        }
    }
}
