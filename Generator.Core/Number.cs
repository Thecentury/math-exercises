using System.Globalization;

namespace Generator.Core
{
	public sealed class Number : Operation<int>
	{
		private readonly int _value;

		public Number( int value )
		{
			_value = value;
		}

		public override int Evaluate()
		{
			return _value;
		}

		public override string Text
		{
			get { return _value.ToString( CultureInfo.InvariantCulture ); }
		}
	}

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

		public Operation<int> Inner
		{
			get { return _inner; }
		}
	}
}