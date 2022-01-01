using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pwnd.Core.GUI.Program.PwndWndw;

namespace Pwnd.Core.Handler.Plugins
{
    public class Help : IPlugin
    {
        public void Go(string parameters)
        {
            foreach (IPlugin plugin in PluginLoader.Plugins)
            {
                MessageBox.Show($"{plugin.Name}, {plugin.Explanation}", "Plugin Loader");
            }
        }

        public string Name
        {
            get
            {
                return "help";
            }
        }

        public string Explanation
        {
            get
            {
                return "This plugin shows all loaded plugins and their explanations";
            }
        }
    }
}
