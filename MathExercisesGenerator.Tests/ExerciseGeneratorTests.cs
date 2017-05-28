using System;
using Generator.Core;
using Generator.Core.Generators;
using Generator.Core.Operations;
using NUnit.Framework;

namespace MathExercisesGenerator.Tests
{
	[TestFixture]
	public class ExerciseGeneratorTests
	{
		[Test]
		public void ShouldCreateNumber()
		{
			var gen = CreateExerciseGenerator( Complexities.NumberComplexity );

			var op = gen.Generate( new Range<int>( 0, 10 ) );

			Assert.That( op, Is.InstanceOf<Number<int>>() );
		}

		private static IntExerciseGenerator CreateExerciseGenerator( double maxComplexity )
		{
			NumberGenerator numberGenerator = new NumberGenerator();
			IntegralAdditionGenerator addSubGenerator = new IntegralAdditionGenerator();

			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(), maxComplexity,
																numberGenerator, addSubGenerator );
			return gen;
		}

		[Test]
		public void ShouldCreateOneAddition()
		{
			var gen = CreateExerciseGenerator( Complexities.AdditionComplexity + 0.1 );

			var op = gen.Generate( new Range<int>( 0, 10 ) );

			Assert.That( op, Is.InstanceOf<BinaryOperation<int>>() );
		}

		[TestCase( 2.0 )]
		[TestCase( 3.0 )]
		[TestCase( 4.0 )]
		public void ShouldBeAbleToGenerateExerciseWithGivenComplexity( double complexity )
		{
			var gen = CreateExerciseGenerator( complexity );

			var op = gen.Generate( new Range<int>( 0, 10 ) );

			Console.WriteLine( op );

			Assert.That( op, Is.Not.Null );
		}
	}
}
