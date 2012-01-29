using System.Text;

namespace Generator.Core
{
	public interface IRandomNumberGenerator<T>
	{
		T Generate( T min, T max );
	}
}
