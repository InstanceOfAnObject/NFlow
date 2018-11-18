using System;
namespace NFlow.Core.Operations
{
    public class IfOperation : IOperation
    {
        public IfOperation()
        {
        }

        public IfOperation(Func<RuleContext, bool> condition, IOperation t, IOperation f)
        {
            Config = new IfOperationConfig() { Condition = condition, TrueOperation = t, FalseOperation = f };
        }

        public IOperationConfig Config { get; set; }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public class IfOperationConfig : IOperationConfig
    {
        public Func<RuleContext,bool> Condition
        {
            get;
            set;
        }

        public IOperation TrueOperation
        {
            get;
            set;
        }

        public IOperation FalseOperation
        {
            get;
            set;
        }
    }
}
