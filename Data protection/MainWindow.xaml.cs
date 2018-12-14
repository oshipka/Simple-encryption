using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace Data_protection
{
    public partial class MainWindow
    {
        private int _key;
        private string _alphabet;
        private bool _action;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cipher_Button_Click(object sender, RoutedEventArgs e)
        {
            _action = true;
            KeyInputBox.Visibility = Visibility.Visible;
        }

        private void Decipher_Button_Click(object sender, RoutedEventArgs e)
        {
            _action = false;
            KeyInputBox.Visibility = Visibility.Visible;
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            KeyInputBox.Visibility = Visibility.Collapsed;
            try
            {
               if (!int.TryParse(XInput.Text, out _key));
            }
            catch (Exception exception)
            {
                KeyInputBox.Visibility = Visibility.Visible;
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            try
            {
                if (_action)
                {
                    OutputBox.Text = Ciphers.Cipher.ElGamalMethod(InputBox.Text, _key);
                    MessageBox.Show("This is your seed. Remember it for the deciphering\n" , "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

                //if (!_action) OutputBox.Text = Ciphers.Decipher.ElGamalMethod(InputBox.Text, _key);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            KeyInputBox.Visibility = Visibility.Collapsed;
            XInput.Text = string.Empty;
        } 

        private async void OpenFile(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog {DefaultExt = ".txt", Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"};

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
            if (dlg.ShowDialog() == true) File.WriteAllText(dlg.FileName, OutputBox.Text);
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) InputBox.Text = InputBox.Text + "\n";
        }
    }
}