using System.ComponentModel;

namespace Generator.Core
{
	public abstract class BindingObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged( string propertyName )
		{
			PropertyChanged.Raise( this, propertyName );
		}
	}
}