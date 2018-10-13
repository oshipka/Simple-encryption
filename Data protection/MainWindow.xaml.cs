using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Data_protection
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow: INotifyPropertyChanged
	{
		private string key;
		private string alphabet;

		
		public string Key
		{
			get { return key; }
			set
			{
				if (key == value) return;
				key = value;
				OnPropertyChanged("KeyValue");
			}
		}
		

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Cipher_Button_Click(object sender, RoutedEventArgs e)
		{
			KeyInputBox.Visibility = Visibility.Visible;/*
			if (HillSelection.IsChecked == true)
			{
				Ciphers.Cipher.HillMethod();
			}
			else if (GammaSelection.IsChecked == true)
			{
				Ciphers.Cipher.GammaMethod();
			}*/
		}

		private void Decipher_Button_Click(object sender, RoutedEventArgs e)
		{
			KeyInputBox.Visibility = Visibility.Visible;
			if (HillSelection.IsChecked == true)
			{
				Ciphers.Decipher.HillMethod();
			}
			else if (GammaSelection.IsChecked == true)
			{
				Ciphers.Decipher.GammaMethod();
			}
		}
		
		private void Ok(object sender, RoutedEventArgs e)
		{
			KeyInputBox.Visibility = Visibility.Collapsed;
			InputBox.Text = Key;
			KeyInput.Text = string.Empty;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			KeyInputBox.Visibility = Visibility.Collapsed;
			KeyInput.Text = string.Empty;
		}

		private async void OpenFile(object sender, RoutedEventArgs e)
		{
			var dlg = new OpenFileDialog
			{
				DefaultExt = ".txt",
				Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
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
				InputBox.Text = "Could not read the file: " + ex.Message;
			}
		}

		private void SaveFile(object sender, RoutedEventArgs e)
		{
			var dlg = new SaveFileDialog
			{
				Filter = "Text file (*.txt)|*.txt",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if (dlg.ShowDialog() == true)
				File.WriteAllText(dlg.FileName, OutputBox.Text);
		}

		private void EnterKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				InputBox.Text = InputBox.Text + "\n";
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string info)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(info));
		}
	}
}