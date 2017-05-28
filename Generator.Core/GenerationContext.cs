using System;
using System.Linq;
using System.Collections.Generic;
using Generator.Core.Constraints;
using Generator.Core.Operations;
using JetBrains.Annotations;

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

		private int _currentDepth;

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

		public int CurrentDepth
		{
			get { return _currentDepth; }
		}

		public void IncreaseDepth()
		{
			_currentDepth++;
		}

		public void DecreaseDepth()
		{
			_currentDepth--;
		}

		public bool PassesConstraints( Operation<T> operation )
		{
			bool passes = _constraints.All( c => c.Passes( this, operation ) );
			return passes;
		}

		public IDisposable PushConstraints( params IConstraint<T>[] constraints )
		{
			return new ConstraintsSession( this, constraints );
		}

		public IDisposable PushConstraints( ICollection<IConstraint<T>> constraints )
		{
			return new ConstraintsSession( this, constraints );
		}

		private void PushConstraint( [NotNull] IConstraint<T> constraint )
		{
			if ( constraint == null )
			{
				throw new ArgumentNullException( "constraint" );
			}

			_constraints.Push( constraint );
		}

		private void PopConstraint()
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
			clone._currentDepth = this._currentDepth;

			CopyConstraints( clone );

			return clone;
		}

		[Pure]
		public GenerationContext<T> CloneWithRange( Range<T> range )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator, _maxComplexity, range, _termRange );

			clone._currentDepth = this._currentDepth;

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

		private class ConstraintsSession : IDisposable
		{
			private readonly int _constraintsCount;
			private readonly GenerationContext<T> _context;

			public ConstraintsSession( GenerationContext<T> context, ICollection<IConstraint<T>> constraints )
			{
				_context = context;
				_constraintsCount = constraints.Count;

				foreach ( var constraint in constraints )
				{
					context.PushConstraint( constraint );
				}
			}

			#region Implementation of IDisposable

			public void Dispose()
			{
				for ( int i = 0; i < _constraintsCount; i++ )
				{
					_context.PopConstraint();
				}
			}

			#endregion
		}
	}
}