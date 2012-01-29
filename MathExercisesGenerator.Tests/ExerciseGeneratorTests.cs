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

			var op = gen.Generate();

			Assert.That( op, Is.InstanceOf<Number>() );
		}

		private static IntExerciseGenerator CreateExerciseGenerator( double maxComplexity )
		{
			NumberGenerator numberGenerator = new NumberGenerator();
			AdditionSubtractionBinaryOperationGenerator addSubGenerator = new AdditionSubtractionBinaryOperationGenerator();

			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(), maxComplexity,
																numberGenerator, addSubGenerator );
			return gen;
		}

		[Test]
		public void ShouldCreateOneAddition()
		{
			var gen = CreateExerciseGenerator( Complexities.AdditionSubtractionComplexity );

			var op = gen.Generate();

			Assert.That( op, Is.InstanceOf<BinaryOperation<int>>() );
		}
	}
}
