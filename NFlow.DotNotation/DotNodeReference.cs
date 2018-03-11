using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public class DotNodeReference<T>
    {
        public static DotNodeReference<T> FromDotNode(DotNode<T> node, String name)
        {
            return new DotNodeReference<T>() { DotNode = node, Name = name };
        }

        public static List<DotNodeReference<T>> FromDotNodeList(List<DotNode<T>> nodes)
        {
            if (nodes == null)
                return null;

            var result = new List<DotNodeReference<T>>();

            foreach (var node in nodes)
            {
                result.Add(new DotNodeReference<T>() { DotNode = node });
            }

            return result;
        }


        public DotNode<T> DotNode { get; set; }

        public String Name { get; set; }

    }
}
