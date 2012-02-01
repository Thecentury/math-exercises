namespace Generator.Core
{
	public interface IVisitor<out TOut>
	{
		TOut VisitCore( dynamic input );
	}
}