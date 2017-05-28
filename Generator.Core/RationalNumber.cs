using System;
using JetBrains.Annotations;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class RationalNumber : IEquatable<RationalNumber>, IComparable<RationalNumber>
	{
		static RationalNumber()
		{
			GlobalAssociations.RegisterNumericAssociation( new RationalNumberMath() );
		}

		private static readonly RationalNumber zero = new RationalNumber( 0, 1 );
		private static readonly RationalNumber one = new RationalNumber( 1, 1 );

		private readonly int _nominator;
		private readonly int _denominator;

		public RationalNumber( int nominator, int denominator )
		{
			_nominator = nominator;
			_denominator = denominator;

			if ( _denominator < 0 )
			{
				_nominator = -_nominator;
				_denominator = -_denominator;
			}
		}


		public int Nominator
		{
			get { return _nominator; }
		}

		public int Denominator
		{
			get { return _denominator; }
		}

		public static RationalNumber Zero
		{
			get { return zero; }
		}

		public static RationalNumber One
		{
			get { return one; }
		}

		public static RationalNumber Parse( [NotNull] string str )
		{
			if ( str == null )
			{
				throw new ArgumentNullException( "str" );
			}

			string[] parts = str.Split( '/' );

			int nominator = Int32.Parse( parts[0] );
			int denominator = Int32.Parse( parts[1] );

			RationalNumber result = new RationalNumber( nominator, denominator );
			return result;
		}

		[Pure]
		public RationalNumber Cancel()
		{
			int gcd = GreatestCommonDivisor( Math.Abs( _nominator ), Math.Abs( _denominator ) );
			if ( gcd > 1 )
			{
				int nom = _nominator / gcd;
				int den = _denominator / gcd;

				return new RationalNumber( nom, den );
			}

			return this;
		}

		public static int GreatestCommonDivisor( int i1, int i2 )
		{
			if ( i1 == 0 || i2 == 0 )
			{
				return Math.Max( i1, i2 );
			}

			if ( i1 < 0 )
			{
				throw new ArgumentOutOfRangeException( "i1" );
			}
			if ( i2 < 0 )
			{
				throw new ArgumentOutOfRangeException( "i2" );
			}

			int max = Math.Max( i1, i2 );
			int min = Math.Min( i1, i2 );

			do
			{
				int diff = max - min;
				max -= min;

				if ( diff == 1 )
				{
					return 1;
				}
				else if ( diff == 0 )
				{
					return min;
				}
			} while ( true );
		}

		public RationalNumber Abs()
		{
			return new RationalNumber( Math.Abs( _nominator ), _denominator );
		}

		#region Operators

		public static bool operator ==( RationalNumber r1, RationalNumber r2 )
		{
			return Equals( r1, r2 );
		}

		public static bool operator !=( RationalNumber r1, RationalNumber r2 )
		{
			return !( r1 == r2 );
		}

		public static implicit operator RationalNumber( int value )
		{
			return new RationalNumber( value, 1 );
		}

		public double ToDouble()
		{
			return (double)this;
		}

		public static explicit operator double( RationalNumber r )
		{
			return ( (double)r.Nominator ) / r.Denominator;
		}

		public static RationalNumber operator -( RationalNumber r )
		{
			return new RationalNumber( -r._nominator, r._denominator );
		}

		public static RationalNumber operator +( RationalNumber r1, RationalNumber r2 )
		{
			RationalNumber result = new RationalNumber( r1.Nominator * r2.Denominator + r2.Nominator * r1.Denominator, r1.Denominator * r2.Denominator );
			result = result.Cancel();

			return result;
		}

		public static RationalNumber operator -( RationalNumber r1, RationalNumber r2 )
		{
			RationalNumber result = new RationalNumber( r1.Nominator * r2.Denominator - r2.Nominator * r1.Denominator, r1.Denominator * r2.Denominator );
			result = result.Cancel();

			return result;
		}

		public static RationalNumber operator *( RationalNumber r1, RationalNumber r2 )
		{
			RationalNumber result = new RationalNumber( r1._nominator * r2._nominator, r1._denominator * r2._denominator );
			result = result.Cancel();
			return result;
		}

		public static RationalNumber operator /( RationalNumber r1, RationalNumber r2 )
		{
			RationalNumber result = new RationalNumber( r1._nominator * r2._denominator, r1._denominator * r2._nominator );
			result = result.Cancel();
			return result;
		}

		#endregion

		#region Overrides

		public int CompareTo( RationalNumber other )
		{
			int comparison = this.ToDouble().CompareTo( other.ToDouble() );
			return comparison;
		}

		public override string ToString()
		{
			return _nominator + "/" + _denominator;
		}

		public bool Equals( RationalNumber other )
		{
			if ( ReferenceEquals( null, other ) )
			{
				return false;
			}
			if ( ReferenceEquals( this, other ) )
			{
				return true;
			}

			RationalNumber canceledThis = this.Cancel();
			RationalNumber canceledOther = other.Cancel();

			return canceledThis._nominator == canceledOther._nominator && canceledThis._denominator == canceledOther._denominator;
		}

		public override bool Equals( object obj )
		{
			if ( ReferenceEquals( null, obj ) )
			{
				return false;
			}
			if ( ReferenceEquals( this, obj ) )
			{
				return true;
			}
			if ( obj.GetType() != typeof( RationalNumber ) )
			{
				return false;
			}
			return Equals( (RationalNumber)obj );
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ( _nominator * 397 ) ^ _denominator;
			}
		}

		#endregion
	}
}
