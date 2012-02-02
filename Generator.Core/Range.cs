using System;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class Range<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		private readonly T _minValue;
		private readonly T _maxValue;

		public Range( T minValue, T maxValue )
		{
			_minValue = minValue;
			_maxValue = maxValue;
		}

		public static Range<int> IntInfinite()
		{
			return new Range<int>( Int32.MinValue, Int32.MaxValue );
		}

		public static Range<T> Infinite()
		{
			IIEEE<T> floatMath = (IIEEE<T>)math;

			return new Range<T>( floatMath.NegativeInfinity, floatMath.PositiveInfinity );
		}

		public T MinValue
		{
			get { return _minValue; }
		}

		public T MaxValue
		{
			get { return _maxValue; }
		}

		public bool Includes( T value )
		{
			bool greater = math.Compare( _minValue, value ) <= 0;
			bool less = math.Compare( value, _maxValue ) <= 0;
			return greater && less;
		}
	}
}