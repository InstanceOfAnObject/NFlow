using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public class RuleContext
    {
        public RuleContext()
        {
        }


        /// <summary>
        /// Variable bag to be used in the execution
        /// </summary>
        Dictionary<string, object> Variables = new Dictionary<string, object>();

        /// <summary>
        /// Description of the tasks executed
        /// </summary>
        List<string> Audit = new List<string>();
    }
}
