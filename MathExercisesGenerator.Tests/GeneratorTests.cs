using Generator.Core;
using Generator.Core.Generators;
using Generator.Core.Operations;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class GeneratorTests : BinaryOperationGeneratorTests
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

			var value = op.Evaluate();
		}

		[Test]
		public void ShouldCreateDivision()
		{
			const double complexity = 2;
			var context = CreateMockContext( complexity, Range.Create( 2, 2 ), Range.Create( 2, 2 ) );

			var generator = new IntegralDivisionGenerator();
			var op = generator.Generate( context );

			Assert.NotNull( op );
			Assert.That( op, Is.InstanceOf<DivideOperation<int>>() );

			var value = op.Evaluate();
		}
	}
}
