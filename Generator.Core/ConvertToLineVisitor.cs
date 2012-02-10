using System.Collections.Generic;
using Generator.Core.Operations;

namespace Generator.Core
{
	public sealed class ConvertToLineVisitor<T> : IVisitor<List<TermViewModel>>
	{
		private List<TermViewModel> Visit( Number<T> number )
		{
			return Single( new NumberViewModel( number.Text ) );
		}

		private List<TermViewModel> Visit( SubtractOperation<T> op )
		{
			return VisitBinaryOp( op, new SubtractionViewModel() );
		}

		private List<TermViewModel> Visit( AddOperation<T> op )
		{
			return VisitBinaryOp( op, new AdditionViewModel() );
		}

		private List<TermViewModel> Visit( MultiplyOperation<T> op )
		{
			return VisitBinaryOp( op, new MultiplicationViewModel() );
		}

		private List<TermViewModel> Visit( DivideOperation<T> op )
		{
			return VisitBinaryOp( op, new DivisionViewModel() );
		}

		private List<TermViewModel> VisitBinaryOp( BinaryOperation<T> op, TermViewModel operation )
		{
			List<TermViewModel> list = new List<TermViewModel>();

			list.AddRange( VisitCore( op.Left ) );
			list.Add( operation );
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