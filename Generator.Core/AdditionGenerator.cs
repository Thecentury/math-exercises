using System;
using JetBrains.Annotations;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class AdditionGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate( GenerationContext<int> context )
		{
			double leftComplexity = context.ProbabilityGenerator.GetProbability() * context.MaxComplexity;
			double rightComplexity = context.MaxComplexity - leftComplexity;

			Range<int> leftRange = context.TermRange.Intersect( context.ExpressionRange - context.TermRange );
			var left = context.ParentGenerator.Generate(
				context
				.CloneWithMaxComplexity( leftComplexity )
				.CloneWithRange( leftRange ) );

			var leftValue = left.Evaluate();
			Range<int> rightRange = context.TermRange.Intersect( context.ExpressionRange - leftValue );

			var right = context.ParentGenerator.Generate(
				context.CloneWithMaxComplexity( rightComplexity )
					.CloneWithRange( rightRange ) );

			var result = new AddOperation<int>( left, right );
			return result;
		}

		public override double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}

		public override bool CanGenerate( GenerationContext<int> context )
		{
			if ( context.ExpressionRange.MaxValue <= context.TermRange.MinValue )
			{
				return false;
			}

			bool twoTermsIntersectsWithExpressionRange =
				( context.TermRange + context.TermRange ).IntersectsWith( context.ExpressionRange );

			if ( !twoTermsIntersectsWithExpressionRange )
			{
				return false;
			}

			return base.CanGenerate( context );
		}
	}
}