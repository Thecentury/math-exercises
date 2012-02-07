using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public static class Range
	{
		public static Range<T> From<T>( T value )
		{
			return new Range<T>( value, value );
		}

		public static Range<T> Create<T>( T minValue, T maxValue )
		{
			return new Range<T>( minValue, maxValue );
		}
	}

	[DebuggerDisplay( "{_minValue} — {_maxValue}" )]
	public sealed class Range<T>
	{
		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		private readonly T _minValue;
		private readonly T _maxValue;

		public Range( T minValue, T maxValue )
		{
			if ( math.Compare( minValue, maxValue ) > 0 )
			{
				throw new ArgumentException( "minValue should be less than maxValue." );
			}

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

		[Pure]
		public static Range<T> operator +( Range<T> range, T value )
		{
			Range<T> result = new Range<T>( math.Add( range.MinValue, value ), math.Add( range.MaxValue, value ) );
			return result;
		}

		[Pure]
		public static Range<T> operator +( Range<T> range1, Range<T> range2 )
		{
			Range<T> result = new Range<T>( math.Add( range1.MinValue, range2.MinValue ), math.Add( range1.MaxValue, range2.MaxValue ) );
			return result;
		}

		[Pure]
		public static Range<T> operator -( Range<T> range )
		{
			Range<T> result = new Range<T>( math.Subtract( math.Zero, range.MaxValue ), math.Subtract( math.Zero, range.MinValue ) );
			return result;
		}

		[Pure]
		public static Range<T> operator -( Range<T> range, T value )
		{
			Range<T> result = new Range<T>( math.Subtract( range.MinValue, value ), math.Subtract( range.MaxValue, value ) );
			return result;
		}

		[Pure]
		public static Range<T> operator -( Range<T> range1, Range<T> range2 )
		{
			Range<T> result = new Range<T>( math.Subtract( range1.MinValue, range2.MaxValue ), math.Subtract( range1.MaxValue, range2.MinValue ) );
			return result;
		}

		[Pure]
		public Range<T> Intersect( Range<T> other )
		{
			T minValue = math.Max( _minValue, other._minValue );
			T maxValue = math.Min( _maxValue, other._maxValue );

			return new Range<T>( minValue, maxValue );
		}

		[Pure]
		public bool IntersectsWith( Range<T> other )
		{
			bool intersects =
				( math.Compare( other.MinValue, this._minValue ) <= 0 &&
				 math.Compare( this._minValue, other._maxValue ) <= 0 ) ||
				( math.Compare( other.MinValue, this._maxValue ) <= 0 &&
				 math.Compare( this._minValue, other._maxValue ) <= 0 ) ||
				 ( math.Compare( this._minValue, other._minValue ) <= 0 &&
				 math.Compare( other._minValue, this._maxValue ) <= 0 ) ||
				 ( math.Compare( this._minValue, other._maxValue ) <= 0 &&
				 math.Compare( other._maxValue, this._maxValue ) <= 0 );

			return intersects;
		}
	}
}