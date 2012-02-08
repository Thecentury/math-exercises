using Generator.Core.Operations;
using Microsoft.FSharp.Math;

namespace Generator.Core.Constraints
{
	public sealed class NotVerbatimConstantConstraint<T> : IConstraint<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();
		private readonly T _unexpectedvalue;

		public NotVerbatimConstantConstraint(T unexpectedvalue)
		{
			_unexpectedvalue = unexpectedvalue;
		}

		public bool Passes( GenerationContext<T> ctx, Operation<T> op )
		{
			Number<T> number = op as Number<T>;
			if ( number == null )
			{
				return true;
			}

			bool equals = math.Equals( number.Value, _unexpectedvalue );
			return !equals;
		}
	}
}