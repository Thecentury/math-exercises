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
		private ApplicationViewModel _viewModel;

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

			_viewModel = new ApplicationViewModel(
				Range.Create( settings.MinValue, settings.MaxValue ),
				settings.ExercisesCount, settings.Complexity );

			DataContext = _viewModel;
		}

		private void MoreExercisesButtonClick( object sender, RoutedEventArgs e )
		{
			CreateExamples();
		}

		private void WindowKeyDown( object sender, KeyEventArgs e )
		{
			if ( e.Key == Key.F2 )
			{
				SettingsEditor editor = new SettingsEditor { DataContext = Settings.Default, Owner = this };
				if ( editor.ShowDialog() == true )
				{
					CreateExamples();
				}
			}
			else if ( e.Key == Key.F5 )
			{
				// refresh
				CreateExamples();
			}
			else if ( e.Key == Key.Space && Keyboard.IsKeyDown( Key.LeftCtrl ) )
			{
				var firstUnsolved = _viewModel.Exercises.FirstOrDefault(ex => !ex.IsCorrect);
				if ( firstUnsolved != null )
				{
					firstUnsolved.Solve();
				}
			}
		}
	}
}
