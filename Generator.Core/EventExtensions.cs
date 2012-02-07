using System.ComponentModel;

namespace Generator.Core
{
	public static class EventExtensions
	{
		public static void Raise( this PropertyChangedEventHandler evt, object sender, string propertyName )
		{
			if ( evt != null )
			{
				evt( sender, new PropertyChangedEventArgs( propertyName ) );
			}
		}
	}
}