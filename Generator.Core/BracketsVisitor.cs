using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Core
{
	public sealed class BracketsVisitor
	{
		private Operation<int> Visit( BinaryOperation<int> op )
		{
			var right = op.Right;
			if ( right is BinaryOperation<int> )
			{
				var clone = op.CloneCore();
				clone.Left = VisitCore( op.Left );
				clone.Right = new Brackets( VisitCore( right ) );
				return clone;
			}

			return op;
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
