﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Generator.Core;
using Generator.Core.Constraints;
using Generator.Core.Generators;

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
																new IntegralAdditionGenerator(),
																new IntegralSubtractionGenerator(),
																new IntegralPositiveMultiplicationGenerator()
																);

			ConvertToLineVisitor<int> convertToLineVisitorvisitor = new ConvertToLineVisitor<int>();
			ConvertToOperationViewModelVisitor convertToOperationViewModelVisitor = new ConvertToOperationViewModelVisitor();
			BracketsVisitor bracketsVisitor = new BracketsVisitor();

			for ( int i = 0; i < exercisesCount; i++ )
			{
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
