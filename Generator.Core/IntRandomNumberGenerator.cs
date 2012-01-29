using System;

namespace Generator.Core
{
	public sealed class IntRandomNumberGenerator : IRandomNumberGenerator<int>
	{
		private readonly Random _rnd = new Random();

		public int Generate( int min, int max )
		{
			int value = _rnd.Next( min, max );
			return value;
		}
	}
}