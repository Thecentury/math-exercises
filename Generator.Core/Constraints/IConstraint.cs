using Generator.Core.Operations;

namespace Generator.Core.Constraints
{
	public interface IConstraint<T>
	{
		bool Passes( GenerationContext<T> ctx, Operation<T> op );
	}
}
