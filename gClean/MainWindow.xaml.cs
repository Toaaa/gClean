using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;
using System.IO.Compression;
using static KeyAuth.api;

namespace gClean
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void timer_tick(object sender, EventArgs e)
        {

        }



        public static string path = "";

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool KeepAddons = AddonsCheck.IsChecked.Value;
            bool ClearCache = ClearCheck.IsChecked.Value;
            string path = textbox.Text;
            cleaner(KeepAddons, path);
            cleaner(ClearCache, path);
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

        private void Button_Click_69(object sender, RoutedEventArgs e)
        {

            string keyName = @"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
            if (key != null)
            {
                key.Close();
                Registry.CurrentUser.DeleteSubKeyTree(keyName);
            }



            string[] directoriesToDelete = new string[] {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OinkIndustries"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LemiProject"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Desmod"),
                Path.Combine("C:\\", "exechack"),
                Path.Combine("C:\\", "GaztoofScripthook"),
                Path.Combine("C:\\", "GMOD-SDK-Settings"),
                Path.Combine(path, "Memoriam")

            };

            foreach (string directory in directoriesToDelete)
            {
                try
                {
                    if (Directory.Exists(directory))
                    {
                        Directory.Delete(directory, true);
                    }
                }
                catch (Exception ex)
                {
                    // handle exception
                }
            }

            string userName = Environment.UserName;
            string[] directories = { $"C:\\Users\\{userName}\\Recent", "C:\\Windows\\Prefetch", $"C:\\Users\\{userName}\\AppData\\Local" };

            foreach (var directory in directories)
            {
                try
                {
                    foreach (var file in new DirectoryInfo(directory).GetFiles())
                    {
                        if (file.Name.Contains("OinkIndustries") || file.Name.Contains("gClean") || file.Name.Contains("GCLEAN") || file.Name.Contains("Intenso") || file.Name.Contains("ESD-USB") || file.Name.Contains("LemiGMOD")) 
                        {
                            file.Delete();
                        }
                    }
                    var directoryInfo = new DirectoryInfo(directory);

                    if (directoryInfo.Exists)
                    {
                        foreach (var dir in directoryInfo.GetDirectories())
                        {
                            if (dir.Name.Contains("gClean"))
                            {
                                dir.Delete(true);
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // wenn prefetch oder recent komplett gelöscht continued es ;)
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    // wenn ordner nicht gefunden continue
                    continue;
                }
                catch (Exception ex)
                {
                    // handle exception
                    continue;
                }
            }




            {
                string[] targets = { "OINK/login" };


                Process.Start("cmdkey.exe", "/delete:" + targets.First());

            }


            var success = new Success();
            success.Show();


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

        // Improved cleaner function
        static void cleaner(bool KeepAddons, string path)
        {

            string[] directoriesToDelete = new string[]
            {
                "data",
                "cache\\lua",
                "download\\user_custom",
                "saves",
                "resource"
                
            };


            string[] filesToDelete = new string[]
            {
                "cl.db",
                "mn.db",
                "sv.db"
            };

            string[] multihunter = new string[] // suchtbunker multidateien xd
{
                "HL2crosshairs.ttf",
                "HL2MP.ttf",
                "fonts\\Roboto-ThinItalic.ttf",
                "fonts\\Roboto-LightItalic.ttf",
                "fonts\\Roboto-BlackItalic.ttf",
                "fonts\\Roboto-Regular.ttf"



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
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                }
            }


            if (KeepAddons == false)
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

            foreach (string fileName in multihunter)
            {
                string filePath = Path.Combine(path, "garrysmod\\resource", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            // resource downloader
            string url = "https://codeload.github.com/Facepunch/garrysmod/zip/refs/heads/master";
            string zipPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "resource.zip");
            string gpath = path;
            string gmod = "\\garrysmod";
            string dumm = gpath + gmod;
            string extractPath = Path.Combine(gpath, dumm);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, zipPath);
            }

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.StartsWith("garrysmod-master/garrysmod/resource") && entry.Name != "")
                    {
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName.Replace("garrysmod-master/garrysmod/", "")));

                        if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                        {
                            string directoryName = Path.GetDirectoryName(destinationPath);
                            if (!Directory.Exists(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }

                            if (!File.Exists(destinationPath))
                            {
                                using (Stream sourceStream = entry.Open())
                                {
                                    using (FileStream destinationStream = new FileStream(destinationPath, FileMode.Create))
                                    {
                                        sourceStream.CopyTo(destinationStream);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            File.Delete(zipPath);




            var success = new Success();
            success.Show();
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