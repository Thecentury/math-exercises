namespace Generator.Core
{
	public sealed class Brackets : Operation<int>
	{
		private readonly Operation<int> _inner;

		public Brackets( Operation<int> inner )
		{
			_inner = inner;
		}

		public override int Evaluate()
		{
			var value = Inner.Evaluate();
			return value;
		}

		public override string Text
		{
			get
			{
				string text = string.Format( "({0})", Inner.Text );
				return text;
			}
		}

		public override double Priority
		{
			get { return (int)OperationPriority.Brackets; }
		}

		public override int Depth
		{
			get
			{
				return _inner.Depth + 1;
			}
		}

		public Operation<int> Inner
		{
			get { return _inner; }
		}
	}
}