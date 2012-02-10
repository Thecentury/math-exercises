using System.Windows.Media;

namespace Generator.Core
{
	public abstract class TermViewModel
	{
	}

	public sealed class NumberViewModel : TermViewModel
	{
		private readonly string _value;

		public NumberViewModel( string value )
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}

	public sealed class AdditionViewModel : TermViewModel { }
	public sealed class SubtractionViewModel : TermViewModel { }
	public sealed class MultiplicationViewModel : TermViewModel { }
	public sealed class DivisionViewModel : TermViewModel { }
	public sealed class OpeningBracketViewModel : TermViewModel { }
	public sealed class ClosingBracketViewModel : TermViewModel { }
}