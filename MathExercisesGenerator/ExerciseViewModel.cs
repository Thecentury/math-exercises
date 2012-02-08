using System.Collections.Generic;
using Generator.Core;
using Generator.Core.Operations;

namespace MathExercisesGenerator
{
	public sealed class ExerciseViewModel : BindingObject
	{
		private readonly Operation<int> _operation;
		private readonly List<TermViewModel> _terms;

		public ExerciseViewModel( Operation<int> operation, List<TermViewModel> terms)
		{
			_operation = operation;
			_terms = terms;
		}

		public List<TermViewModel> Terms
		{
			get { return _terms; }
		}

		public int CorrectValue
		{
			get { return _operation.Evaluate(); }
		}

		public bool IsCorrect
		{
			get { return CorrectValue == UserValue; }
		}

		public bool IsIncorrect
		{
			get { return _userValue.HasValue && CorrectValue != UserValue; }
		}

		private int? _userValue;
		public int? UserValue
		{
			get { return _userValue; }
			set
			{
				_userValue = value;
				RaisePropertyChanged( "UserValue" );
				RaisePropertyChanged( "IsCorrect" );
				RaisePropertyChanged( "IsIncorrect" );
			}
		}
	}
}