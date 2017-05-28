using System;
using System.Windows;

namespace MathExercisesGenerator
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup( StartupEventArgs e )
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler( CurrentDomain_UnhandledException );
		}

		void CurrentDomain_UnhandledException( object sender, UnhandledExceptionEventArgs e )
		{
			MessageBox.Show( e.ExceptionObject.ToString() );
		}
	}
}
