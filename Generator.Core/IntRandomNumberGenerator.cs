using System;

namespace Generator.Core
{
	public sealed class IntRandomNumberGenerator : IRandomNumberGenerator<int>
	{
		private readonly Random _rnd = new Random();

		public int Generate( Range<int> range )
		{
			int value = _rnd.Next( range.MinValue, range.MaxValue );
			return value;
		}
	}
}