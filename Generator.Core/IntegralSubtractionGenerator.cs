namespace Generator.Core
{
	public sealed class IntegralSubtractionGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate( GenerationContext<int> context )
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			Range<int> leftRange = context.TermRange.Intersect( context.ExpressionRange + context.TermRange );
			var left = context.ParentGenerator.Generate(
				context
					.CloneWithMaxComplexity( leftComplexity )
					.CloneWithRange( leftRange ) );

			var leftValue = left.Evaluate();
			Range<int> rightRange = context.TermRange.Intersect( Range.From( leftValue ) - context.ExpressionRange );
			var right = context.ParentGenerator.Generate(
				context
					.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var result = new SubtractOperation<int>( left, right );
			return result;
		}

		public override bool CanGenerate( GenerationContext<int> context )
		{
			bool differenceOfTwoTermsIntersectsWithExpressionRange =
				(context.TermRange - context.TermRange).IntersectsWith(context.ExpressionRange);

			if ( !differenceOfTwoTermsIntersectsWithExpressionRange )
			{
				return false;
			}

			return base.CanGenerate( context );
		}

		public override double Complexity
		{
			get { return Complexities.SubtractionComplexity; }
		}
	}
}