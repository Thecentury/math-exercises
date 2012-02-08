using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class RationalNumberTests
	{
		[TestCase( "1/2", 1, 2 )]
		[TestCase( "-1/2", -1, 2 )]
		public void ShouldParse( string str, int nominator, int denominator )
		{
			var r = RationalNumber.Parse( str );

			Assert.AreEqual( new RationalNumber( nominator, denominator ), r );
		}

		[TestCase( "2/4", 1, 2 )]
		[TestCase( "5/15", 1, 3 )]
		[TestCase( "-5/20", -1, 4 )]
		[TestCase( "1/2", 1, 2 )]
		[TestCase( "-1/2", -1, 2 )]
		public void ShouldCancel( string str, int nominator, int denominator )
		{
			var r = RationalNumber.Parse( str ).Cancel();

			Assert.That( r.Nominator, Is.EqualTo( nominator ) );
			Assert.That( r.Denominator, Is.EqualTo( denominator ) );
		}

		[TestCase( 10, 5, 5 )]
		[TestCase( 10, 3, 1 )]
		[TestCase( 21, 21, 21 )]
		public void ShouldProperlyCalculateGreatestCommonDivisor( int i1, int i2, int expectedGcd )
		{
			int gcd = RationalNumber.GreatestCommonDivisor( i1, i2 );

			Assert.AreEqual( expectedGcd, gcd );
		}

		[TestCase( "1/2", 0.5 )]
		[TestCase( "1/1", 1 )]
		[TestCase( "3/2", 1.5 )]
		[TestCase( "-3/2", -1.5 )]
		public void ShouldCalculateDoubleValue( string str, double value )
		{
			RationalNumber r = RationalNumber.Parse( str );

			double actual = (double)r;

			Assert.That( actual, Is.EqualTo( value ) );
		}

		[TestCase( "1/3", "3/9", 0 )]
		[TestCase( "1/3", "1/9", +1 )]
		[TestCase( "1/9", "1/3", -1 )]
		public void ShouldCompareProperly( string str1, string str2, int expectedComparison )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			int actualComparison = r1.CompareTo( r2 );
			Assert.That( actualComparison, Is.EqualTo( expectedComparison ) );
		}

		[TestCase( "1/3", "3/9", true )]
		[TestCase( "-1/3", "-3/9", true )]
		[TestCase( "1/3", "4/9", false )]
		public void ShouldBeOfProperEquality( string str1, string str2, bool expectedAreEqual )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			bool actualAreEqual = r1.Equals( r2 );
			Assert.AreEqual( actualAreEqual, expectedAreEqual );
		}
	}
}
