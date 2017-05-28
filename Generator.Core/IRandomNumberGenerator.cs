namespace Generator.Core
{
	public interface IRandomNumberGenerator<T>
	{
		T Generate( Range<T> range );
	}
}
