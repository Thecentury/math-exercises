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
			const double maxComplexity = 3.0;

			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(),
																maxComplexity,
																new NumberGenerator(),
																new AdditionSubtractionBinaryOperationGenerator() );

			ConvertToLineVisitor visitor = new ConvertToLineVisitor();
			BracketsVisitor bracketsVisitor = new BracketsVisitor();

			for ( int i = 0; i < _exercisesCount; i++ )
			{
				var op = gen.Generate();
				var opWithBrackets = bracketsVisitor.VisitCore( op );
				var terms = visitor.VisitCore( opWithBrackets );
				_exercises.Add( new ExerciseViewModel( op, terms ) );
			}
		}

		public List<ExerciseViewModel> Exercises
		{
			get { return _exercises; }
		}
	}
}
