using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using Generator.Core;
using Generator.Core.Constraints;
using Generator.Core.Generators;
using MathExercisesGenerator.Properties;

namespace MathExercisesGenerator
{
	public sealed class ApplicationViewModel : BindingObject
	{
		private readonly List<ExerciseViewModel> _exercises = new List<ExerciseViewModel>();

		public ApplicationViewModel( Range<int> range, int exercisesCount, Range<double> сomplexityRange )
		{
			ConvertToLineVisitor<int> convertToLineVisitorvisitor = new ConvertToLineVisitor<int>();
			ConvertToOperationViewModelVisitor convertToOperationViewModelVisitor = new ConvertToOperationViewModelVisitor();
			BracketsVisitor bracketsVisitor = new BracketsVisitor();
			var rnd = new ProbabilityGenerator();

			var settings = Settings.Default;

			List<IOperationGenerator<int>> generators = new List<IOperationGenerator<int>>();
			generators.Add( new NumberGenerator() );
			if ( settings.AdditionEnabled )
			{
				generators.Add( new IntegralAdditionGenerator() );
			}
			if ( settings.SubtractionEnabled )
			{
				generators.Add( new IntegralSubtractionGenerator() );
			}
			if ( settings.MultiplicationEnabled )
			{
				generators.Add( new IntegralPositiveMultiplicationGenerator() );
			}

			var generatorsArray = generators.ToArray();

			for ( int i = 0; i < exercisesCount; i++ )
			{
				double complexity = rnd.Generate( сomplexityRange );


				IntExerciseGenerator gen = new IntExerciseGenerator( rnd, new IntRandomNumberGenerator(),
																	complexity,
																	generatorsArray );
				//new IntegralDivisionGenerator()

				var op = gen.Generate( range );

				var opWithBrackets = bracketsVisitor.VisitCore( op );
				var terms = convertToLineVisitorvisitor.VisitCore( opWithBrackets );
				var operationViewModel = convertToOperationViewModelVisitor.VisitCore( opWithBrackets );

				var exercise = new ExerciseViewModel( op, terms, operationViewModel );
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
