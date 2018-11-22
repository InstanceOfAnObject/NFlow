using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class Flow
    {
        List<IContinuation> continuations;

        /// <summary>
        /// Simple way to create a new Flow insntance
        /// </summary>
        /// <returns></returns>
        public static Flow New()
        {
            return new Flow();
        }

        public Flow()
        {
            continuations = new List<IContinuation>();
        }

        internal IRule Rule { get; set; }

        public void AddContinuation(IContinuation continuation)
        {
            continuations.Add(continuation);
        }

        public ReadOnlyCollection<IContinuation> Continuations => continuations.AsReadOnly();

        public IRule End()
        {
            return Rule;
        }

        public async Task Execute(RuleContext context)
        {
            foreach (var continuation in this.Continuations)
            {
                await continuation.Invoke(context);
            }
        }
    }
}
