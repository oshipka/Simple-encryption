using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Data_protection
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ActivateCalculations(object sender, RoutedEventArgs e)
		{
			try
			{
				var numbers = new Generator();
				pLabel.Content = numbers.GetP().ToString();
				aLabel.Content = numbers.GetA().ToString();
				ALabel.Content = numbers.GetRandA().ToString();
				BLabel.Content = numbers.GetRandB().ToString();
				XLabel.Content = numbers.GetX().ToString();
				YLabel.Content = numbers.GetY().ToString();
				kLabel.Content = numbers.GetK().ToString();
				kpLabel.Content = numbers.GetKP().ToString();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message + "\n" + exception.StackTrace, "Error", MessageBoxButton.OK,
					MessageBoxImage.Error);
            }
		}
	}
}
