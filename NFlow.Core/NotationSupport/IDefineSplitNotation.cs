using NFlow.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.NotationSupport
{
    /// <summary>
    /// Notation support to be used on actions that split the flow (one to many relation).
    /// An example are conditions or parallel execution actions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDefineSplitNotation<T> : IDefineSimpleNotation
    {

        (IDefineSimpleNotation Action, Dictionary<String, FlowActions<T>> InnerFlows) ToNotation();

    }
}
