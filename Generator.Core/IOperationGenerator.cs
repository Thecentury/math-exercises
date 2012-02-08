using Generator.Core.Operations;

namespace Generator.Core
{
	public interface IOperationGenerator<T>
	{
		bool CanGenerate( GenerationContext<T> context );
		Operation<T> Generate( GenerationContext<T> context );
		double Complexity { get; }
	}

	public abstract class OperationGeneratorBase<T>: IOperationGenerator<T>
	{
		#region Implementation of IOperationGenerator<T>

		public virtual bool CanGenerate(GenerationContext<T> context)
		{
			return true;
		}

		public abstract Operation<T> Generate(GenerationContext<T> context);
		public abstract double Complexity { get; }

		#endregion
	}
}