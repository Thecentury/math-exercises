using System.Collections.Generic;

namespace Generator.Core
{
	public sealed class ConvertToLineVisitor : IVisitor<List<TermViewModel>>
	{
		private List<TermViewModel> Visit( Number number )
		{
			return Single( new NumberViewModel( number.Text ) );
		}

		private List<TermViewModel> Visit( SubtractOperation op )
		{
			List<TermViewModel> list = new List<TermViewModel>();

			list.AddRange( VisitCore( op.Left ) );
			list.Add( new SubtractionViewModel() );
			list.AddRange( VisitCore( op.Right ) );

			return list;
		}

		private List<TermViewModel> Visit( AddOperation op )
		{
			List<TermViewModel> list = new List<TermViewModel>();

			list.AddRange( VisitCore( op.Left ) );
			list.Add( new AdditionViewModel() );
			list.AddRange( VisitCore( op.Right ) );

			return list;
		}

		private List<TermViewModel> Visit( Brackets brackets )
		{
			List<TermViewModel> list = new List<TermViewModel>();

			list.Add( new OpeningBracketViewModel() );
			list.AddRange( VisitCore( brackets.Inner ) );
			list.Add( new ClosingBracketViewModel() );

			return list;
		}

		public List<TermViewModel> VisitCore( dynamic input )
		{
			return Visit( input );
		}

		private List<TermViewModel> Single( TermViewModel term )
		{
			return new List<TermViewModel> { term };
		}
	}
}