using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFlow.Core.Actions.Base;
using NFlow.Core.NotationSupport;

namespace NFlow.Core.Middlewares.Tracing
{
    /// <summary>
    /// Simple implementation of a console tracing provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConsoleMiddleware<T> : IFlowMiddleware<T>
    {
        Dictionary<Guid, (DateTime? Start, DateTime? End)> executionTimes = new Dictionary<Guid, (DateTime? Start, DateTime? End)>();

        public void OnActionExecuting(IFlowAction<T> action)
        {
            var now = DateTime.Now;
            if (!executionTimes.ContainsKey(action.Id))
                executionTimes.Add(action.Id, (null, null));

            executionTimes[action.Id] = (now, null);

            System.Console.WriteLine($"[{now}] Started - {GetActionText(action)}");
        }

        public void OnActionExecuted(IFlowAction<T> action)
        {
            var now = DateTime.Now;
            executionTimes[action.Id] = (executionTimes[action.Id].Start, now);
            
            System.Console.WriteLine($"[{now}] Finished ({(executionTimes[action.Id].End - executionTimes[action.Id].Start).Value.TotalMilliseconds}) - {GetActionText(action)}");
        }

        private String GetActionText(IFlowAction<T> action)
        {
            String value = String.Empty;

            if (action is IDefineSimpleNotation)
            {
                var notation = (IDefineSimpleNotation)action;
                value = $"{notation.Text}";
            }
            else
            {
                value = action.GetType().Name;
            }

            return value;
        }

        public void OnFlowStarting(Flow<T> flow)
        {
           
        }

        public void OnFlowFinished(Flow<T> flow)
        {
            
        }

        public void OnActionException(IFlowAction<T> action)
        {
            
        }
    }
}
