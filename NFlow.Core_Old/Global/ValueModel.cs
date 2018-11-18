using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core
{
    /// <summary>
    /// Base model class to wrap primitive type models.
    /// It can also be used for types other that primitive types but it's not required.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueModel<T>
    {
        public T Value { get; set; }
    }
}
