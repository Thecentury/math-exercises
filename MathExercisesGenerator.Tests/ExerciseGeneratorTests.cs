using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;
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

			Assert.That( op, Is.InstanceOf<Number>() );
		}

		private static IntExerciseGenerator CreateExerciseGenerator( double maxComplexity )
		{
			NumberGenerator numberGenerator = new NumberGenerator();
			AdditionGenerator addSubGenerator = new AdditionGenerator();

			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(), maxComplexity,
																numberGenerator, addSubGenerator );
			return gen;
		}

		[Test]
		public void ShouldCreateOneAddition()
		{
			var gen = CreateExerciseGenerator( Complexities.AdditionSubtractionComplexity );

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
