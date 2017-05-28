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
		[TestCase( 0, 4, 4 )]
		[TestCase( 4, 0, 4 )]
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

		[TestCase( "1/2", "-1/2" )]
		[TestCase( "-1/3", "1/3" )]
		public void TestNegation( string str, string expectedStr )
		{
			RationalNumber r1 = RationalNumber.Parse( str );
			RationalNumber expectedNegative = RationalNumber.Parse( expectedStr );

			RationalNumber negative = -r1;
			Assert.That( negative, Is.EqualTo( expectedNegative ) );
		}

		[TestCase( "1/2" )]
		[TestCase( "-1/2" )]
		public void TestDoubleNegation( string str )
		{
			RationalNumber r = RationalNumber.Parse( str );

			Assert.That( r, Is.EqualTo( -( -r ) ) );
		}

		[TestCase( "1/2", "1/3", "5/6" )]
		[TestCase( "1/2", "-1/2", "0/1" )]
		public void ShouldAdd( string str1, string str2, string resultStr )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			RationalNumber sum = r1 + r2;
			RationalNumber expectedSum = RationalNumber.Parse( resultStr );

			Assert.That( sum, Is.EqualTo( expectedSum ) );
		}

		[TestCase( "1/2", "1/3", "1/6" )]
		[TestCase( "1/2", "-1/2", "1/1" )]
		[TestCase( "1/2", "1/2", "0/1" )]
		public void ShouldSubtract( string str1, string str2, string resultStr )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			RationalNumber sum = r1 - r2;
			RationalNumber expectedDifference = RationalNumber.Parse( resultStr );

			Assert.That( sum, Is.EqualTo( expectedDifference ) );
		}

		[TestCase( "1/2", "1/3", "1/6" )]
		[TestCase( "1/2", "-1/2", "-1/4" )]
		[TestCase( "1/2", "1/2", "1/4" )]
		[TestCase( "0/1", "1/2", "0/1" )]
		public void ShouldMultiply( string str1, string str2, string resultStr )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			RationalNumber multiplication = r1 * r2;
			RationalNumber expectedMultiplication = RationalNumber.Parse( resultStr );

			Assert.That( multiplication, Is.EqualTo( expectedMultiplication ) );
		}

		[TestCase( "1/2", "1/3", "3/2" )]
		[TestCase( "1/2", "-1/2", "-1/1" )]
		[TestCase( "1/2", "1/2", "1/1" )]
		public void ShouldDivide( string str1, string str2, string resultStr )
		{
			RationalNumber r1 = RationalNumber.Parse( str1 );
			RationalNumber r2 = RationalNumber.Parse( str2 );

			RationalNumber division = r1 / r2;
			RationalNumber expected = RationalNumber.Parse( resultStr );

			Assert.That( division, Is.EqualTo( expected ) );
		}
	}
}
