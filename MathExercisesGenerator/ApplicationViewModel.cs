using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Generator.Core;

namespace MathExercisesGenerator
{
	public sealed class ApplicationViewModel : BindingObject
	{
		private readonly List<ExerciseViewModel> _exercises = new List<ExerciseViewModel>();

		public ApplicationViewModel( Range<int> range, int exercisesCount = 5, double maxComplexity = 1.0 )
		{
			IntExerciseGenerator gen = new IntExerciseGenerator( new ProbabilityGenerator(), new IntRandomNumberGenerator(),
																maxComplexity,
																new NumberGenerator(),
																new IntergralAdditionGenerator(),
																new IntegralSubtractionGenerator(),
																new IntegralPositiveMultiplicationGenerator()
																);

			ConvertToLineVisitor<int> visitor = new ConvertToLineVisitor<int>();
			BracketsVisitor bracketsVisitor = new BracketsVisitor();

			for ( int i = 0; i < exercisesCount; i++ )
			{
				var op = gen.Generate( range );
				var opWithBrackets = bracketsVisitor.VisitCore( op );
				var terms = visitor.VisitCore( opWithBrackets );
				var exercise = new ExerciseViewModel( op, terms );
				exercise.PropertyChanged += OnExercisePropertyChanged;
				_exercises.Add( exercise );
			}
		}

		private void OnExercisePropertyChanged( object sender, PropertyChangedEventArgs e )
		{
			IsComplete = _exercises.All( ex => ex.IsCorrect );
		}

		public List<ExerciseViewModel> Exercises
		{
			get { return _exercises; }
		}

		private bool _isComplete;
		public bool IsComplete
		{
			get { return _isComplete; }
			set
			{
				_isComplete = value;
				RaisePropertyChanged( "IsComplete" );
			}
		}
	}
}
