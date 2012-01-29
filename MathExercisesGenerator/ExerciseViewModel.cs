using Generator.Core;

namespace MathExercisesGenerator
{
	public sealed class ExerciseViewModel : BindingObject
	{
		private readonly Operation<int> _operation;

		public ExerciseViewModel( Operation<int> operation )
		{
			_operation = operation;
		}

		public int CorrectValue
		{
			get { return _operation.Evaluate(); }
		}

		public bool IsCorrect
		{
			get { return CorrectValue == UserValue; }
		}

		private int _userValue;
		public int UserValue
		{
			get { return _userValue; }
			set
			{
				_userValue = value;
				RaisePropertyChanged( "UserValue" );
				RaisePropertyChanged( "IsCorrect" );
			}
		}
	}
}