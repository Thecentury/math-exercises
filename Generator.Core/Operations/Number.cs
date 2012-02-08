namespace Generator.Core.Operations
{
	public class Number<T> : Operation<T>
	{
		private readonly T _value;

		public Number( T value )
		{
			_value = value;
		}

		public T Value
		{
			get { return _value; }
		}

		public override T Evaluate()
		{
			return _value;
		}

		public override string Text
		{
			get { return _value.ToString(); }
		}

		public override double Priority
		{
			get { return (int) OperationPriority.Number; }
		}
	}
}