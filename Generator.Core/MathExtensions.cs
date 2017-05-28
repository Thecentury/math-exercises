using Microsoft.FSharp.Math;

namespace Generator.Core
{
	public static class MathExtensions
	{
		public static T Max<T>(this INumeric<T> math, T value1, T value2)
		{
			int comparison = math.Compare(value1, value2);
			if ( comparison < 0 )
			{
				return value2;
			}
			else
			{
				return value1;
			}
		}

		public static T Min<T>( this INumeric<T> math, T value1, T value2 )
		{
			int comparison = math.Compare( value1, value2 );
			if ( comparison < 0 )
			{
				return value1;
			}
			else
			{
				return value2;
			}
		}
	}
}
