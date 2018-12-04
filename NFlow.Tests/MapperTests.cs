using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace NFlow.Tests
{
    public class MapperTests
    {
        [Fact]
        public void MapObjects_GetValueByFQN()
        {
            var obj = new TestClass() { Prop1 = 1, Prop2 = "Item 1", Prop3 = new TestClass() { Prop1 = 12 } };

            Assert.True(GetValueByFQN(obj, "Prop1").Equals(1));
            Assert.True(GetValueByFQN(obj, "Prop3.Prop1").Equals(12));
        }

        public void MapObjects_CScript()
        {

        }

        public object GetValueByFQN(object obj, string fqn)
        {
            var props = fqn.Split('.');
            Object value = obj;
            foreach (var p in props)
            {
                value = value.GetType().GetProperty(p).GetValue(value);
            }

            return value;
        }

        public class TestClass
        {
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public TestClass Prop3 { get; set; }
        }
    }
}
