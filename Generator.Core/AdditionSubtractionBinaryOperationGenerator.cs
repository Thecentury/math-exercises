using System;
using JetBrains.Annotations;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class AdditionGenerator : IOperationGenerator<int>
	{
		public Operation<int> Generate( GenerationContext<int> context )
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			Range<int> leftRange = new Range<int>( context.ExpressionRange.MinValue, context.ExpressionRange.MaxValue - 1 );
			var left = context.ParentGenerator.Generate(
				context
				.CloneWithMaxComplexity( leftComplexity )
				.CloneWithRange( leftRange ) );

			var leftValue = left.Evaluate();
			Range<int> rightRange = context.TermRange.Intersect( context.ExpressionRange - leftValue );

			var right = context.ParentGenerator.Generate(
				context.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var result = new AddOperation( left, right );
			return result;
		}

		public double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}
	}

	public sealed class SubtractionGenerator : IOperationGenerator<int>
	{
		public Operation<int> Generate( GenerationContext<int> context )
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			Range<int> leftRange = new Range<int>( context.ExpressionRange.MinValue, context.ExpressionRange.MaxValue );
			var left = context.ParentGenerator.Generate(
				context
				.CloneWithMaxComplexity( leftComplexity )
				.CloneWithRange( leftRange ) );

			var leftValue = left.Evaluate();
			Range<int> rightRange = context.TermRange.Intersect( Range.From( leftValue ) - context.ExpressionRange );
			var right = context.ParentGenerator.Generate(
				context.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var rightValue = right.Evaluate();

			var result = new SubtractOperation( left, right );
			return result;
		}

		public double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}
	}
}