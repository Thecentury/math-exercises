using Generator.Core;
using Generator.Core.Operations;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class BracketsVisitorTests
	{
		[Test]
		public void ShouldEncloseInBrackets()
		{
			var op = Operation.From( 1 ) + ( Operation.From( 2 ) + Operation.From( 3 ) );
			string text = GetOperationTextWithBrackets( op );

			Assert.That( text, Is.EqualTo( "1 + (2 + 3)" ) );
		}

		[Test]
		public void ShouldNotEncloseInBrackets()
		{
			var op = ( Operation.From( 1 ) + Operation.From( 2 ) ) + Operation.From( 3 );
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
