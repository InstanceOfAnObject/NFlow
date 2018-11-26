using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public class Flow : List<IContinuation>
    {

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
        }

        internal IRule Rule { get; set; }

        public IRule End()
        {
            return Rule;
        }

        public async Task Execute(RuleContext context)
        {
            foreach (var continuation in this)
            {
                await continuation.Invoke(context);
            }
        }
    }
}
