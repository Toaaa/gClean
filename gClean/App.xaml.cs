using KeyAuth;
using System;
using System.Windows;

namespace gClean
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Login loginWindow = new Login();
            loginWindow.Show();
        }
    }
}
