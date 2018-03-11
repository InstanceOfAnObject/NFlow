using NFlow.Core;
using NFlow.DotNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    public static class ExportFlowToDotNotation
    {

        public static String ToDotNotation<T>(this Flow<T> flow, DotNotationSettings settings = null)
        {
            var provider = new DotNotation.DotNotationProvider<T>(settings);
            return provider.Convert(flow);
        }

    }
}
