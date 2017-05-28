using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class StackTests
	{
		[Test]
		public void ShouldEnumerateElementsInRightOrder()
		{
			Stack<int> stack = new Stack<int>();
			stack.Push( 1 );
			stack.Push( 2 );
			stack.Push( 3 );

			int[] array = new int[3];
			stack.CopyTo( array, 0 );
			Array.Reverse( array );

			foreach ( var i in array )
			{
				Console.WriteLine( i );
			}
		}
	}
}
