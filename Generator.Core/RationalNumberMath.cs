using System;
using System.Globalization;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class RationalNumberMath : INumeric<RationalNumber>, IFractional<RationalNumber>
	{
		public RationalNumber Add( RationalNumber r1, RationalNumber r2 )
		{
			return r1 + r2;
		}

		public RationalNumber Subtract( RationalNumber r1, RationalNumber r2 )
		{
			return r1 - r2;
		}

		public RationalNumber Multiply( RationalNumber r1, RationalNumber r2 )
		{
			return r1 * r2;
		}

		public int Compare( RationalNumber r1, RationalNumber r2 )
		{
			return r1.CompareTo( r2 );
		}

		public bool Equals( RationalNumber r1, RationalNumber r2 )
		{
			return r1.Equals( r2 );
		}

		public RationalNumber Negate( RationalNumber r )
		{
			return -r;
		}

		public int Sign( RationalNumber r )
		{
			return r.CompareTo( RationalNumber.Zero );
		}

		public RationalNumber Abs( RationalNumber r )
		{
			return r.Abs();
		}

		public string ToString( RationalNumber r, string obj1, IFormatProvider obj2 )
		{
			return r.ToString();
		}

		public RationalNumber Parse( string str, NumberStyles obj1, IFormatProvider obj2 )
		{
			return RationalNumber.Parse( str );
		}

		public RationalNumber Zero
		{
			get { return RationalNumber.Zero; }
		}

		public RationalNumber One
		{
			get { return RationalNumber.One; }
		}

		#region Implementation of IFractional<RationalNumber>

		public RationalNumber Reciprocal( RationalNumber r )
		{
			return 1 / r;
		}

		public RationalNumber Divide( RationalNumber r1, RationalNumber r2 )
		{
			return r1 / r2;
		}

		#endregion
	}
}