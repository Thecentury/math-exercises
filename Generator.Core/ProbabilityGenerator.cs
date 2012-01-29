using System;

namespace Generator.Core
{
	public sealed class ProbabilityGenerator : IRandomNumberGenerator<double>
	{
		private readonly Random _rnd = new Random();

		public double Generate( double min, double max )
		{
			double value = _rnd.NextDouble();
			double result = min + value * (max - min);
			return result;
		}
	}
}