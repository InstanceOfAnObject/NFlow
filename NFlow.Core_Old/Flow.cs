using NFlow.Core.Global;
using NFlow.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlow.Core.Middlewares;

namespace NFlow.Core
{
    public sealed class Flow<T>
    {
        private Guid _id = Guid.NewGuid();

        private Boolean isSubFlow = false;
        private FlowContext<T> Context { get; set; }
        public MiddlewareManager<T> TracingProviders { get; } = new MiddlewareManager<T>();
        public FlowActions<T> Actions { get; } = new FlowActions<T>();
        public FlowVariables DefaultVariables { get; } = new FlowVariables();

        public Guid Id => _id;

        public Flow()
        {
            Actions = new FlowActions<T>();
        }

        /// <summary>
        /// Trigger the execution of the flow passing a model
        /// </summary>
        /// <param name="context"></param>
        public void Execute(T context)
        {
            Execute(new FlowContext<T>(context));
        }

        /// <summary>
        /// Trigger the execution of the flow passing an existing context
        /// </summary>
        /// <param name="context"></param>
        public void Execute(FlowContext<T> context)
        {
            this.Context = context;
            this.Context.Variables.Merge(DefaultVariables, false);
            
            foreach (var action in Actions)
            {
                TracingProviders.OnActionExecuting(action);

                action.Execute(Context);

                TracingProviders.OnActionExecuted(action);
            }
        }

        /// <summary>
        /// Register a flow middleware
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public Flow<T> Register(IFlowMiddleware<T> middleware)
        {
            TracingProviders.Register(middleware);
            return this;
        }
    }
}
