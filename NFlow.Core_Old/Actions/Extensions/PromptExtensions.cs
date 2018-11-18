using NFlow.Core.Actions;
using NFlow.Core.Actions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class PromptExtensions
    {

        public static FlowActions<T> WriteLine<T>(this FlowActions<T> task, String text)
        {

            return task.Add(new PromptAction<T>(text));

        }

    }
}
