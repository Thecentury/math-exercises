namespace Generator.Core
{
	public sealed class SubstractOperation : BinaryOperation<int>
	{
		public SubstractOperation() { }

		public SubstractOperation( Operation<int> operand1, Operation<int> operand2 ) : base( operand1, operand2 ) { }

		protected override int EvaluateCore( int value1, int value2 )
		{
			return value1 - value2;
		}

		public override string OperationText
		{
			get { return "-"; }
		}

		public override BinaryOperation<int> CloneCore()
		{
			return new SubstractOperation();
		}
	}
}