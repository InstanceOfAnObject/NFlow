using NFlow.Core.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Tracing
{
    public class TracingProvidersManager<T> : ITracingProvider<T>
    {
        private List<ITracingProvider<T>> providers = new List<ITracingProvider<T>>();

        public TracingProvidersManager()
        {

        }

        public void Register(ITracingProvider<T> provider)
        {
            if (provider == null)
                throw new ArgumentNullException("Tracing provider cannot be null");

            providers.Add(provider);
        }

        public void OnActionExecuting(IFlowAction<T> action)
        {
            foreach (var p in providers)
            {
                p.OnActionExecuting(action);
            }
        }

        public void OnActionExecuted(IFlowAction<T> action)
        {
            foreach (var p in providers)
            {
                p.OnActionExecuted(action);
            }
        }

        public void OnFlowStarting(Flow<T> flow)
        {
            foreach (var p in providers)
            {
                p.OnFlowStarting(flow);
            }
        }

        public void OnFlowFinished(Flow<T> flow)
        {
            foreach (var p in providers)
            {
                p.OnFlowFinished(flow);
            }
        }
    }
}
