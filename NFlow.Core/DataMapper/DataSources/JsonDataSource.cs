using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFlow.Core.DataMapper.DataSources
{
    public class JsonDataSource : IDataSource
    {
        readonly string inputJson;
        public JsonDataSource(string json)
        {
            inputJson = json;   
        }

        public object GetInput()
        {
            return JsonConvert.DeserializeObject(inputJson);
        }
    }
}
