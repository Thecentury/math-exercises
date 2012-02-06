namespace Generator.Core
{
	public sealed class SubtractionGenerator : OperationGeneratorBase<int>
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
				context.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var result = new SubtractOperation( left, right );
			return result;
		}

		public override double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}
	}
}