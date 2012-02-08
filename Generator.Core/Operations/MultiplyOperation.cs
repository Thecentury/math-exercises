using Microsoft.FSharp.Math;

namespace Generator.Core.Operations
{
	public class MultiplyOperation<T> : BinaryOperation<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		public MultiplyOperation() { }

		public MultiplyOperation( Operation<T> operand1, Operation<T> operand2 ) : base( operand1, operand2 ) { }

		public sealed override double Priority
		{
			get { return (int)OperationPriority.MultiplicationDivision; }
		}

		protected sealed override T EvaluateCore( T value1, T value2 )
		{
			return math.Multiply( value1, value2 );
		}

		public sealed override string OperationText
		{
			get { return "·"; }
		}

		public sealed override BinaryOperation<T> CloneCore()
		{
			return new MultiplyOperation<T>();
		}
	}
}