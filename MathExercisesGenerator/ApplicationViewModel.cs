using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core;

namespace MathExercisesGenerator
{
	public sealed class ApplicationViewModel
	{
		private readonly List<ExerciseViewModel> _exercises = new List<ExerciseViewModel>();
		private readonly int _exercisesCount = 10;

		public ApplicationViewModel()
		{
			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(), 2,
				new NumberGenerator(), new AdditionSubtractionBinaryOperationGenerator() );

			for ( int i = 0; i < _exercisesCount; i++ )
			{
				_exercises.Add( new ExerciseViewModel( gen.Generate() ) );
			}
		}
	}
}
