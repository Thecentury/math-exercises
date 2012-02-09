using System;
using Generator.Core.Operations;
using JetBrains.Annotations;

namespace Generator.Core.Constraints
{
	public class DelegateCompositeConstraint<T> : IConstraint<T>
	{
		private readonly Func<GenerationContext<T>, Operation<T>, bool> _condition;
		private readonly IConstraint<T> _inner;

		public DelegateCompositeConstraint( [NotNull] Func<GenerationContext<T>, Operation<T>, bool> condition, [NotNull] IConstraint<T> inner )
		{
			if ( condition == null )
			{
				throw new ArgumentNullException( "condition" );
			}
			if ( inner == null )
			{
				throw new ArgumentNullException( "inner" );
			}

			_condition = condition;
			_inner = inner;
		}

		public bool Passes( GenerationContext<T> ctx, Operation<T> op )
		{
			if ( _condition( ctx, op ) )
			{
				bool passes = _inner.Passes( ctx, op );
				return passes;
			}
			else
			{
				return true;
			}
		}
	}
}