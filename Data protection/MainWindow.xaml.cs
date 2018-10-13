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
			KeyInputBox.Visibility = System.Windows.Visibility.Visible;
		}

		private void YesButton_Click(object sender, RoutedEventArgs e)
		{
			// YesButton Clicked! Let's hide our InputBox and handle the input text.
			KeyInputBox.Visibility = System.Windows.Visibility.Collapsed;

			// Do something with the Input
			String input = KeyInput.Text;
			//MyListBox.Items.Add(input); // Add Input to our ListBox.

			// Clear InputBox.
			KeyInput.Text = String.Empty;
		}

		private void NoButton_Click(object sender, RoutedEventArgs e)
		{
			// NoButton Clicked! Let's hide our InputBox.
			KeyInputBox.Visibility = System.Windows.Visibility.Collapsed;

			// Clear InputBox.
			KeyInput.Text = String.Empty;
		}

		private void Decipher_Button_Click(object sender, RoutedEventArgs e)
		{
			
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

		private void EnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				InputBox.Text += Environment.NewLine;
			}
		}
	}
}
