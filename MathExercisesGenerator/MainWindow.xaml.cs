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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Generator.Core;
using MathExercisesGenerator.Properties;

namespace MathExercisesGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Loaded += MainWindowLoaded;
		}

		private void MainWindowLoaded( object sender, RoutedEventArgs e )
		{
			CreateExamples();
		}

		private void CreateExamples()
		{
			var settings = Settings.Default;

			ApplicationViewModel vm = new ApplicationViewModel(
				Range.Create( settings.MinValue, settings.MaxValue ),
				settings.ExercisesCount, settings.Complexity );

			DataContext = vm;
		}

		private void MoreExercisesButtonClick( object sender, RoutedEventArgs e )
		{
			CreateExamples();
		}

		private void WindowKeyDown( object sender, KeyEventArgs e )
		{
			if ( e.Key == Key.F1 )
			{
				SettingsEditor editor = new SettingsEditor {DataContext = Settings.Default};
				editor.ShowDialog();
			}
		}
	}
}
