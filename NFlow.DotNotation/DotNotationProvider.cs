using NFlow.Core;
using NFlow.Core.Actions;
using NFlow.Core.Actions.Core;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public class DotNotationProvider<T>
    {
        public DotNotationProvider(DotNotationSettings settings = null)
        {
            this.Settings = settings;
        }

        public DotNotationSettings Settings { get; set; } = null;

        public string Convert(Flow<T> flow)
        {
            DotNotationSettings _settings = this.Settings == null ? DotNotationSettings.Default : this.Settings;

            var notations = ReadFlow(flow.Actions).Nodes;

            var result = ToDotNotation(notations, Settings);

            return  $"digraph G {{\n" +
                    $"compound=true;\n" +   // allow links between edges
                    $"{result.Nodes}\n" +   // declare all the nodes and subgraphs
                    $"{result.Links}\n" +   // links must always be declared all together at the end; including links within subgraphs
                    $"}}";
            }

        private (List<DotNode<T>> Nodes, List<DotNode<T>> TerminalNodes) ReadFlow(FlowActions<T> actions)
        {
            var _nodes = new List<DotNode<T>>();
            List<DotNode<T>> prevNodes = null;

            foreach (var action in actions)
            {
                DotNode<T> dot = null;  // current node dot notation

                if (action is ActionsGroupAction<T>)
                {
                    // assumes that ActionsGroup only has one inner flow

                    dot = DotNode<T>.FromGenericNotation(((IDefineSplitNotation<T>)action).ToNotation().Action);

                    var innerFlows = ((IDefineSplitNotation<T>)action).ToNotation().InnerFlows;
                    if (innerFlows != null && innerFlows.Keys.Count == 1)
                    {
                        var nodes = ReadFlow(innerFlows.Keys.First());

                        dot.SubNodes.AddRange(nodes.Nodes);

                        // set the parent node(s)
                        if (prevNodes != null && (nodes.Nodes != null || nodes.Nodes.Count > 0))
                            nodes.Nodes.First().ParentNodes.AddRange(DotNodeReference<T>.FromDotNodeList(prevNodes));

                        // set the last node as one of the previous for the next
                        if (nodes.Nodes != null || nodes.Nodes.Count > 0)
                            prevNodes = nodes.TerminalNodes;
                    }
                    else
                        throw new Exception("Invalid Action Group configuration");
                }
                else if (action is IDefineSplitNotation<T>)
                {
                    dot = DotNode<T>.FromGenericNotation(((IDefineSplitNotation<T>)action).ToNotation().Action);
                    // set parent node(s)
                    if (prevNodes != null)
                    {
                        dot.ParentNodes.AddRange(DotNodeReference<T>.FromDotNodeList(prevNodes));
                    }

                    // handle the inner flows
                    var innerFlows = ((IDefineSplitNotation<T>)action).ToNotation().InnerFlows;
                    prevNodes = new List<DotNode<T>>();

                    if (innerFlows != null)
                    {
                        var innerPrevNode = dot;
                        foreach (var flowKey in innerFlows.Keys)
                        {
                            var nodes = ReadFlow(flowKey);

                            _nodes.AddRange(nodes.Nodes);

                            // set the first node parent as the main split node
                            if (nodes.Nodes != null && nodes.Nodes.Count > 0)
                                nodes.Nodes.First().ParentNodes.Add(DotNodeReference<T>.FromDotNode(dot, innerFlows[flowKey]));

                            // set the last node as one of the previous for the next
                            if (nodes.Nodes != null && nodes.Nodes.Count > 0)
                                prevNodes.AddRange(nodes.TerminalNodes);
                        }
                    }
                }
                else if (action is IDefineSimpleNotation)
                {
                    var nAction = (IDefineSimpleNotation)action;
                    dot = DotNode<T>.FromGenericNotation(nAction);

                    // set parent node(s)
                    if(prevNodes != null)
                        dot.ParentNodes.AddRange(DotNodeReference<T>.FromDotNodeList(prevNodes));

                    // finish off by setting this node as the previous of the next one(s)
                    prevNodes = new List<DotNode<T>>() { dot };
                }
                else
                {
                    dot = DotNode<T>.GetDefaultRepresentation(action);

                    // set parent node(s)
                    dot.ParentNodes.AddRange(DotNodeReference<T>.FromDotNodeList(prevNodes));

                    // finish off by setting this node as the previous of the next one(s)
                    prevNodes = new List<DotNode<T>>() { dot };
                }

                if(dot != null)
                    _nodes.Add(dot);
            }

            return (_nodes, prevNodes);
        }

        private (string Nodes, string Links) ToDotNotation(List<DotNode<T>> nodes, DotNotationSettings settings = null)
        {
            if (settings == null)
                settings = DotNotationSettings.Default;

            String strSettings = $"fontsize={settings.FontSize.ToString()} margin={settings.Margin}";

            StringBuilder dotSb = new StringBuilder();
            StringBuilder linkSb = new StringBuilder();

            foreach (var node in nodes)
            {
                if (node.SubNodes.Count > 0)
                {
                    // cluster id is based on the group node id, replacing the guid - by _
                    dotSb.AppendLine($"subgraph cluster_{node.Id.ToString().Replace('-', '_')} {{");
                    dotSb.AppendLine($"    label = \"{node.Text}\"");

                    var subNotation = ToDotNotation(node.SubNodes, settings);

                    dotSb.AppendLine(subNotation.Nodes);
                    linkSb.AppendLine(subNotation.Links);

                    dotSb.AppendLine("}");
                }
                else
                {
                    dotSb.AppendLine(GetObjectDotDeclaration(node, settings));
                    linkSb.AppendLine(GetLinkDotDeclaration(node, settings));
                }

            }

            dotSb.AppendLine(); // just a separation line before declaring the links

            return (dotSb.ToString(), linkSb.ToString());
        }

        private String GetObjectDotDeclaration(DotNode<T> node, DotNotationSettings settings)
        {
            var shape = node.Shape == null ? DotShapes.Box : node.Shape;
            var text = String.IsNullOrEmpty(node.Text) ? node.GetType().Name : node.Text;
            String strSettings = $"fontsize={settings.FontSize.ToString()} margin={settings.Margin}";

            return $"\"{node.Id}\" [shape=\"{shape.ToString()}\" label=\"{text}\" tooltip=\"{text}\" {strSettings}]";
        }

        private String GetLinkDotDeclaration(DotNode<T> node, DotNotationSettings settings)
        {
            String strSettings = $"fontsize={settings.FontSize.ToString()} margin={settings.Margin}";

            var sb = new StringBuilder();

            foreach (var nodeRef in node.ParentNodes)
            {
                sb.AppendLine($"\"{nodeRef.DotNode.Id.ToString()}\" -> \"{node.Id.ToString()}\" [label=\"{nodeRef.Name}\" {strSettings}]");
            }

            return sb.ToString();
        }

    }
}
