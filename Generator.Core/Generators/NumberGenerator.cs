using Generator.Core.Operations;

namespace Generator.Core.Generators
{
	public sealed class NumberGenerator : OperationGeneratorBase<int>
	{
		public override Operation<int> Generate( GenerationContext<int> context )
		{
			int value = context.NextValue();
			return new Number<int>( value );
		}

		public override double Complexity
		{
			get { return Complexities.NumberComplexity; }
		}
	}
}