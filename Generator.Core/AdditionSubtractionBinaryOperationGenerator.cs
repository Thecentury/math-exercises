using System;
using JetBrains.Annotations;

namespace Generator.Core
{
	public sealed class AdditionSubtractionBinaryOperationGenerator : IOperationGenerator<int>
	{
		public Operation<int> Generate( GenerationContext<int> context )
		{
			var operands = context.Generate( 2 );
			var left = operands[0];
			var right = operands[1];

			BinaryOperation<int> operation;
			bool isAddition = context.ProbabilityGenerator.GetBool();
			if ( isAddition )
			{
				operation = new AddOperation( left, right );
			}
			else
			{
				operation = new SubtractOperation( left, right );
			}

			return operation;
		}

		public double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}
	}

	public interface IConstraint<T>
	{
		bool Passes( Operation<T> operation );
	}

	public sealed class InRangeConstraint<T> : IConstraint<T>
	{
		private readonly Range<T> _range;

		public InRangeConstraint( [NotNull] Range<T> range )
		{
			if ( range == null ) throw new ArgumentNullException( "range" );
			_range = range;
		}

		public bool Passes( Operation<T> operation )
		{
			var value = operation.Evaluate();
			bool passes = _range.Includes( value );
			return passes;
		}
	}
}