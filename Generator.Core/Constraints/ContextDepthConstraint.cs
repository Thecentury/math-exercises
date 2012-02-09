namespace Generator.Core.Constraints
{
	public sealed class ContextDepthConstraint<T> : DelegateCompositeConstraint<T>
	{
		public ContextDepthConstraint( int depth, IConstraint<T> inner )
			: base( ( ctx, op ) => ctx.CurrentDepth == depth, inner )
		{ }
	}
}