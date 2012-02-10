using Microsoft.FSharp.Math;

namespace Generator.Core.Operations
{
	public class DivideOperation<T> : BinaryOperation<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		public DivideOperation() { }

		public DivideOperation( Operation<T> left, Operation<T> right ) : base( left, right ) { }

		public override double Priority
		{
			get { return (int)OperationPriority.MultiplicationDivision; }
		}

		protected override T EvaluateCore( T value1, T value2 )
		{
			IFractional<T> fractional = math as IFractional<T>;
			if ( fractional != null )
			{
				return fractional.Divide( value1, value2 );
			}
			else
			{
				IIntegral<T> integral = (IIntegral<T>)math;
				return integral.Divide( value1, value2 );
			}
		}

		public override string OperationText
		{
			get { return "÷"; }
		}

		public override BinaryOperation<T> CloneCore()
		{
			return new DivideOperation<T>();
		}
	}
}