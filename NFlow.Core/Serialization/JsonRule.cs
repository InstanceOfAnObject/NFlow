using System;
using System.Collections.Generic;

namespace NFlow.Core.Serialization
{
    public class JsonRule
    {
        public JsonRule()
        {
        }

        public string Name
        {
            get;
            set;
        }

        public List<JsonOperation> Operations
        {
            get;
            set;
        }
    }
}
