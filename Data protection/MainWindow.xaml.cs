using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        private void Cipher_Button_Click(object sender, RoutedEventArgs e)
        {
            int key = 0;
            if(CaesarSelection.IsChecked == true)
            {
                int.TryParse(CaesarKey.Text, out key);
            }
            OutputBox.Text = Utils.Cipher(InputBox.Text, CaesarSelection.IsChecked, VigenerSelection.IsChecked, key, Keyword.Text);
        }

        private void Decipher_Button_Click(object sender, RoutedEventArgs e)
        {
            int key = 0;
            if (CaesarSelection.IsChecked == true && notBruteforce.IsChecked == true)
            {
                int.TryParse(CaesarKey.Text, out key);
            }
            OutputBox.Text = Utils.Decipher(InputBox.Text, CaesarSelection.IsChecked, VigenerSelection.IsChecked, notBruteforce.IsChecked, key, Keyword.Text);
        }

        private async void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                try
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        String line = await sr.ReadToEndAsync();
                        InputBox.Text = line;
                    }
                }
                catch (Exception ex)
                {
                    InputBox.Text = "Could not read the file";
                }
            }
        }
    }
}
