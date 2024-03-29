using System.ComponentModel;
using Generator.Core;

namespace MathExercisesGenerator
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