using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public interface IRule
    {
        string Name { get; set; }

        void AddOperation(IOperation operation);
        IReadOnlyList<IOperation> Operations { get; }

        void Execute();
    }
}
