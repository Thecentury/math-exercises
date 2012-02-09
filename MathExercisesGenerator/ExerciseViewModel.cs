using System.Collections.Generic;
using Generator.Core;
using Generator.Core.Operations;

namespace MathExercisesGenerator
{
	public sealed class ExerciseViewModel : BindingObject
	{
		private readonly Operation<int> _operation;
		private readonly List<TermViewModel> _terms;
		private readonly OperationViewModel _operationViewModel;

		public ExerciseViewModel( Operation<int> operation, List<TermViewModel> terms, OperationViewModel operationViewModel )
		{
			_operation = operation;
			_terms = terms;
			_operationViewModel = operationViewModel;
		}

		public OperationViewModel OperationViewModel
		{
			get { return _operationViewModel; }
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

		public bool UserHasAnswered
		{
			get { return _userValue.HasValue; }
		}

		public void Solve()
		{
			UserValue = CorrectValue;
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
				RaisePropertyChanged( "UserHasAnswered" );
			}
		}
	}
}