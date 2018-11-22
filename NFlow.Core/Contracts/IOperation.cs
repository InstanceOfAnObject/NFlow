using System;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public interface IOperation
    {
        /// <summary>
        /// Defines the operation config. Each operation will have its own implementation of this interface.
        /// If the operation doesn't require any configuration, this should return null.
        /// </summary>
        /// <value>The config.</value>
        IOperationConfig Config { get; set; }

        /// <summary>
        /// Executes the operation
        /// </summary>
        Task Invoke(RuleContext context);
    }
}
