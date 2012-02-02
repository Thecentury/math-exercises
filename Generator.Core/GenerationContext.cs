using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Generator.Core
{
	public static class GenerationContextExtensions
	{
		public static bool Passes<T>(this GenerationContext<T> context, Operation<T> operation )
		{
			foreach ( var constraint in context.Constraints)
			{
				bool passes = constraint.Passes(operation);
				if ( !passes )
				{
					return false;
				}
			}

			return true;
		}
	}

	public sealed class GenerationContext<T>
	{
		private readonly double _maxComplexity;

		private readonly IRandomNumberGenerator<double> _probabilityGenerator;
		private readonly IRandomNumberGenerator<T> _numberGenerator;
		private readonly IOperationGenerator<T> _parentGenerator;
		private readonly Range<T> _range;
		private readonly List<IConstraint<T>> _constraints = new List<IConstraint<T>>();

		public Range<T> Range
		{
			get { return _range; }
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

		public List<IConstraint<T>> Constraints
		{
			get { return _constraints; }
		}

		public GenerationContext( IRandomNumberGenerator<double> probabilityGenerator, IRandomNumberGenerator<T> numberGenerator,
								  IOperationGenerator<T> parentGenerator,
								  double maxComplexity, [NotNull] Range<T> range )
		{
			if ( range == null ) throw new ArgumentNullException( "range" );

			_maxComplexity = maxComplexity;
			_range = range;
			_parentGenerator = parentGenerator;
			_probabilityGenerator = probabilityGenerator;
			_numberGenerator = numberGenerator;
		}

		[Pure]
		public GenerationContext<T> CloneWithMaxComplexity( double maxComplexity )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator,
																  maxComplexity, _range );
			return clone;
		}
	}
}