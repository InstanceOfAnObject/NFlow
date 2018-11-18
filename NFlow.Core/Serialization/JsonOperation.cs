using System;
namespace NFlow.Core.Serialization
{
    public class JsonOperation
    {
        public JsonOperation()
        {
        }

        public string Type
        {
            get;
            set;
        }

        public IOperationConfig Config
        {
            get;
            set;
        }
    }
}
