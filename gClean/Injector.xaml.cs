using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace gClean
{
    /// <summary>
    /// Interaktionslogik für Injector.xaml
    /// </summary>

    public partial class Injector : Window
    {
        public bool IsInjected
        {
            get;
            private set;
        } = false;
        public bool IsAutoInjecting
        {
            get;
            private set;
        } = false;

        public Injector()
        {
            InitializeComponent();
            InjConsole.IsReadOnly = true;
            InjConsole.Document.Blocks.Clear();
            InjConsole.AppendText("Waiting on Process...");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "dll files (*.dll)|*.dll";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Get the path of specified file
                    string filePath = openFileDialog.FileName;
                    textbox.Text = filePath;
                }
            }
        }
        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Inject_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox.Text) && textbox.Text != "File to Hook (.dll)")
            {
                TryInjecting();
            }
            else
            {
                InjConsole.AppendText(Environment.NewLine + "Invalid DLL filepath!");
            }
        }

        private void AutoInject_Checked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textbox.Text) && textbox.Text != "File to Hook (.dll)")
            {
                if (IsAutoInjecting == false)
                {
                    IsAutoInjecting = true;
                    System.Timers.Timer timer = new System.Timers.Timer
                    {
                        Interval = 1500
                    };
                    timer.Elapsed += timer_Elapsed;
                    timer.Start();
                }
            }
            else
            {
                InjConsole.AppendText(Environment.NewLine + "Invalid DLL filepath!");
            }

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            AutoTryInjecting();
        }

        public void AutoTryInjecting()
        {
            var hl2 = Process.GetProcessesByName("hl2");
            var gmod = Process.GetProcessesByName("gmod");
            if (hl2.Length >= 1 || gmod.Length >= 1 && IsInjected == false)
            {
                Thread.Sleep(7500);
                TryInjecting();
            }
        }
        public void TryInjecting()
        {
            Dispatcher.Invoke(() => {
                try
                {
                    var hl2 = Process.GetProcessesByName("hl2");
                    var gmod = Process.GetProcessesByName("gmod");
                    if (hl2.Length >= 1)
                    {
                        Reloaded.Injector.Injector x86inj = new Reloaded.Injector.Injector(process: hl2[0]);
                        x86inj.Inject(textbox.Text);
                        InjConsole.AppendText(Environment.NewLine + "Detected x86 Process. Injected to hl2.exe");
                        IsInjected = true;
                    }
                    else if (gmod.Length >= 1)
                    {
                        Reloaded.Injector.Injector x64inj = new Reloaded.Injector.Injector(process: gmod[0]);
                        x64inj.Inject(textbox.Text);
                        InjConsole.AppendText(Environment.NewLine + "Detected x64 Process. Injected to gmod.exe");
                        IsInjected = true;
                    }
                    else if (IsAutoInjecting == false)
                    {
                        InjConsole.AppendText(Environment.NewLine + "Couldn't detect a Gmod Process");
                    }
                }
                catch
                {

                }
            });

        }
    }
}