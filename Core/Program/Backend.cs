using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pwnd.Core.Handler.Program
{
    public static class Backend
    {
        public static void CheckForUpdate()
        {
            try
            {
                WebClient client = new WebClient();
                if (client.DownloadString("").Contains("pwnd Version = 1.1"))
                {

                }
                else
                {
                    MessageBox.Show($"There is an update for {Application.ProductName}, you are required to download it for security reasons.", Application.ProductName);
                    //download
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void CheckKillSwitch()
        {
            WebClient client = new WebClient();
            if (client.DownloadString("").Contains("pwnd Killswitch = true"))
            {
                MessageBox.Show($"Kill switch is enabled, {Application.ProductName} cannot be used currently.", Application.ProductName);
                Environment.Exit(0);
            }
            else
            {
                return;
            }
        }
    }
}
