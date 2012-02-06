namespace Generator.Core
{
	public sealed class NumberGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate( GenerationContext<int> context )
		{
			int value = context.NextValue();
			return new Number( value );
		}

		public override double Complexity
		{
			get { return Complexities.NumberComplexity; }
		}
	}
}