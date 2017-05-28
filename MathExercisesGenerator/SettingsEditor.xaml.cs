using System.Windows;
using System.Windows.Input;
using MathExercisesGenerator.Properties;

namespace MathExercisesGenerator
{
	/// <summary>
	/// Interaction logic for SettingsEditor.xaml
	/// </summary>
	public partial class SettingsEditor : Window
	{
		public SettingsEditor()
		{
			InitializeComponent();
		}

		protected override void OnKeyDown( KeyEventArgs e )
		{
			if ( e.Key == Key.Enter )
			{
				SaveAndClose();
			}
			else if ( e.Key == Key.Escape )
			{
				CancelAndClose();
			}

			base.OnKeyDown( e );
		}

		private void SaveButtonClick( object sender, RoutedEventArgs e )
		{
			SaveAndClose();
		}

		private void SaveAndClose()
		{
			Settings.Default.Save();
			DialogResult = true;
			Close();
		}

		private void CancelButtonClick( object sender, RoutedEventArgs e )
		{
			CancelAndClose();
		}

		private void CancelAndClose()
		{
			Settings.Default.Reset();
			DialogResult = false;
			Close();
		}
	}
}
