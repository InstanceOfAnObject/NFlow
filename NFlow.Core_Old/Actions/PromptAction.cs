using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlow.Core.Global;
using System.Text.RegularExpressions;
using NFlow.Core.Actions.Base;
using NFlow.Core.NotationSupport;

namespace NFlow.Core.Actions.Core
{
    public class PromptAction<T> : BaseFlowAction<T>
    {
        Guid _id = Guid.NewGuid();

        public override string Text { get; set; }
        public override NotationObjectTypes NotationObjectType { get; set; }

        public PromptAction() { }
        public PromptAction(String text) { this.Text = text; }

        public override void Execute(FlowContext<T> context)
        {
            var reg = new Regex(@"\${([0-9a-zA-Z.]*)}");
            var matches = reg.Matches(Text);

            foreach (Match m in matches)
            {
                var token = m.ToString();
                var variable = m.Groups[1].Value;

                var value = context.Variables.Get(variable.ToString()).ToString();
                Text = Text.Replace(token, value);
            }

            Console.WriteLine(Text);
        }
    }
}
