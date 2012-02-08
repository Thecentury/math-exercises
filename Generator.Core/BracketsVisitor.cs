using System;
using System.Linq;
using System.Text;
using Generator.Core.Operations;

namespace Generator.Core
{
	public sealed class BracketsVisitor : IVisitor<Operation<int>>
	{
		private Operation<int> Visit( BinaryOperation<int> op )
		{
			bool needAddBracketsLeft = op.Priority > op.Left.Priority;
			bool needAddBracketsRight = op.Priority >= op.Right.Priority;

			Operation<int> left = VisitCore( op.Left );
			if ( needAddBracketsLeft )
			{
				left = new Brackets( left );
			}
			Operation<int> right = VisitCore( op.Right );

			if ( needAddBracketsRight )
			{
				right = new Brackets( right );
			}

			var clone = op.CloneCore();
			clone.Left = left;
			clone.Right = right;

			return clone;
		}

		private Operation<int> Visit( Operation<int> op )
		{
			return op;
		}

		public Operation<int> VisitCore( dynamic target )
		{
			return Visit( target );
		}
	}
}
