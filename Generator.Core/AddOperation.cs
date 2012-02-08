using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public class AddOperation<T> : BinaryOperation<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		public AddOperation() { }

		public AddOperation( Operation<T> operand1, Operation<T> operand2 ) : base( operand1, operand2 ) { }

		protected sealed override T EvaluateCore( T value1, T value2 )
		{
			return math.Add( value1, value2 );
		}

		public sealed override string OperationText
		{
			get { return "+"; }
		}

		public sealed override BinaryOperation<T> CloneCore()
		{
			return new AddOperation<T>();
		}

		public sealed override double Priority
		{
			get { return (int)OperationPriority.AdditionSubtraction; }
		}
	}
}