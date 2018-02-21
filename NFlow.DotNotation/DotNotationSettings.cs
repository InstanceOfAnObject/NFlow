using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.DotNotation
{
    public class DotNotationSettings
    {
        public static DotNotationSettings Default
        {
            get
            {
                return new DotNotationSettings();
            }
        }

        public int FontSize { get; set; } = 10;
        public decimal Margin { get; set; } = 0.05M;
    }
}
