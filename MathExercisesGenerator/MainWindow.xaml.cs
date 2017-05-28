using System.Linq;
using System.Windows;
using System.Windows.Input;
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
				settings.ExercisesCount, Range.Create( settings.MinComplexity, settings.MaxComplexity ) );

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
			else if ( e.Key == Key.Space && (Keyboard.IsKeyDown( Key.LeftCtrl ) || Keyboard.IsKeyDown( Key.RightCtrl )) )
			{
				var firstUnsolved = _viewModel.Exercises.FirstOrDefault( ex => !ex.IsCorrect );
				if ( firstUnsolved != null )
				{
					firstUnsolved.Solve();
				}
			}
		}
	}
}
