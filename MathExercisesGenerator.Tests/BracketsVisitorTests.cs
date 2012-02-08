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
			var op = new Number( 1 ) + ( new Number( 2 ) + new Number( 3 ) );
			string text = GetOperationTextWithBrackets( op );

			Assert.That( text, Is.EqualTo( "1 + (2 + 3)" ) );
		}

		[Test]
		public void ShouldNotEncloseInBrackets()
		{
			var op = ( new Number( 1 ) + new Number( 2 ) ) + new Number( 3 );
			string text = GetOperationTextWithBrackets( op );

			Assert.That( text, Is.EqualTo( "1 + 2 + 3" ) );
		}

		private static string GetOperationTextWithBrackets( Operation<int> op )
		{
			BracketsVisitor visitor = new BracketsVisitor();
			var clone = visitor.VisitCore( op );
			return clone.Text;
		}
	}
}
