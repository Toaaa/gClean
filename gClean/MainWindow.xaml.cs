using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace gClean
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private static readonly ILogger _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<MainWindow>();
        public MainWindow()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void timer_tick(object sender, EventArgs e)
        {

        }

        public static string path = "";

        private void CleanCheatsCheck_Checked(object sender, RoutedEventArgs e)
        { }

        private void CleanCheatsCheck_Unchecked(object sender, RoutedEventArgs e)
        { }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool KeepAddons = AddonsCheck.IsChecked.Value;
            bool ClearCache = ClearCheck.IsChecked.Value;
            // bool CleanCheats = CleanCheatsCheck.IsChecked.Value;
            string path = textbox.Text;

            if (KeepAddons)
            {
                string addonsPath = Path.Combine(path, "addons");
                cleaner(true, addonsPath);
            }

            if (ClearCache)
            {
                string cachePath = Path.Combine(path, "cache");
                cleaner(false, cachePath);

                string keyName = @"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
                if (key != null)
                {
                    key.Close();
                    Registry.CurrentUser.DeleteSubKeyTree(keyName);
                }
            }

            // if (CleanCheatsCheck.IsChecked.HasValue && CleanCheatsCheck.IsChecked.Value)
            // {
            //     CleanCheats();
            // }

            var result = new LoadingWindow();
            result.Show();
        }

        private void Button_MouseHover(object sender, EventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.pathtxt = textbox.Text;
            Properties.Settings.Default.Save();
        }

        // Improved Window_Loaded function
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.pathtxt))
            {
                textbox.Text = Properties.Settings.Default.pathtxt;
            }
            else
            {
                try
                {
                    Properties.Settings.Default.pathtxt = LocateGMod();
                    textbox.Text = Properties.Settings.Default.pathtxt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var lw = new LoadingWindow();
            lw.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var sw = new SettingsWindow();
            sw.Show();
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void cbox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void cbox_Checked1(object sender, RoutedEventArgs e)
        {

        }

        private void cbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void cbox_Unchecked1(object sender, RoutedEventArgs e)
        {

        }

        private async void CleanCheats(object sender, RoutedEventArgs e)
        {
            string keyName = @"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU";

            using (var key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.DeleteSubKeyTree(keyName);
                }
            }

            string[] directoriesToDelete = new string[] {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OinkIndustries"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LemiProject"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Desmod"),
                Path.Combine("C:\\", "exechack"),
                Path.Combine("C:\\", "GaztoofScripthook"),
                Path.Combine("C:\\Program Files (x86)\\Steam\\steamapps\\common\\GarrysMod", "Memoriam"),
                Path.Combine("C:\\", "GMOD-SDK-Settings")
            };

            foreach (string directory in directoriesToDelete)
            {
                try
                {
                    if (Directory.Exists(directory))
                    {
                        await Task.Run(() => Directory.Delete(directory, true));
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception instead of writing to the console
                    _logger.LogError(ex, $"Failed to delete directory {directory}");
                }
            }

            var loadingWindow = new LoadingWindow();
            loadingWindow.Show();
        }


        // Improved cleaner function
        static void cleaner(bool keepAddons, string path)
        {
            string[] directoriesToDelete = new string[]
            {
                "data",
                "cache\\lua",
                "download\\user_custom"
            };

            string[] filesToDelete = new string[]
            {
                "cl.db",
                "mn.db",
                "sv.db"
            };

            List<string> excludeFromDelete = new List<string>
            {
                "sb_dupes",
                "sb_adverts"
            };

            if (!Directory.Exists(path))
            {
                var ew = new Error();
                ew.Show();
                return;
            }

            foreach (string directoryName in directoriesToDelete)
            {
                string directoryPath = Path.Combine(path, "garrysmod", directoryName);
                if (Directory.Exists(directoryPath) && !excludeFromDelete.Contains(directoryName))
                {
                    Directory.Delete(directoryPath, true);
                }
            }

            if (!keepAddons)
            {
                string addonsPath = Path.Combine(path, "garrysmod", "addons");
                if (Directory.Exists(addonsPath))
                {
                    Directory.Delete(addonsPath, true);
                }
            }

            foreach (string fileName in filesToDelete)
            {
                string filePath = Path.Combine(path, "garrysmod", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }



        // Improved locategmod function
        private string LocateGMod()
        {
            string[] possibleDirectories =
            {
                "Program Files (x86)\\Steam\\steamapps\\common\\GarrysMod",
                "Program Files\\Steam\\steamapps\\common\\GarrysMod",
                "Steam\\steamapps\\common\\GarrysMod",
                "Games\\SteamLibrary\\steamapps\\common\\GarrysMod",
                "SteamLibrary\\steamapps\\common\\GarrysMod",
                "SteamApps\\common\\GarrysMod"
            };
            string directory = "";
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                foreach (string possibleDirectory in possibleDirectories)
                {
                    if (Directory.Exists(Path.Combine(d.Name, possibleDirectory)))
                    {
                        directory = Path.Combine(d.Name, possibleDirectory);
                        return directory;
                    }
                }
            }
            throw new Exception("gClean couldn't find your GMod installation. Please enter it manually");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var inj = new Injector();
            inj.Show();
        }

    }
}