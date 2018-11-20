using System;
using System.Collections.Generic;

namespace NFlow.Core
{
    public interface IRule
    {
        string Name { get; set; }

        void AddContinuation(IContinuation continuation);
        IReadOnlyList<IContinuation> Continuations { get; }

        object this[string name] { get; set; }

        void Execute();
    }
}
