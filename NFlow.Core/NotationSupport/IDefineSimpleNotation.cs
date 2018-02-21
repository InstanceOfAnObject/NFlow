using NFlow.Core.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.NotationSupport
{
    /// <summary>
    /// Notation support to be used on actions that follow a one to one connection with the next action.
    /// </summary>
    public interface IDefineSimpleNotation : IFlowActionDescriptor
    {
        String Text { get; set; }

        NotationObjectTypes NotationObjectType { get; set; }
    }
}
