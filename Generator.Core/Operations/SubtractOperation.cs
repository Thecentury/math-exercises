using Microsoft.FSharp.Math;

namespace Generator.Core.Operations
{
	public sealed class SubtractOperation<T> : BinaryOperation<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		public SubtractOperation() { }

		public SubtractOperation( Operation<T> operand1, Operation<T> operand2 ) : base( operand1, operand2 ) { }

		protected override T EvaluateCore( T value1, T value2 )
		{
			return math.Subtract( value1, value2 );
		}

		public override string OperationText
		{
			get { return "-"; }
		}

		public override BinaryOperation<T> CloneCore()
		{
			return new SubtractOperation<T>();
		}

		public override double Priority
		{
			get { return (int)OperationPriority.AdditionSubtraction; }
		}
	}
}