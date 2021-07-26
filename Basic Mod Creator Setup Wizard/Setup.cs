using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Mod_Creator_Setup_Wizard
{
    public partial class Setup : Form
    {
        public bool InstallPyYes = true;
        public bool InstallVisualRedistYes = true;
        public bool InstallObjLibYes = true;
        public bool InstallPyAppsYes = true;
        public bool AddBCMToPATHYes = true;
        public bool InstallGUIYes = true;
        public Control button;

        public int SetProgress;
        public ProgressBar progressBar;
        public static string temp()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMCtemp";
        }
        public Setup()
        {
            InitializeComponent();
            panel2.Visible = false;
            panel3.Visible = false;
        }
        #region Switches | Done
        //Python Install Switch
        private void SwitchPy37_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (InstallPyYes == false)
            {
                InstallPyYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallPyYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        private void btnPy37_Click(object sender, EventArgs e)
        {
            button = SwitchPy37;
            if (InstallPyYes == false)
            {
                InstallPyYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallPyYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        //Visual Redist Switch
        private void SwitchVisualRedist_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (InstallVisualRedistYes == false)
            {
                InstallVisualRedistYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallVisualRedistYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }

        private void btnVisualRedist_Click(object sender, EventArgs e)
        {
            button = SwitchVisualRedist;
            if (InstallVisualRedistYes == false)
            {
                InstallVisualRedistYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallVisualRedistYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        //Install Object Library Switch
        private void SwitchBMCObjLib_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (InstallObjLibYes == false)
            {
                InstallObjLibYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallObjLibYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }

        private void btnBCMLObjLib_Click(object sender, EventArgs e)
        {
            button = SwitchBMCObjLib;
            if (InstallObjLibYes == false)
            {
                InstallObjLibYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallObjLibYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        //Install Python Applications Switch
        private void SwicthInstallPyApps_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (InstallPyAppsYes == false)
            {
                InstallPyAppsYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallPyAppsYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }

        private void btnInstallPyApps_Click(object sender, EventArgs e)
        {
            button = SwitchInstallPyApps;
            if (InstallPyAppsYes == false)
            {
                InstallPyAppsYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallPyAppsYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        //Add to path
        private void SwicthAddBCMtoPATH_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (AddBCMToPATHYes == false)
            {
                AddBCMToPATHYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                AddBCMToPATHYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }

        private void btnAddBCMtoPATH_Click(object sender, EventArgs e)
        {
            button = SwicthAddBCMtoPATH;
            if (AddBCMToPATHYes == false)
            {
                AddBCMToPATHYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                AddBCMToPATHYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        //Install GUI - WIP
        private void SwicthInstallGUIisWIP_Click(object sender, EventArgs e)
        {
            button = (Button)sender;
            if (InstallGUIYes == false)
            {
                InstallGUIYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallGUIYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }

        private void btnInstallGUIisWIP_Click(object sender, EventArgs e)
        {
            button = SwicthInstallGUIisWIP;
            if (InstallGUIYes == false)
            {
                InstallGUIYes = true;
                button.Location = new Point(button.Location.X + 22, button.Location.Y);
            }
            else
            {
                InstallGUIYes = false;
                button.Location = new Point(button.Location.X - 22, button.Location.Y);
            }
        }
        #endregion

        #region Python Install Settings

        private void btnPy37Menu_Click(object sender, EventArgs e)
        {
            if (panel4.Visible == false)
            {
                btnPy37Menu.Text = "➖";
                panel4.Visible = true;
            }
            else
            {
                btnPy37Menu.Text = "➕";
                panel4.Visible = false;
            }

        }
        private void btnBrowsePythonPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            DialogResult result = browse.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtBoxPyInstallPath.Text = browse.SelectedPath;
            }
        }

        #endregion

        #region Next button and Folder Browse Dialog, Progress Timer Tick Events
        private void btnBrowseInstallPath_Click(Object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            DialogResult result = browse.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtBoxInstallPath.Text = browse.SelectedPath;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            System.IO.Directory.CreateDirectory(temp());

            progressBar = progressBar1;
            SetProgress = 20;
            progressTimer.Start();
        }
        private void btnCancelPg2_Click(object sender, EventArgs e)
        {
            DialogResult msgBox = MessageBox.Show("Are you sure you wish to cancel?", "WARNING", MessageBoxButtons.YesNo);
            if (msgBox == DialogResult.Yes)
            {
                panel2.Visible = false;
                progressTimer.Stop();
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnRestartSetup_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void progressTimer_Tick(object sender, EventArgs e)
        {
            if (SetProgress != progressBar.Value)
            {
                progressBar.Value = progressBar.Value + 1;
            }
        }

        #endregion

        #region Links | Done
        private void lnkHyRe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://github.com/NiceneNerd/Hyrule-Builder")
            {
                UseShellExecute = true,
            };
            Process.Start(ps);
        }

        private void lnkHKX2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://github.com/krenyy/HKX2")
            {
                UseShellExecute = true,
            };
            Process.Start(ps);
        }

        private void lnkYazIt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://github.com/NiceneNerd/yaz-it")
            {
                UseShellExecute = true,
            };
            Process.Start(ps);
        }

        private void inkCemu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://cemu.info/")
            {
                UseShellExecute = true,
            };
            Process.Start(ps);
        }

        private void inkServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://discord.com/invite/vPzgy5S")
            {
                UseShellExecute = true,
            };
            Process.Start(ps);
        }
        #endregion

        #region Download files
        //Required files link array
        public static async Task DownloadFiles()
        {
            string[] links = System.IO.File.ReadAllLines(temp() + "\\files.ini");

            string linksE = null;
            foreach (var link in links)
            {
                linksE = linksE + link + "\n";
            }

            MessageBox.Show(linksE);
        }        

        #endregion

        #region Move, Copy, Delete Files
        #endregion

        #region Game Paths Setup



        #endregion
    }
}
