using System;

namespace Generator.Core
{
	public sealed class IntegralPositiveMultiplicationGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate( GenerationContext<int> context )
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			context.PushConstraint( new NotVerbatimConstantConstraint<int>( 0 ) );
			context.PushConstraint( new NotVerbatimConstantConstraint<int>( 1 ) );

			var left = context.ParentGenerator.Generate(
				context
				.CloneWithMaxComplexity( leftComplexity )
				.CloneWithRange( Range.Create( 1, 9 ) ) );

			var right = context.ParentGenerator.Generate(
				context
					.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( Range.Create( 1, 9 ) ) );

			context.PopConstraint();
			context.PopConstraint();

			var result = new MultiplyOperation<int>( left, right );
			return result;
		}

		public override double Complexity
		{
			get { return Complexities.MultiplicationComplexity; }
		}
	}
}