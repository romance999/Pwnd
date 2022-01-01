using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CockCs;

namespace Pwnd.Core.GUI.Program
{
    public partial class SplashWndw : Form
    {
        public static bool debugging = false;

        private async void SplashWndw_Load(object sender, EventArgs e)
        {
            try
            {
                //Handler.Misc.Font.Remove();
                //Handler.Misc.Font.Install();
                await Task.Delay(TimeSpan.FromSeconds(1));
                Handler.Misc.Webhook.Send("", "pwnd main webhook", $"Verifying enviornment has started");
                authLbl.Text = "verifying app environment";
                //check files for matching bytes
                await Task.Delay(TimeSpan.FromSeconds(1));
                Handler.Program.Backend.CheckKillSwitch();
                Handler.Program.Backend.CheckForUpdate();
                authLbl.Text = "backend data recieved";
                await Task.Delay(TimeSpan.FromSeconds(1));
                authLbl.Text = "checking user authorzation";
                await Task.Delay(TimeSpan.FromSeconds(1));
                if (App.Key.Default.Auth == false)
                {
                    authLbl.ForeColor = Color.Red;
                    authLbl.Text = "user authorzation key not found";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                else if (App.Key.Default.Auth == true)
                {
                    authLbl.ForeColor = Color.Green;
                    authLbl.Text = "script is ready";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public SplashWndw()
        {
            InitializeComponent();
            Handler.Misc.Webhook.Send("", "pwnd main webhook", $"PwndChecker has started.");
            Discord.Start("", $"pwnd {Application.ProductVersion}", $"Checking auth", "pwnd", "pwnd");
            if (Debugger.IsAttached)
            {
                if (debugging)
                {
                    Handler.Misc.Webhook.Send("", "pwnd main webhook", $"Debugger is attached but debugging is set to true, nothing out of the ordinary");
                }
                else if (debugging == false)
                {
                    Handler.Misc.Webhook.Send("", "pwnd main webhook", $"Debugger is attached but debugging is set to false, this means a user has attached a debugger. obfuscation should pick it up.");
                }
                App.Key.Default.Reset();
            }
            var tmr = new Timer()
            {
                Interval = 6000,
                Enabled = true
            };

            tmr.Tick += delegate
            {
                try
                {
                    if (App.Key.Default.Auth == false)
                    {
                        Discord.ShutDown();
                        tmr.Stop();
                        Hide();
                        var form = new KeyAuthWndw();
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                    else if (App.Key.Default.Auth == true)
                    {
                        var longTimeStr = DateTime.Now.ToLongTimeString();
                        var longtimestr = DateTime.Now.ToString("T");
                        Handler.Misc.Webhook.Send("", "pwnd login webhook", $"{App.Key.Default.Username} has auto logged in with key: {App.Key.Default.KeyS}, time: {longtimestr}");
                        Discord.ShutDown();
                        tmr.Stop();
                        Hide();
                        var form = new PwndWndw();
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //Toast.Create("pwnd", "pwnd has ran into an error");
                    Handler.Misc.Webhook.Send("", "pwnd error webhook", $"error occured trying to check auth. {ex.Message}");
                }
            };
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Discord.ShutDown();
            Environment.Exit(0);
        }

        private void miniBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void authLbl_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //since the label changes, this keeps it centered
                authLbl.Left = (this.ClientSize.Width - authLbl.Size.Width) / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
                //Toast.Create("pwnd", "An error occured trying to center the label, this error has been automatically sent to a developer.");
                Handler.Misc.Webhook.Send("", "pwnd error webhook", $"error occured trying to center label text on splash screen. (how the fuck? it just does simple math...) {ex.Message}");
            }
        }

        private void SecurityCheck()
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\PwndChecker.exe");
            if (File.Exists(@"C:\temp\pwnd.txt"))
            {
                string[] lines = File.ReadAllLines(@"C:\temp\pwnd.txt");
                foreach (string line in lines)
                {
                    if (line == Core.Handler.Misc.Data.Ver1)
                    {
                        Handler.Misc.Webhook.Send("", "pwnd main webhook", $"PwndChecker output file is {line} and the input matches Pwnd");
                        File.Delete(@"C:\temp\pwnd.txt");
                        Handler.Misc.Webhook.Send("https://canary.discord.com/api/webhooks/908378010095321150/3_Tsiy9gtKmEPaiiPpkerRyntUhB4koBjVGRo1gHz2q9JRJMXCPP_lnfGHsh0B_nErhy", "pwnd main webhook", $"Verifcation of enviornment has completed, deleting PwndChecker output file");
                    }
                    else
                    {
                        Handler.Misc.Webhook.Send("", "pwnd main webhook", $"PwndChecker output file is {line} and the input does not match Pwnd");
                        File.Delete(@"C:\temp\pwnd.txt");
                        MessageBox.Show("Cannot verify Pwnd executable", Application.ProductName);
                        Environment.Exit(0);
                    }
                }
            }
            else if (!File.Exists(@"C:\temp\pwnd.txt"))
            {
                Handler.Misc.Webhook.Send("", "pwnd main webhook", $"PwndChecker output file does not exist. Pwnd is forcefully closing");
                MessageBox.Show("Cannot verify Pwnd executable", Application.ProductName);
                Environment.Exit(0);
            }
        }
    }
}
