using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CockCs;

namespace Pwnd.Core.Handler.Input
{
    public static class Keyboard
    {
        public static string[] keyArray = new string[] {"W", "A", "S", "D"};
        public static string[] wordArray = new string[] { ".", "cock", "fuck you", "kys", "fucked your mom", "you have a small dick", "get good", "ur bad at the game", "stop crying", "bozo", "rip bozo" };

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);
        [STAThread]


        public static void AntiAfk()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("RustClient");
                while (Handler.Misc.Data.antiafk == true)
                {

                    foreach (Process proc in processes)
                    {
                        SetForegroundWindow(proc.MainWindowHandle);
                        SendKeys.SendWait("A");
                        Thread.Sleep(300);
                        SendKeys.SendWait("A");
                        Thread.Sleep(300);
                        SendKeys.SendWait("D");
                        Thread.Sleep(300);
                        SendKeys.SendWait("D");
                        Thread.Sleep(300);
                        SendKeys.SendWait("W");
                        Thread.Sleep(300);
                        SendKeys.SendWait("S");
                        Thread.Sleep(300);
                        SendKeys.SendWait("S");
                        Thread.Sleep(300);
                        SendKeys.SendWait("W");
                        Thread.Sleep(300);
                        SendKeys.SendWait("1");
                        Thread.Sleep(300);
                        SendKeys.SendWait("1");
                        Thread.Sleep(300);
                        Mouse.KeyPress(9); //9 is vkeycode for tab
                        Thread.Sleep(300);
                        Mouse.KeyPress(9);
                        Thread.Sleep(300);
                    }
                    Thread.Sleep(300);
                }
                if (Handler.Misc.Data.antiafk == false)
                {

                }
            }
            catch (Exception ex)
            {
                 //Toast.Create("pwnd", "pwnd ran into an error when running static anti afk function");
                MessageBox.Show(ex.Message, Application.ProductName);
            }
        }

        public static void RandomAntiAfk()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("RustClient");
                while (Handler.Misc.Data.antiafk == true)
                {
                    foreach (Process proc in processes)
                    {
                        Random random = new Random();
                        int value = random.Next(0, keyArray.Length);
                        SendKeys.SendWait(keyArray[value]);
                    }
                    Thread.Sleep(50);
                }
                if (Handler.Misc.Data.antiafk == false)
                {

                }
            }
            catch (Exception ex)
            {
                //Toast.Create("pwnd", "pwnd ran into an error when running random anti afk function");
                MessageBox.Show(ex.Message, Application.ProductName);
                Handler.Misc.Webhook.Send("", "pwnd error webhook", $"error occured trying to execute random afk function {ex.Message}");
            }
        }

        public static void ChatSpam()
        {
            try
            {
                Process[] processes = Process.GetProcessesByName("RustClient");
                while (Handler.Misc.Data.spam == true)
                {
                    foreach (Process proc in processes)
                    {
                        //13 is the keycode for enter
                        Random random = new Random();
                        int value = random.Next(0, keyArray.Length);
                        SendKeys.SendWait("13"); // open chat
                        SendKeys.SendWait(wordArray[value]);
                        SendKeys.SendWait("13"); // close chat
                        SendKeys.SendWait("13"); // open chat
                        SendKeys.SendWait(wordArray[value]);
                        SendKeys.SendWait("13"); //close chat
                    }
                    Thread.Sleep(3000); //most chats make you wait 3 seconds
                }
                if (Handler.Misc.Data.antiafk == false)
                {

                }
            } 
            catch (Exception ex)
            {
                //Toast.Create("pwnd", "pwnd ran into an error when running chat spam function");
                MessageBox.Show(ex.Message, Application.ProductName);
                Handler.Misc.Webhook.Send("", "pwnd error webhook", $"error occured trying to execute chat spam function. {ex.Message}");
            }
        }
    }
}
