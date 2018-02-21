using NFlow.Core;
using NFlow.Core.Actions;
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
            String result = string.Empty;

            var notations = ReadFlow(flow);

            result = ToDotNotation(notations.Objects, notations.Links, Settings);

            return result;
        }

        public string ToDotNotation(
            List<ActionDotRepresentation<T>> objects,
            List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, String name)> links,
            DotNotationSettings settings = null)
        {
            if (settings == null) settings = DotNotationSettings.Default;

            String strSettings = $"fontsize={settings.FontSize.ToString()} margin={settings.Margin}";

            StringBuilder dotSb = new StringBuilder();

            foreach (var obj in objects)
            {
                var shape = obj.Shape == null ? DotShapes.Box : obj.Shape;
                var text = String.IsNullOrEmpty(obj.Text) ? obj.GetType().Name : obj.Text;

                dotSb.AppendLine($"\"{obj.Id}\" [shape=\"{shape.ToString()}\" label=\"{text}\" tooltip=\"{text}\" {strSettings}]");
            }

            dotSb.AppendLine(); // just a separation line before declaring the links

            foreach (var link in links)
            {
                dotSb.AppendLine($"  \"{link.from.Id.ToString()}\" -> \"{link.to.Id.ToString()}\" [label=\"{link.name}\" {strSettings}]");
            }

            return $"digraph G {{\n {dotSb.ToString()} \n}}";
        }

        private (List<ActionDotRepresentation<T>> Objects, List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, String name)> Links) ReadFlow(Flow<T> flow)
        {
            return (GetObjects(flow.Actions), GetLinks(flow.Actions));
        }

        private List<ActionDotRepresentation<T>> GetObjects(FlowActions<T> actions)
        {
            var _objects = new List<ActionDotRepresentation<T>>();

            foreach (var action in actions)
            {
                ActionDotRepresentation<T> dot = null;

                if (action is IDefineSplitNotation<T>)
                {
                    dot = ActionDotRepresentation<T>.FromGenericNotation(((IDefineSplitNotation<T>)action).ToNotation().Action);

                    var innerFlows = ((IDefineSplitNotation<T>)action).ToNotation().InnerFlows;
                    if (innerFlows != null)
                        foreach (var flowKey in innerFlows.Keys)
                        {
                            var objects = GetObjects(innerFlows[flowKey]);
                            _objects.AddRange(objects);
                        }
                }
                else if (action is IDefineSimpleNotation)
                {
                    var nAction = (IDefineSimpleNotation)action;
                    dot = ActionDotRepresentation<T>.FromGenericNotation(nAction);
                }
                else
                    dot = ActionDotRepresentation<T>.GetDefaultRepresentation(action);

                _objects.Add(dot);
            }

            return _objects;
        }

        private List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, String name)> GetLinks(FlowActions<T> actions, ActionDotRepresentation<T> prevNode = null, String linkName = "")
        {
            var _links = new List<(ActionDotRepresentation<T> from, ActionDotRepresentation<T> to, String name)>();
            foreach (var action in actions)
            {
                ActionDotRepresentation<T> currentNode = null;

                if (action is IDefineSplitNotation<T>)
                {
                    currentNode = ActionDotRepresentation<T>.FromGenericNotation(((IDefineSplitNotation<T>)action).ToNotation().Action);

                    var innerFlows = ((IDefineSplitNotation<T>)action).ToNotation().InnerFlows;
                    if (innerFlows != null)
                        foreach (var flowKey in innerFlows.Keys)
                        {
                            var links = GetLinks(innerFlows[flowKey], currentNode, flowKey);
                            _links.AddRange(links);
                        }
                }
                else if (action is IDefineSimpleNotation)
                {
                    var nAction = (IDefineSimpleNotation)action;
                    currentNode = ActionDotRepresentation<T>.FromGenericNotation(nAction);
                }
                else
                    currentNode = ActionDotRepresentation<T>.GetDefaultRepresentation(action);

                if (prevNode != null)
                {
                    _links.Add((prevNode, currentNode, linkName));
                }

                prevNode = currentNode;
                linkName = string.Empty;
            }

            return _links;
        }
    }
}
