using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pwnd.Core.Handler.Misc
{
    public static class Font
    {
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)]
                                         string lpFileName);

        [DllImport("gdi32.dll", EntryPoint = "RemoveFontResourceW", SetLastError = true)]
        public static extern int RemoveFontResource([In][MarshalAs(UnmanagedType.LPWStr)]
                                            string lpFileName);

        public static int result = -1;
        public static int error = 0;

        public static void Install()
        {
            try
            {
                result = AddFontResource(@"\\badcache.ttf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Remove()
        {
            try
            {
                result = AddFontResource(@"\\badcache.ttf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
