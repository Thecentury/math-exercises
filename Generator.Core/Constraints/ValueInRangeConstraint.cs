using System;
using Generator.Core.Operations;
using JetBrains.Annotations;

namespace Generator.Core.Constraints
{
	public sealed class ValueInRangeConstraint<T> : IConstraint<T>
	{
		private readonly Range<T> _range;

		public ValueInRangeConstraint( [NotNull] Range<T> range )
		{
			if ( range == null )
			{
				throw new ArgumentNullException( "range" );
			}
			_range = range;
		}

		public ValueInRangeConstraint( T minValue, T maxValue ) : this( new Range<T>( minValue, maxValue ) ) { }

		public bool Passes( GenerationContext<T> ctx, Operation<T> op )
		{
			T value = op.Evaluate();
			bool passes = _range.Includes( value );
			return passes;
		}
	}
}