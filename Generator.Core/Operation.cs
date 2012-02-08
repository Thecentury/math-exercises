namespace Generator.Core
{
	public static class Operation
	{
		public static Number<T> From<T>( T value )
		{
			return new Number<T>( value );
		}
	}

	public abstract class Operation<T>
	{
		public abstract T Evaluate();

		public abstract string Text { get; }

		public abstract double Priority { get; }

		public override string ToString()
		{
			return Text;
		}

		public virtual int Depth
		{
			get { return 1; }
		}

		#region Operators

		public static Operation<T> operator +( Operation<T> left, Operation<T> right )
		{
			return new AddOperation<T>( left, right );
		}

		public static Operation<T> operator -( Operation<T> left, Operation<T> right )
		{
			return new SubtractOperation<T>( left, right );
		}

		public static Operation<T> operator *( Operation<T> left, Operation<T> right )
		{
			return new MultiplyOperation<T>( left, right );
		}

		#endregion
	}

	public enum OperationPriority
	{
		AdditionSubtraction,
		MultiplicationDivision,
		Number,
		Brackets
	}
}