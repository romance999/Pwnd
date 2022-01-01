using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pwnd.Core.GUI.Program.PwndWndw;
using System.Windows.Forms;

namespace Pwnd.Core.Handler.Plugins.Embedded
{
    public class StrLengthPlugin : IPlugin
    {
        public string Explanation
        {
            get
            {
                return "Gets a string as parameter and returns the string length in characters.";
            }
        }

        public string Name
        {
            get
            {
                return "strlength";
            }
        }

        public void Go(string parameters)
        {
            MessageBox.Show(parameters.Length.ToString(), "Plugin Loader");        
        }
    }
}
