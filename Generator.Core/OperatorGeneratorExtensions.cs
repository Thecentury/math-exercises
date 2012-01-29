namespace Generator.Core
{
	public static class OperatorGeneratorExtensions
	{
		public static double GetWeight<T>( this IOperationGenerator<T> generator )
		{
			if ( generator.Complexity == 0 )
				return 0;
			else
				return 1 / generator.Complexity;
		}
	}
}