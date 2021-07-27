using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

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
        public bool InstallHKX2Blender = true;
        public Control button;

        public int SetProgress;
        public ProgressBar progressBar;
        static string temp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BMCtemp";
        static string installPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BasicModCreator";
        public Setup()
        {
            InitializeComponent();
            Directory.CreateDirectory(temp);
            txtBoxInstallPath.PlaceholderText = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BasicModCreator";
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
        private void btnInstallHKX2blender_Click(object sender, EventArgs e)
        {
            if (InstallHKX2Blender == true)
            {
                InstallHKX2Blender = false;
                btnInstallHKX2blender.Text = "➕";
                BasicToolsTip.SetToolTip(btnInstallHKX2blender, "Install HKX2 Blender Addon (false)");
            }
            else
            {
                InstallHKX2Blender = true;
                btnInstallHKX2blender.Text = "✔";
                BasicToolsTip.SetToolTip(btnInstallHKX2blender, "Install HKX2 Blender Addon (true)");
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
        private async void btnNext_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            System.IO.Directory.CreateDirectory(temp);

            if (txtBoxInstallPath.Text != "")
            {
                installPath = txtBoxInstallPath.Text;
            }

            await DownloadFiles();
            //await InstallFiles();
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
        public async Task DownloadFiles()
        {
            progressBar = progressBar1;
            using (var client = new WebClient())
            {
                await Task.Run(() => client.DownloadFile("https://raw.githubusercontent.com/ArchLeaders/Breath-of-the-Wild-Basic-Mod-Creator/master/Basic%20Mod%20Creator%20Setup%20Wizard/Data/files.ini", temp + "\\files.ini"));
                SetProgress = 10;
                progressTimer.Start();
            }
            string[] links = File.ReadAllLines(temp + "\\files.ini");

            string linksE = null;
            foreach (var link in links)
            {
                if (link.StartsWith("https://"))
                {
                    string[] filenameX = link.Split('/');
                    string fileName = filenameX[filenameX.Length - 1];

                    using (var client = new WebClient())
                    {
                        await Task.Run(() => client.DownloadFile(link, temp + "\\" +
                            fileName.Replace("CreateCollisionAndNavmesh.exe", "MinGW_DLLs\\CreateCollisionAndNavmesh.exe")));
                    }

                    if (fileName.EndsWith(".zip"))
                    {
                        await Task.Run(() => ZipFile.ExtractToDirectory(temp + "\\" + fileName, temp + "\\" + fileName.Replace(".zip", ""), true));
                        File.Delete(temp + "\\" + fileName);
                    }
                    SetProgress = SetProgress + 12;
                }
            }
            File.Delete(temp + "\\files.ini");
            SetProgress = SetProgress + 4;
        }

        #endregion

        #region Move, Copy, Delete Files
        public async Task Install()
        {

        }
        public async Task CleanupFiles()
        {
            progressBar = progressBar3;
            try
            {
                Directory.CreateDirectory(installPath);
                if (InstallHKX2Blender == true)
                {
                    string[] blenderVer = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Blender Foundation\\Blender");
                    Clipboard.SetText(blenderVer[0] + "\\scripts\\addons\\blenderaddon_hkx2tools");
                    Task mvHKX2 = Task.Run(() => Directory.Move(temp + "\\blenderaddon_hkx2tools\\blenderaddon_hkx2tools", blenderVer[0] + "\\scripts\\addons\\blenderaddon_hkx2tools"));
                    SetProgress = 2;
                }

                if (InstallObjLibYes == true)
                {
                    Task mvObjData = Task.Run(() => Directory.Move(temp + "\\ObjLib", installPath + "\\data"));
                    SetProgress = SetProgress + 2;
                }

                Task mvMinGW = Task.Run(() => Directory.Move(temp + "\\MinGW_DLLs", installPath + "\\collision"));
                SetProgress = SetProgress + 1;

                if (InstallPyYes == true)
                {
                    string ExtendedArgs = null;

                    #region Define extended arguments ExtendedArgs
                    string pyInstallPath = "C:\\Python-3.7";

                    if (checkBox1.Checked == true)
                    {
                        ExtendedArgs = ExtendedArgs + " InstallAllUsers=1";
                    }
                    if (checkBox2.Checked == false)
                    {
                        ExtendedArgs = ExtendedArgs + " Include_doc=0";
                    }
                    if (checkBox3.Checked == false)
                    {
                        ExtendedArgs = ExtendedArgs + " Shortcuts=0";
                    }
                    if (txtBoxPyInstallPath.Text != "")
                    {
                        pyInstallPath = txtBoxPyInstallPath.Text;
                    }
                    #endregion

                    Process InstallPy = new Process();
                    InstallPy.StartInfo.FileName = "cmd.exe";
                    InstallPy.StartInfo.UseShellExecute = true;
                    InstallPy.StartInfo.CreateNoWindow = true;
                    InstallPy.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    InstallPy.StartInfo.Arguments = "/k \"" + temp + "\\python-3.9.6-amd64.exe /quiet TargetDir=\"" + txtBoxPyInstallPath.Text + "\"" + ExtendedArgs + " && EXIT";
                    if (InstallVisualRedistYes == true) { SetProgress = SetProgress + 95; }
                    else { SetProgress = SetProgress + 50; }
                    await Task.Run(() => InstallPy.Start());
                    await InstallPy.WaitForExitAsync();
                }

                if (InstallVisualRedistYes == true)
                {
                    Process InstallVCR = new Process();
                    InstallVCR.StartInfo.FileName = "cmd.exe";
                    InstallVCR.StartInfo.UseShellExecute = true;
                    InstallVCR.StartInfo.CreateNoWindow = true;
                    InstallVCR.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    InstallVCR.StartInfo.Arguments = "/k \"" + temp + "\\vc_redist.x64.exe /install /quiet /norestart && EXIT";
                    if (InstallPyYes == true) { SetProgress = SetProgress + 95; }
                    else { SetProgress = SetProgress + 45; }
                    await Task.Run(() => InstallVCR.Start());
                    await InstallVCR.WaitForExitAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion

        #region Game Paths Setup



        #endregion
    }
}
