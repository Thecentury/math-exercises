namespace Generator.Core
{
	public sealed class ConvertToOperationViewModelVisitor : IVisitor<OperationViewModel>
	{
		private OperationViewModel Visit( Number number )
		{
			return new NumberOperationViewModel( number );
		}

		private OperationViewModel Visit( BinaryOperation<int> op )
		{
			var left = VisitCore( op.Left );
			var right = VisitCore( op.Right );

			return new BinaryOperationViewModel( op.OperationText, left, right, op );
		}

		private OperationViewModel Visit( Brackets brackets )
		{
			var inner = VisitCore( brackets.Inner );
			return new BracketOperationViewModel( inner );
		}

		public OperationViewModel VisitCore( dynamic input )
		{
			OperationViewModel result = Visit(input);
			return result;
		}
	}
}