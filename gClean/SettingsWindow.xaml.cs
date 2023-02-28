using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace gClean
{
    /// <summary>
    /// Interaktionslogik für SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string spath = textbox.Text;
            string wpath = "\\garrysmod\\cache\\workshop";
            if (Directory.Exists(spath))
            {
                var lw = new LoadingWindow();
                Directory.Delete(spath + wpath, true);
                lw.Show();
            }
            else
            {
                var ew = new Error();
                ew.Show();
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmResult = MessageBox.Show("Are you sure you want to delete G-Mod?", "Confirm Delete", MessageBoxButton.YesNo);

            if (confirmResult == MessageBoxResult.Yes)
            {
                string spath = textbox.Text;
                if (Directory.Exists(spath))
                {
                    var lw = new LoadingWindow();
                    Directory.Delete(spath, true);
                    lw.Show();
                }
                else
                {
                    var ew = new Error();
                    ew.Show();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string spath = textbox.Text;
            if (Directory.Exists(spath + "\\garrysmod\\addons"))
            {
                var lw = new LoadingWindow();
                Directory.Delete(spath + "\\garrysmod\\addons", true);
                lw.Show();
            }
            else
            {
                var ew = new Error();
                ew.Show();
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
