using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public static class DotShapes
    {
        public static DotShape Diamond { get; } = new DotShape("diamond");
        public static DotShape Box { get { return new DotShape("box"); } }
        public static DotShape Oval { get; } = new DotShape("oval");
        public static DotShape Circle { get; } = new DotShape("circle");
        public static DotShape Point { get; } = new DotShape("point");
    }

    public class DotShape
    {
        public static DotShape FromGenericType(NotationObjectTypes genericType)
        {
            DotShape result = null;

            switch (genericType)
            {
                case NotationObjectTypes.Action:
                    result = DotShapes.Box;
                    break;
                case NotationObjectTypes.Decision:
                    result = DotShapes.Diamond;
                    break;
                default:
                    result = DotShapes.Box;
                    break;
            }

            return result;
        }


        String value = String.Empty;

        private DotShape() { }
        public DotShape(String value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value;
        }
    }
}
