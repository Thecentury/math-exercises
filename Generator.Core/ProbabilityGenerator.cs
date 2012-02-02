using System;

namespace Generator.Core
{
	public sealed class ProbabilityGenerator : IRandomNumberGenerator<double>
	{
		private readonly Random _rnd = new Random();

		public double Generate( Range<double> range )
		{
			double value = _rnd.NextDouble();
			double result = range.MinValue + value * (range.MaxValue - range.MinValue);
			return result;
		}
	}
}