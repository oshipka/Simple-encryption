using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Data_protection
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private bool _triedBruteForce;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Cipher_Button_Click(object sender, RoutedEventArgs e)
		{
			var key = 0;
			if(CaesarSelection.IsChecked == true)
			{
				int.TryParse(CaesarKey.Text, out key);
			}
			OutputBox.Text = Utils.Cipher(InputBox.Text, CaesarSelection.IsChecked, VigenerSelection.IsChecked, key, Keyword.Text);
		}

		private void Decipher_Button_Click(object sender, RoutedEventArgs e)
		{
			var key = 0;
			if (CaesarSelection.IsChecked == true && notBruteforce.IsChecked == true)
			{
				int.TryParse(CaesarKey.Text, out key);
			}

			if (!_triedBruteForce)
			{
				OutputBox.Text = Utils.Decipher(InputBox.Text, CaesarSelection.IsChecked, VigenerSelection.IsChecked, notBruteforce.IsChecked, key, Keyword.Text);
				if (notBruteforce.IsChecked==false)
				{
					_triedBruteForce = true;
				}
			}

			if (_triedBruteForce)
			{
				OutputBox.Text = Utils.Decipher(OutputBox.Text, CaesarSelection.IsChecked, VigenerSelection.IsChecked, notBruteforce.IsChecked, key, Keyword.Text);
			}
}

		private async void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new Microsoft.Win32.OpenFileDialog
			{
				DefaultExt = ".txt", Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
			};


			var result = dlg.ShowDialog();

			if (result != true) return;
			var filename = dlg.FileName;
			try
			{
				using (var sr = new StreamReader(filename))
				{
					var line = await sr.ReadToEndAsync();
					InputBox.Text = line;
				}
			}
			catch (Exception ex)
			{
				InputBox.Text = "Could not read the file: "+ex.Message;
			}
		}

		private void btnSaveFile_Click(object sender, RoutedEventArgs e)
		{
			var dlg = new SaveFileDialog
			{
				Filter = "Text file (*.txt)|*.txt",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if(dlg.ShowDialog() == true)
				File.WriteAllText(dlg.FileName, OutputBox.Text);
		}
	}
}
