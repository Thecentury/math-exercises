namespace Generator.Core
{
	public abstract class Operation<T>
	{
		public abstract T Evaluate();

		public abstract string Text { get; }

		public abstract double Priority { get; }

		public override string ToString()
		{
			return Text;
		}

		public static Operation<T> operator +( Operation<T> left, Operation<T> right )
		{
			return new AddOperation<T>( left, right );
		}

		public static Operation<T> operator -( Operation<T> left, Operation<T> right )
		{
			return new SubtractOperation<T>( left, right );
		}
	}

	public enum OperationPriority
	{
		AdditionSubtraction,
		MultiplicationDivision,
		Number,
		Brackets
	}
}