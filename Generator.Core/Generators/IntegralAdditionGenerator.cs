using System.Diagnostics;
using Generator.Core.Operations;

namespace Generator.Core.Generators
{
	public sealed class IntegralAdditionGenerator : OperationGeneratorBase<int>
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

			Debug.WriteLine( "{0}, Complexity {1} & {2}", result, leftComplexity, rightComplexity );

			return result;
		}

		public override double Complexity
		{
			get { return Complexities.AdditionComplexity; }
		}

		public override bool CanGenerate( GenerationContext<int> context )
		{
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