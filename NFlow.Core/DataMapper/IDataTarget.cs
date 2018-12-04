using System;
using System.Collections.Generic;
using System.Text;

namespace NFlow.Core.DataMapper
{
    public interface IDataTarget
    {
        object GetOutput(object input);
    }
}
