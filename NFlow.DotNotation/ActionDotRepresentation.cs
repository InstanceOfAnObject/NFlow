using NFlow.Core.Actions.Base;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public class ActionDotRepresentation<T>
    {

        /// <summary>
        /// Get a default representation for a IFlowAction. Usefull for IFlowActions that don't support Dot Notation (dont't implement ISupportDotNotation).
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ActionDotRepresentation<T> GetDefaultRepresentation(IFlowActionDescriptor action)
        {
            return new ActionDotRepresentation<T>(action.Id, action.GetType().Name, DotShapes.Box);
        }

        public static ActionDotRepresentation<T> FromGenericNotation(IDefineSimpleNotation notation)
        {
            return new ActionDotRepresentation<T>(notation.Id, notation.Text, DotShape.FromGenericType(notation.NotationObjectType));
        }

        public Guid Id { get; private set; }
        public String Text { get; private set; }
        public DotShape Shape { get; private set; }

        public List<ActionDotRepresentation<T>> SubActions { get; } = new List<ActionDotRepresentation<T>>();
        public List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, String name)> SubActionsLinks { get; } = new List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, string name)>();

        public ActionDotRepresentation(Guid id)
        {
            this.Id = id;
        }

        public ActionDotRepresentation(Guid id, String text) : this(id)
        {
            this.Text = text;
        }

        public ActionDotRepresentation(Guid id, String text, DotShape shape) : this(id, text)
        {
            this.Shape = shape;
        }

    }
}
