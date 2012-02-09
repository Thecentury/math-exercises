using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
