using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
using Generator.Core.Generators;
using Generator.Core.Operations;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class IntegralMultiplicationGeneratorTests : BinaryOperationGeneratorTests
	{
		[Test]
		public void ShouldCreateMultiplication()
		{
			const double complexity = 2;
			var context = CreateMockContext( complexity, Range.Create( 2, 2 ), Range.Create( 1, 100 ) );

			var generator = new IntegralPositiveMultiplicationGenerator();
			var op = generator.Generate( context );

			Assert.NotNull( op );
			Assert.That( op, Is.InstanceOf<MultiplyOperation<int>>() );
		}
	}
}
