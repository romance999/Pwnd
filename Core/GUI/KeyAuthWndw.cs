using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CockCs;

namespace Pwnd.Core.GUI.Program
{
    public partial class KeyAuthWndw : Form
    {
        public KeyAuthWndw()
        {
            InitializeComponent();
            Handler.Program.Backend.CheckKillSwitch();
            Handler.Program.Backend.CheckForUpdate();
            Discord.Start("", $"pwnd {Application.ProductVersion}", $"Authenticating", "pwnd", "pwnd");
        }

        private void KeyAuthWndw_Load(object sender, EventArgs e)
        {
            keyTxtBox.Text = Core.Handler.Misc.Data.key1;
            usernameTxtBox.Text = "BADTEMPER";
        }

        private void keyTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Auth();
            }
        }

        private void usernameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Auth();
            }
        }

        private void Auth()
        {
            try
            {
                if (keyTxtBox.Text.Length < 1 || usernameTxtBox.Text.Length < 1)
                {
                    MessageBox.Show("Please fill out the key text box and username text box", Application.ProductName);
                }
                else
                {
                    if (keyTxtBox.Text == Core.Handler.Misc.Data.key1 || keyTxtBox.Text == Core.Handler.Misc.Data.key2)
                    {
                        var longTimeStr = DateTime.Now.ToLongTimeString();
                        var longtimestr = DateTime.Now.ToString("T");
                        Handler.Misc.Webhook.Send("", "pwnd login webhook", $"{usernameTxtBox.Text} has registered with key: {keyTxtBox.Text}, time: {longtimestr}");
                        Discord.ShutDown();
                        App.Key.Default.KeyS = keyTxtBox.Text;
                        App.Key.Default.Username = usernameTxtBox.Text;
                        App.Key.Default.Auth = true;
                        App.Key.Default.Save();
                        MessageBox.Show("Key auth succuessful", Application.ProductName);
                        Hide();
                        var form = new PwndWndw();
                        form.Closed += (s, args) => this.Close();
                        form.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Toast.Create("pwnd", "pwnd has ran into an error");
                Handler.Misc.Webhook.Send("", "pwnd error webhook", $"error occured trying to authorize user. {ex.Message}");
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void miniBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
