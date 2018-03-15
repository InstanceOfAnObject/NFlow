using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions.Extensions
{
    public static class IOExtensions
    {

        public static FlowActions<T> ReadFile<T>(this FlowActions<T> task, String filePath, FileReaderActionSupportedTypes fileType, String variable)
        {

            return task.Add(new FileReaderAction<T>(filePath, fileType, variable));

        }

    }
}
