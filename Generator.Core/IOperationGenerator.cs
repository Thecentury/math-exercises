namespace Generator.Core
{
	public interface IOperationGenerator<T>
	{
		Operation<T> Generate( GenerationContext<T> context );
		double Complexity { get; }
	}
}