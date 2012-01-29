using JetBrains.Annotations;

namespace Generator.Core
{
	public sealed class GenerationContext<T>
	{
		private readonly double _maxComplexity;

		private readonly IRandomNumberGenerator<double> _probabilityGenerator;
		private readonly IRandomNumberGenerator<T> _numberGenerator;

		private readonly IOperationGenerator<T> _parentGenerator;

		public T MinValue { get; set; }
		public T MaxValue { get; set; }

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
		                          double maxComplexity )
		{
			_maxComplexity = maxComplexity;
			_parentGenerator = parentGenerator;
			_probabilityGenerator = probabilityGenerator;
			_numberGenerator = numberGenerator;
		}

		[Pure]
		public GenerationContext<T> CloneWithMaxComplexity( double maxComplexity )
		{
			GenerationContext<T> clone = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, _parentGenerator, maxComplexity );
			return clone;
		}
	}
}