using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Generator.Core.Constraints;
using Generator.Core.Operations;
using JetBrains.Annotations;

namespace Generator.Core.Generators
{
	public class ExerciseGenerator<T> : OperationGeneratorBase<T>
	{
		private readonly IRandomNumberGenerator<double> _probabilityGenerator;
		private readonly IRandomNumberGenerator<T> _numberGenerator;
		private readonly double _maxComplexity;

		private readonly List<IOperationGenerator<T>> _generators = new List<IOperationGenerator<T>>();

		private readonly List<IConstraint<T>> _customConstraints = new List<IConstraint<T>>();

		public ExerciseGenerator( IRandomNumberGenerator<double> probabilityGenerator, IRandomNumberGenerator<T> numberGenerator, double maxComplexity,
								  [NotNull] params IOperationGenerator<T>[] generators )
		{
			if ( probabilityGenerator == null ) throw new ArgumentNullException( "probabilityGenerator" );
			if ( numberGenerator == null ) throw new ArgumentNullException( "numberGenerator" );
			if ( generators == null ) throw new ArgumentNullException( "generators" );

			_probabilityGenerator = probabilityGenerator;
			_numberGenerator = numberGenerator;
			_maxComplexity = maxComplexity;

			_generators.AddRange( generators );
		}

		public override Operation<T> Generate( GenerationContext<T> context )
		{
			int attemptCounter = 0;

			context.IncreaseDepth();

			try
			{
				do
				{
					var generator = GetSuitableGenerator( context );

					var clonedContext = context.CloneWithMaxComplexity( context.MaxComplexity - generator.Complexity );
					var op = generator.Generate( clonedContext );

					bool passesConstraints = context.PassesConstraints( op );
					if ( passesConstraints )
					{
						return op;
					}
					else
					{
						attemptCounter++;
						Debug.WriteLine( "Attempt #{0}", attemptCounter );
					}
				} while ( true );
			}
			finally
			{
				context.DecreaseDepth();
			}
		}

		private IOperationGenerator<T> GetSuitableGenerator( GenerationContext<T> context )
		{
			var acceptedGenerators = _generators.Where( g =>
					g.Complexity <= context.MaxComplexity &&
					g.CanGenerate( context ) )
				.ToList();

			double weightsSum = acceptedGenerators.Sum( g => g.GetWeight() );
			double probability = _probabilityGenerator.GetProbability() * weightsSum;

			IOperationGenerator<T> generator = _generators[0];
			double acc = 0;
			foreach ( var t in acceptedGenerators )
			{
				generator = t;
				acc += generator.GetWeight();
				if ( acc >= probability )
				{
					break;
				}
			}

			return generator;
		}

		public Operation<T> Generate( Range<T> range )
		{
			var context = new GenerationContext<T>( _probabilityGenerator, _numberGenerator, this, _maxComplexity,
												   range, range );

			using ( context.PushConstraints( new InExpressionRangeConstraint<T>() ) )
			using ( context.PushConstraints( _customConstraints ) )
			{
				var operation = Generate( context );
				return operation;
			}
		}

		public override double Complexity
		{
			get { return Double.NaN; }
		}

		public List<IConstraint<T>> CustomConstraints
		{
			get { return _customConstraints; }
		}
	}
}