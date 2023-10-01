using KeyAuth;
using System.Windows;

namespace gClean
{
    public partial class StartUp : Window
    {
        protected override void OnStartUp(StartupEventArgs e)
        {
            base.OnStartUp(e);

            Login login = new Login();
            login.Show();
        }
    }
}