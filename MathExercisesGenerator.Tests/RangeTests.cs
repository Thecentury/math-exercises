using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class RangeTests
	{
		[TestCase( -1, 2 )]
		[TestCase( -1, 0 )]
		[TestCase( 0, 1 )]
		[TestCase( 1, 2 )]
		[TestCase( 5, 10 )]
		[TestCase( 5, 12 )]
		[TestCase( 10, 12 )]
		public void TwoRangesShouldIntersectEachOther( int min, int max )
		{
			var r1 = Range.Create( 0, 10 );
			var r2 = Range.Create( min, max );

			Assert.IsTrue( r1.IntersectsWith( r2 ) );
			Assert.IsTrue( r2.IntersectsWith( r1 ) );
		}

		[TestCase( -2, -1 )]
		[TestCase( 11, 12 )]
		public void TwoRangesShouldNotInersect( int min, int max )
		{
			var r1 = Range.Create( 0, 10 );
			var r2 = Range.Create( min, max );

			Assert.IsFalse( r1.IntersectsWith( r2 ) );
			Assert.IsFalse( r2.IntersectsWith( r1 ) );
		}
	}
}
