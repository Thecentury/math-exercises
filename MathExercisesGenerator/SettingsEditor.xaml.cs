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

		private void SaveButtonClick( object sender, RoutedEventArgs e )
		{
			Settings.Default.Save();
			Close();
		}

		private void CancelButtonClick( object sender, RoutedEventArgs e )
		{
			Settings.Default.Reset();
			Close();
		}
	}
}
