using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace gClean
{
    public partial class SettingsWindow : Window
    {
        private readonly string garrysModPath;

        public SettingsWindow()
        {
            InitializeComponent();
            garrysModPath = textbox.Text;
        }

        private void DeleteDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            else
            {
                var ew = new Error();
                ew.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string workshopPath = "\\garrysmod\\cache\\workshop";
            string fullPath = garrysModPath + workshopPath;
            DeleteDirectory(fullPath);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string addonsPath = "\\garrysmod\\addons";
            string fullPath = garrysModPath + addonsPath;
            DeleteDirectory(fullPath);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmResult = MessageBox.Show("Are you sure you want to delete G-Mod?", "Confirm Delete", MessageBoxButton.YesNo);

            if (confirmResult == MessageBoxResult.Yes)
            {
                DeleteDirectory(garrysModPath);
            }
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textbox.Text = Properties.Settings.Default.pathtxt;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.pathtxt = textbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
