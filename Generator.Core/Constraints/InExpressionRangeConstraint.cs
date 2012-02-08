using Generator.Core.Operations;

namespace Generator.Core.Constraints
{
	public sealed class InExpressionRangeConstraint<T> : IConstraint<T>
	{
		public bool Passes( GenerationContext<T> ctx, Operation<T> op )
		{
			T value = op.Evaluate();
			bool passes = ctx.ExpressionRange.Includes( value );
			return passes;
		}
	}
}