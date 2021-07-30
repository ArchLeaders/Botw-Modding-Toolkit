using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BMCLibrary.BCM;

namespace BasicModCreator_UI
{
    class UI_Controls
    {
        #region Strings, Bools, integers

        static string applicationPath = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("\\BasicModCreator-UI.dll", "");
        public static string toolName = null;
        public static string[] paths = File.ReadAllLines(applicationPath + "\\data\\paths.txt");

        public static List<string> botwFiles = new List<string>();
        public static List<Control> tools = new List<Control>();

        #endregion

        #region startup process

        public static async Task getFiles(ListBox listBox)
        {
            foreach (var item in File.ReadAllLines(applicationPath + "\\data\\botw.bin"))
            {
                await Task.Run(() => botwFiles.Add(GetName(item)));
            }
        }

        #endregion

        #region Search Methods

        public static void search(string searchTerm, ListBox searchObject)
        {
            for (int i = 0; i < searchObject.Items.Count; i++)
            {
                if (searchObject.Items[i].ToString().ToLower().StartsWith(searchTerm.ToLower()))
                {
                    searchObject.SetSelected(i, true);
                    return;
                }
            }
        }

        public static async Task searchTools(string searchTerm, List<Control> searchObject)
        {

        }

        #endregion

        #region Install methods

        public static void OpenInstallerForm(object sender)
        {
            Button btn = (Button)sender;
            toolName = btn.Text;

            BasicConsoleForm console = new BasicConsoleForm();
            console.Show();
        }

        #endregion
    }
}
