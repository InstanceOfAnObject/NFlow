using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public interface IRule
    {
        string Name { get; set; }
        
        Flow Flow { get; }

        object this[string name] { get; set; }

        void Execute();

        Task ExecuteAsync();
    }
}
