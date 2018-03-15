using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFlow.Core.Actions.Base;
using NFlow.Core.NotationSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlow.Core.Actions
{
    public class FileReaderAction<T> : BaseFlowAction<T>
    {
        String _filePath = null;
        FileReaderActionSupportedTypes _fileType = FileReaderActionSupportedTypes.JSON;
        String _variable = null;

        public FileReaderAction(String filepah, FileReaderActionSupportedTypes fileType, String variable)
        {
            this._filePath = filepah;
            this._fileType = fileType;
            this._variable = variable;
        }

        public override string Text { get; set; } = "Read file";
        public override NotationObjectTypes NotationObjectType { get; set; } = NotationObjectTypes.Action;
        
        public override void Execute(FlowContext<T> context)
        {
            if (this._fileType == FileReaderActionSupportedTypes.JSON)
            {
                var obj = ReadJSON(this._filePath);
                context.Variables.Set(_variable, obj);
            }
        }

        private dynamic ReadJSON(String filePath)
        {
            dynamic json = JObject.Parse(File.ReadAllText(filePath));
            return json;
        }

    }

    /// <summary>
    /// List of supported file types
    /// </summary>
    public enum FileReaderActionSupportedTypes : int
    {
        JSON
    }
}
