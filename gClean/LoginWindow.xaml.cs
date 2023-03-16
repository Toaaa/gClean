
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using KeyAuth_WPF.Core;

namespace KeyAuth_WPF.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        static string name = "";
        static string ownerid = "";
        static string secret = "";
        static string version = "1.0";

        public static api KeyAuthApp = new api(name, ownerid, secret, version);


        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            KeyAuthApp.init();
            if (KeyAuthApp.checkblack())
            {
                this.Close();
            }

            LicenseBox.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Hidden;
            BackButtonIcon.Visibility = Visibility.Hidden;
            UsernameBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Visible;
            ButtonAlreadyAcc.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            PackIconSignIn.Kind = PackIconKind.Login;
            SignInText8.Text = " Login";
            Settings.AlreadyAcc = "1";
        }

        private void CaptionButtons_Click(object sender, RoutedEventArgs e)
        {
          
            Button button = (Button)sender;
            if (button.Content is PackIcon pI)
            { 
                switch (pI.Kind)
                {
                    case PackIconKind.WindowClose:
                        BeginStoryboard sb = this.FindResource("CloseAnim") as BeginStoryboard;
                        sb.Storyboard.Begin();
                        Close();
                        break;
                    case PackIconKind.WindowMinimize:
                        WindowState = WindowState.Minimized;
                        break;
                }
            }
        } 
        

        private bool ClosingAnimationFinished;

        private void FinishedClosingAnimation(object sender, EventArgs e)
        {
            ClosingAnimationFinished = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ClosingAnimationFinished)
            {
                BeginStoryboard sb = this.FindResource("CloseAnim") as BeginStoryboard;
                sb.Storyboard.Begin();
                e.Cancel = true;
            }
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.AlreadyAcc == "0")
            {
                if (KeyAuthApp.register(UsernameBox.Text, PasswordBox.Password, LicenseBox.Text))
                {
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else if (Settings.AlreadyAcc == "1")
            {
                if (KeyAuthApp.login(UsernameBox.Text, PasswordBox.Password))
                {
                    new MainWindow().Show();
                    this.Close();
                } 
                else
                {
                    this.Close();
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonAlreadyAcc.Visibility = Visibility.Collapsed;
            LicenseBox.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            BackButtonIcon.Visibility = Visibility.Visible;
            PackIconSignIn.Kind = PackIconKind.RegisterOutline;
            LicenseBox.Margin = new Thickness(20, 20, 20, 0);
            SignInText8.Text = " Register";
            Settings.AlreadyAcc = "0";
        }


        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            LicenseBox.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Hidden;
            BackButtonIcon.Visibility = Visibility.Hidden;
            UsernameBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Visible;
            ButtonAlreadyAcc.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            PackIconSignIn.Kind = PackIconKind.Login;
            SignInText8.Text = " Login";
            Settings.AlreadyAcc = "1";
        }
    }
}