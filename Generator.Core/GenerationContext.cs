using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public sealed class GenerationContext<T>
	{
		private readonly double _maxComplexity;

		private readonly IRandomNumberGenerator<double> _probabilityGenerator;
		private readonly IRandomNumberGenerator<T> _numberGenerator;
		private readonly IOperationGenerator<T> _parentGenerator;
		private readonly Range<T> _expressionRange;
		private readonly Range<T> _termRange;
		private readonly Stack<IConstraint<T>> _constraints = new Stack<IConstraint<T>>();

		public Range<T> ExpressionRange
		{
			get { return _expressionRange; }
		}

		public Range<T> TermRange
		{
			get { return _termRange; }
		}

		public double MaxComplexity
		{
			get { return _maxComplexity; }
		}

		public IRandomNumberGenerator<double> ProbabilityGenerator
		{
			get { return _probabilityGenerator; }
		}

		public IRandomNumberGenerator<T> NumberGenerator
		{
			get { return _numberGenerator; }
		}

		public IOperationGenerator<T> ParentGenerator
		{
			get { return _parentGenerator; }
		}

		public bool PassesConstraints( Operation<T> operation )
		{
			bool passes = _constraints.All( c => c.Passes( this, operation ) );
			return passes;
		}

		public void PushConstraint( [NotNull] IConstraint<T> constraint )
		{
			if ( constraint == null )
			{
				throw new ArgumentNullException( "constraint" );
			}

			_constraints.Push( constraint );
		}

		public void PopConstraint()
		{
			_constraints.Pop();
		}

		public GenerationContext( IRandomNumberGenerator<double> probabilityGenerator, IRandomNumberGenerator<T> numberGenerator,
								  IOperationGenerator<T> parentGenerator,
								  double maxComplexity,
			[NotNull] Range<T> range,
			[NotNull] Range<T> termRange
			)
		{
			if ( range == null ) throw new ArgumentNullException( "range" );
			if ( termRange == null ) throw new ArgumentNullException( "termRange" );

			_maxComplexity = maxComplexity;
			_expressionRange = range;
			_termRange = termRange;
			_parentGenerator = parentGenerator;
			_probabilityGenerator = probabilityGenerator;
			_numberGenerator = numberGenerator;
		}

		[Pure]
		public GenerationContext<T> CloneWithMaxComplexity( double maxComplexity )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator,
																  maxComplexity, _expressionRange, _termRange );

			CopyConstraints( clone );

			return clone;
		}

		[Pure]
		public GenerationContext<T> CloneWithRange( Range<T> range )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator, _maxComplexity, range, _termRange );

			CopyConstraints( clone );

			return clone;
		}

		private void CopyConstraints( GenerationContext<T> clone )
		{
			IConstraint<T>[] array = new IConstraint<T>[_constraints.Count];
			_constraints.CopyTo( array, 0 );
			Array.Reverse( array );

			foreach ( var constraint in array )
			{
				clone._constraints.Push( constraint );
			}
		}
	}
}