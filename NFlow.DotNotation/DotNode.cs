using NFlow.Core.Actions.Base;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public class DotNode<T>
    {
        public DotNode(Guid id)
        {
            this.Id = id;
        }

        public DotNode(Guid id, String text) : this(id)
        {
            this.Text = text;
        }

        public DotNode(Guid id, String text, DotShape shape) : this(id, text)
        {
            this.Shape = shape;
        }

        public Guid Id { get; private set; }
        public String Text { get; private set; }
        public DotShape Shape { get; private set; }

        /// <summary>
        /// Get the list of nodes parent to this one.
        /// Adding items to this list wil create links between this node and the ones on the list.
        /// </summary>
        public List<DotNodeReference<T>> ParentNodes { get; } = new List<DotNodeReference<T>>();

        /// <summary>
        /// Gets the list of sub-nodes.
        /// This list is used in case of this node being a subgraph (a group of nodes, usually represented by a box containing actions inside).
        /// </summary>
        public List<DotNode<T>> SubNodes { get; } = new List<DotNode<T>>();

        public static DotNode<T> GetDefaultRepresentation(IFlowActionDescriptor action)
        {
            return new DotNode<T>(action.Id, action.GetType().Name, DotShapes.Box);
        }

        public static DotNode<T> FromGenericNotation(IDefineSimpleNotation notation)
        {
            return new DotNode<T>(notation.Id, notation.Text, DotShape.FromGenericType(notation.NotationObjectType));
        }
    }
}
