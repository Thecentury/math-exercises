using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Core.Operations;

namespace Generator.Core.Constraints
{
	public sealed class MaxDepthConstraint<T> : IConstraint<T>
	{
		private readonly int _maxDepth;

		public MaxDepthConstraint(int maxDepth)
		{
			_maxDepth = maxDepth;
		}

		public bool Passes(GenerationContext<T> ctx, Operation<T> op)
		{
			bool passes = op.Depth <= _maxDepth;
			return passes;
		}
	}
}
