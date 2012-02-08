using System.Windows.Markup;
using Generator.Core.Operations;

namespace Generator.Core.Generators
{
	[ContentProperty( "Inner" )]
	public class ComplexityGeneratorWrapper<T> : OperationGeneratorBase<T>
	{
		private IOperationGenerator<T> _inner;

		public ComplexityGeneratorWrapper() { }

		public sealed override Operation<T> Generate( GenerationContext<T> context )
		{
			var result = _inner.Generate( context );
			return result;
		}

		public sealed override double Complexity
		{
			get { return CustomComplexity; }
		}

		public double CustomComplexity { get; set; }

		public IOperationGenerator<T> Inner
		{
			get { return _inner; }
			set { _inner = value; }
		}

		public sealed override bool CanGenerate( GenerationContext<T> context )
		{
			bool result = _inner.CanGenerate( context );
			return result;
		}
	}

	public sealed class IntComplexityGeneratorWrapper : ComplexityGeneratorWrapper<int>
	{
		public IntComplexityGeneratorWrapper()
		{
			
		}

		public IntComplexityGeneratorWrapper(IOperationGenerator<int> inner, double complexity )
		{
			Inner = inner;
			CustomComplexity = complexity;
		}
	}
}