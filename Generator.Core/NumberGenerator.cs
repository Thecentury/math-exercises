namespace Generator.Core
{
	public sealed class NumberGenerator : IOperationGenerator<int>
	{
		public Operation<int> Generate( GenerationContext<int> context )
		{
			int value = context.NextValue();
			return new Number( value );
		}

		public double Complexity
		{
			get { return Complexities.NumberComplexity; }
		}
	}
}