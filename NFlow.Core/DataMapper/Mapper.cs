using System;
using System.Collections.Generic;
using System.Text;

namespace NFlow.Core.DataMapper
{
    public class Mapper
    {
        /// <summary>
        /// Maps a give source data into a desired target data.
        /// The process is done by converting the source data in a commong format, 
        /// mapping it into the desired format still in the common format and then 
        /// converting it into the desired target format.
        /// </summary>
        /// <param name="source">The source of data</param>
        /// <param name="target">The desired output data</param>
        /// <param name="config">Defines how the mapping is to be done</param>
        /// <returns></returns>
        public static object Map(IDataSource source, IDataTarget target, MappingConfig config)
        {
            var transformation = Transform(source.GetInput(), config);
            var output = target.GetOutput(transformation);

            return output;
        }

        /// <summary>
        /// Takes an object and converts it into another based on a provided configuration
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="config">Mapping configuration</param>
        /// <returns></returns>
        public static dynamic Transform(object source, MappingConfig config)
        {
            return null;
        }

        protected Mapper() { }
        

    }
}
