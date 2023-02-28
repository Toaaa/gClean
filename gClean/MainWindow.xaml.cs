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
using System.Windows.Threading;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace gClean
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool KeepAddons = AddonsCheck.IsChecked.Value;
            bool ClearCache = ClearCheck.IsChecked.Value;
            string path = textbox.Text;
            cleaner(KeepAddons, path);
            cleaner(ClearCache, path);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string directory = "";
            if (Properties.Settings.Default.pathtxt != "")
            {
                textbox.Text = Properties.Settings.Default.pathtxt;
            }
            else
            {
                Properties.Settings.Default.pathtxt = locategmod(directory);
                textbox.Text = Properties.Settings.Default.pathtxt;
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

        private void Button_Click_69(object sender, RoutedEventArgs e)
        {


            string keyName = @"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\Shell\BagMRU";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
            if (key != null)
            {
                key.Close();
                Registry.CurrentUser.DeleteSubKeyTree(keyName);
            }



            string username = Environment.UserName;

            string opath = $"C:\\Users\\{username}\\AppData\\Roaming\\OinkIndustries";
            string lpath = $"C:\\Users\\{username}\\Documents\\LemiProject";
            string dlpath = $"C:\\Users\\{username}\\Documents\\Desmod";
            string epath = "C:\\exechack";
            string gpath = "C:\\GaztoofScripthook";
            string ggpath = "C:\\GMOD-SDK-Settings";



            if (Directory.Exists(opath))
            {
                Directory.Delete(opath, true);
            }

            if (Directory.Exists(lpath))
            {
                Directory.Delete(lpath, true);
            }

            if (Directory.Exists(dlpath))
            {
                Directory.Delete(dlpath, true);
            }

            if (Directory.Exists(epath))
            {
                Directory.Delete(epath, true);
            }

            if (Directory.Exists(gpath))
            {
                Directory.Delete(gpath, true);
            }

            if (Directory.Exists(ggpath))
            {
                Directory.Delete(ggpath, true);
            }


            var lw = new LoadingWindow();
            lw.Show();




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

        static void cleaner(bool KeepAddons, string path)
        {
 

            string dpath = "\\garrysmod\\data";
            string apath = "\\garrysmod\\addons";
            string clpath = "\\garrysmod\\cache\\lua";
            string upath = "\\garrysmod\\download\\user_custom";


            if (Directory.Exists(path))
            {
                // Removed most stuff from v1.2 as it was too aggressive. Might make it optional later

                if (Directory.Exists(path + dpath))
                {
                    Directory.Delete(path + dpath, true);
                }

                if (Directory.Exists(path + clpath))
                {
                    Directory.Delete(path + clpath, true);
                }

                if (Directory.Exists(path + upath))
                {
                    Directory.Delete(path + upath, true);
                }


                if (KeepAddons == true)
                {
                    if (Directory.Exists(path + apath))
                    {
                        Directory.Delete(path + apath, true);
                    }
                }



                if (File.Exists(path + "\\garrysmod\\cl.db"))
                {
                    File.Delete(path + "\\garrysmod\\cl.db");
                }

                if (File.Exists(path + "\\garrysmod\\mn.db"))
                {
                    File.Delete(path + "\\garrysmod\\mn.db");
                }

                if (File.Exists(path + "\\garrysmod\\sv.db"))
                {
                    File.Delete(path + "\\garrysmod\\sv.db");
                }
            }
            else
            {
                var ew = new Error();
                ew.Show();
            }
        }

        static string locategmod(string directory)
        {
            // Checks possible GMod Installations.

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            try
            {
                foreach (DriveInfo d in allDrives)
                {
                    if (Directory.Exists(d.Name + "Program Files (x86)\\Steam\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "Program Files (x86)\\Steam\\steamapps\\common\\GarrysMod";
                    }
                    else if (Directory.Exists(d.Name + "Program Files\\Steam\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "Program Files\\Steam\\steamapps\\common\\GarrysMod";
                    }
                    else if (Directory.Exists(d.Name + "Steam\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "Steam\\steamapps\\common\\GarrysMod";
                    }
                    else if (Directory.Exists(d.Name + "Games\\Steam\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "Games\\SteamLibrary\\steamapps\\common\\GarrysMod";
                    }
                    else if (Directory.Exists(d.Name + "SteamLibrary\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "SteamLibrary\\steamapps\\common\\GarrysMod";
                    }
                    else if (Directory.Exists(d.Name + "SteamLibrary\\steamapps\\common\\GarrysMod"))
                    {
                        directory = d.Name + "SteamLibrary\\steamapps\\common\\GarrysMod";
                    }
                }
            }
            catch
            {
                MessageBox.Show("gClean couldn't find your GMod installation. Please enter it manually");
            }
            return directory;

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            var inj = new Injector();
            inj.Show();
        }


    }
}

