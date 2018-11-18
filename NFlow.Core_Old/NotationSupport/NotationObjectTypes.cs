using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.NotationSupport
{
    /// <summary>
    /// List of types agnostic to any specific notation standard.
    /// The export process of each notation is responsible for converting these into notatin specific representations.
    /// </summary>
    public enum NotationObjectTypes : Int32
    {
        Action = 1,
        Decision = 2
    }
}
