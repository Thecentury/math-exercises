using System;

namespace Generator.Core
{
	public sealed class IntegralPositiveMultiplicationGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate(GenerationContext<int> context)
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			Range<int> leftRange = context.ExpressionRange;
			var left = context.ParentGenerator.Generate(
				context
				.CloneWithMaxComplexity( leftComplexity )
				.CloneWithRange( leftRange ) );

			Range<int> rightRange = context.ExpressionRange;

			var right = context.ParentGenerator.Generate(
				context.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var result = new MultiplyOperation<int>( left, right );
			return result;
		}

		public override double Complexity
		{
			get { return Complexities.MultiplicationComplexity; }
		}
	}
}