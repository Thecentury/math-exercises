using System;
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

		public Range<T> ExpressionRange
		{
			get { return _expressionRange; }
		}

		private static readonly INumeric<T> math = GlobalAssociations.GetNumericAssociation<T>();

		private readonly Range<T> _termRange = new Range<T>( 
			math.Parse( "1", NumberStyles.Number, CultureInfo.InvariantCulture ), 
			math.Parse( "100", NumberStyles.Number, CultureInfo.InvariantCulture ) );

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

		public GenerationContext( IRandomNumberGenerator<double> probabilityGenerator, IRandomNumberGenerator<T> numberGenerator,
								  IOperationGenerator<T> parentGenerator,
								  double maxComplexity, [NotNull] Range<T> range )
		{
			if ( range == null ) throw new ArgumentNullException( "range" );

			_maxComplexity = maxComplexity;
			_expressionRange = range;
			_parentGenerator = parentGenerator;
			_probabilityGenerator = probabilityGenerator;
			_numberGenerator = numberGenerator;
		}

		[Pure]
		public GenerationContext<T> CloneWithMaxComplexity( double maxComplexity )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator,
																  maxComplexity, _expressionRange );
			return clone;
		}

		public GenerationContext<T> CloneWithRange( Range<T> range )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator, _maxComplexity, range );

			return clone;
		}
	}
}