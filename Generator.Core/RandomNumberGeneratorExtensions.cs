namespace Generator.Core
{
	public static class RandomNumberGeneratorExtensions
	{
		public static double GetProbability( this IRandomNumberGenerator<double> generator )
		{
			return generator.Generate( 0, 1 );
		}

		public static bool GetBool( this IRandomNumberGenerator<double> generator )
		{
			var randomValue = generator.GetProbability();
			return randomValue > 0.5;
		}

		public static bool GetBool( this IRandomNumberGenerator<int> generator )
		{
			var randomValue = generator.Generate( 0, 2 );
			return randomValue == 1;
		}
	}
}