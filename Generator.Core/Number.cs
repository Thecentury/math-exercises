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

		public override double Complexity
		{
			get { return 0; }
		}
	}
}