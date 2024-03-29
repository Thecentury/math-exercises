using System.Linq;
using Generator.Core.Operations;

namespace Generator.Core
{
	public static class GeneratorContextExtensions
	{
		public static T NextValue<T>( this GenerationContext<T> context )
		{
			return context.NumberGenerator.Generate( context.ExpressionRange );
		}

		public static Operation<T>[] Generate<T>( this GenerationContext<T> context, int count )
		{
			Operation<T>[] result = new Operation<T>[count];

			var probabilities = Enumerable.Range( 0, count )
				.Select( _ => context.ProbabilityGenerator.GetProbability() )
				.ToList();

			double probabilitiesSum = probabilities.Sum();

			for ( int i = 0; i < count; i++ )
			{
				double complexity = context.MaxComplexity * probabilities[i] / probabilitiesSum;
				var contextClone = context.CloneWithMaxComplexity( complexity );
				var op = context.ParentGenerator.Generate( contextClone );
				result[i] = op;
			}

			return result;
		}
	}
}