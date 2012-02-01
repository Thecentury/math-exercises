using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class BracketsVisitorTests
	{
		[Test]
		public void ShouldEncloseInBrackets()
		{
			var op = new AddOperation( new Number( 1 ), new AddOperation( new Number( 2 ), new Number( 3 ) ) );
			BracketsVisitor visitor = new BracketsVisitor();
			var clone = visitor.VisitCore( op );

			Assert.That( clone.Text, Is.EqualTo( "1 + (2 + 3)" ) );
		}

		[Test]
		public void ShouldNotEncloseInBrackets()
		{
			var op = new AddOperation( new AddOperation( new Number( 1 ), new Number( 2 ) ), new Number( 3 ) );
			BracketsVisitor visitor = new BracketsVisitor();
			var clone = visitor.VisitCore( op );

			Assert.That( clone.Text, Is.EqualTo( "1 + 2 + 3" ) );
		}
	}
}
