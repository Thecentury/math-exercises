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
				operation = new SubstractOperation( left, right );
			}

			return operation;
		}

		public double Complexity
		{
			get { return Complexities.AdditionSubtractionComplexity; }
		}
	}
}