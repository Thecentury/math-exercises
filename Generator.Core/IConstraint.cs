using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public interface IConstraint<T>
	{
		bool Passes( GenerationContext<T> ctx, Operation<T> op );
	}

	public sealed class InExpressionRangeConstraint<T> : IConstraint<T>
	{
		public bool Passes( GenerationContext<T> ctx, Operation<T> op )
		{
			T value = op.Evaluate();
			bool passes = ctx.ExpressionRange.Includes( value );
			return passes;
		}
	}

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
