using gClean;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace KeyAuth
{
    public partial class Login : Form
    {
        public static api KeyAuthApp = new api(
            name: "gClean",
            ownerid: "Z08eenRjz7",
            secret: "c7eb1341b02d3321c880f8cfd3ccb8b94e452ed06bf4fda3fce39a381d7c3128",
            version: "2.3.3"
        );

        private void ShowResponse(string type)
        {
            MessageBox.Show($"It took {api.responseTime} msg to {type}");
        }

        public Login()
        {
            InitializeComponent();
        }

        private void siticoneControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        
        public static bool SubExist(string name)
        {
            if(KeyAuthApp.user_data.subscriptions.Exists(x => x.subscription == name))
                return true;
            return false;
        }
        
        private void Login_Load(object sender, EventArgs e)
        {
            KeyAuthApp.init();

            if (KeyAuthApp.response.message == "invalidver")
            {
                if (!string.IsNullOrEmpty(KeyAuthApp.app_data.downloadLink))
                {
                    DialogResult dialogResult = MessageBox.Show("Yes to open file in browser\nNo to download file automatically", "Auto update", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            Process.Start(KeyAuthApp.app_data.downloadLink);
                            Environment.Exit(0);
                            break;
                        case DialogResult.No:
                            WebClient webClient = new WebClient();
                            string destFile = Application.ExecutablePath;

                            string rand = random_string();

                            destFile = destFile.Replace(".exe", $"-{rand}.exe");
                            webClient.DownloadFile(KeyAuthApp.app_data.downloadLink, destFile);

                            Process.Start(destFile);
                            Process.Start(new ProcessStartInfo()
                            {
                                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = true,
                                FileName = "cmd.exe"
                            });
                            Environment.Exit(0);

                            break;
                        default:
                            MessageBox.Show("Invalid option");
                            Environment.Exit(0);
                            break;
                    }
                }
                MessageBox.Show("Version of this program does not match the one online. Furthermore, the download link online isn't set. You will need to manually obtain the download link from the developer");
                Environment.Exit(0);
            }
            
            if (!KeyAuthApp.response.success)
            {
                MessageBox.Show(KeyAuthApp.response.message);
                Environment.Exit(0);
            }
        }

        static string random_string()
        {
            string str = null;

            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                str += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }
            return str;

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            KeyAuthApp.login(username.Text,password.Text);
            if (KeyAuthApp.response.success)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Hide();
            }
            else
                status.Text = "Status: " + KeyAuthApp.response.message;
        }

        private void key_TextChanged(object sender, EventArgs e)
        {

        }

        private void status_Click(object sender, EventArgs e)
        {

        }
    }
}
