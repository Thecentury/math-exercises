using JetBrains.Annotations;

namespace Generator.Core.Generators
{
	public sealed class IntExerciseGenerator : ExerciseGenerator<int>
	{
		public IntExerciseGenerator( IRandomNumberGenerator<double> probabilityGenerator, IRandomNumberGenerator<int> numberGenerator, double maxComplexity,
		                             [NotNull] params IOperationGenerator<int>[] generators )
			: base( probabilityGenerator, numberGenerator, maxComplexity, generators ) { }
	}
}