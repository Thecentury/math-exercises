using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Generator.Core
{
	public sealed class RationalNumber : IEquatable<RationalNumber>, IComparable<RationalNumber>
	{
		private readonly int _nominator;
		private readonly int _denominator;

		public RationalNumber( int nominator, int denominator )
		{
			_nominator = nominator;
			_denominator = denominator;
		}

		public int Nominator
		{
			get { return _nominator; }
		}

		public int Denominator
		{
			get { return _denominator; }
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
			if ( i1 < 1 )
			{
				throw new ArgumentOutOfRangeException( "i1" );
			}
			if ( i2 < 1 )
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

		public static explicit operator double( RationalNumber number )
		{
			return ( (double)number.Nominator ) / number.Denominator;
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
