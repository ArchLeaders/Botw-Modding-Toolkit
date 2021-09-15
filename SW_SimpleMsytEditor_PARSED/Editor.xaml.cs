using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace SW_Msyt_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public string InSessionFileName = "MsytTextFile.msyt";
        public string InSessionFile = "MsytTextFile.msyt";
        public string ExportFormat = "wiiu";

        #region Fix Window Sixe in fullscreen.
        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>x coordinate of point.</summary>
            public int x;
            /// <summary>y coordinate of point.</summary>
            public int y;
            /// <summary>Construct a point of coordinates (x,y).</summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public static readonly RECT Empty = new RECT();
            public int Width { get { return Math.Abs(right - left); } }
            public int Height { get { return bottom - top; } }
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }
            public bool IsEmpty { get { return left >= right || top >= bottom; } }
            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }
            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode() => left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2) { return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom); }
            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2) { return !(rect1 == rect2); }
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #endregion

        public Editor()
        {
            InitializeComponent();

            SourceInitialized += (s, e) =>
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };

            btnMinimize.Click += (s, e) => WindowState = WindowState.Minimized;
            btnFullScreen.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            btnExitApplication.Click += (s, e) => Environment.Exit(1);

            using (var stream = File.OpenRead("x64\\msyt.xml"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    rtbMain.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            using (var stream = File.OpenRead("x64\\dtext.xml"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    rtbDeserializedText.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            tbEditor_Speech_Text.Text = "I'm sorry, usually we have a stock of\nfresh Pumpkins but recently my sister\nhas been having some pest issues.";
            tbEditor_Speech_Option_1.Text = "Hmm, OK.";
            tbEditor_Speech_Option_2.Text = "Alright then.";
            tbEditor_Speech_Option_3.Text = "Oh, I see.";
            tbEditor_Speech_Option_4.Text = "Byė.";
            tbEditor_Speech_Near.Text = "Are ye kidd'n me?";
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                btnFullScreen.Content = "'";
            }
            else
            {
                btnFullScreen.Content = "\"";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F12:
                    System.Windows.Forms.FolderBrowserDialog defaultOut = new();
                    defaultOut.SelectedPath = "Output";
                    if (defaultOut.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        File.WriteAllText("x64\\settings.ini", defaultOut.SelectedPath);
                    }
                    break;
            }
        }

        #region Left Panel

        private void btnShowEditor_Click(object sender, RoutedEventArgs e)
        {
            if (winEditorView.Visibility == Visibility.Visible)
            {
                winEditorView.Visibility = Visibility.Hidden;
                winMsytView.Visibility = Visibility.Visible;
                btnShowEditor.Content = "Show Editor";
            }
            else
            {
                winEditorView.Visibility = Visibility.Visible;
                winMsytView.Visibility = Visibility.Hidden;
                btnShowEditor.Content = "Show Msyt";
            }
        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            MSTT.GetText();
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openMsyt = new();
            openMsyt.Filter = "BotW Text Files|*.msyt;*.msbt|MSYT|*.msyt|MSBT|*.msbt";

            if (openMsyt.ShowDialog() == true)
            {
                string[] str = openMsyt.FileName.Split('.');
                string extension = str[str.Length - 1];

                if (extension == "msyt")
                {
                    rtbMain.Text = File.ReadAllText(openMsyt.FileName);
                }
                else if (extension == "msbt")
                {
                    Process proc = new();
                    proc.StartInfo.FileName = "x64\\msyt.exe";
                    proc.StartInfo.Arguments = "export \"" + openMsyt.FileName + "\"";
                    proc.StartInfo.CreateNoWindow = true;

                    proc.Start();
                    await proc.WaitForExitAsync();
                    rtbMain.Text = File.ReadAllText(openMsyt.FileName.Replace(".msbt", ".msyt"));
                    File.Delete(openMsyt.FileName.Replace(".msbt", ".msyt"));
                }

                InSessionFileName = openMsyt.SafeFileName;
                InSessionFile = openMsyt.FileName;
            }
        }

        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                File.WriteAllText("x64\\.app\\" + InSessionFileName.Replace(".msbt", ".msyt"), rtbMain.Text);

                Process proc = new();
                proc.StartInfo.FileName = "x64\\msyt.exe";
                proc.StartInfo.Arguments = "create \"x64\\.app\\" + InSessionFileName.Replace(".msbt", ".msyt") + "\" --output \"x64\\.app\\" + InSessionFileName.Replace(".msyt", ".msbt") + "\" --platform " + ExportFormat;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                await proc.WaitForExitAsync();

                string defaultOut = File.ReadAllLines("x64\\settings.ini")[0];

                Directory.Delete("x64\\.app\\" + InSessionFileName.Replace(".msyt", ".msbt") + ".bak");
                File.Delete("x64\\.app\\" + InSessionFileName.Replace(".msbt", ".msyt"));
                File.Move("x64\\.app\\" + InSessionFileName.Replace(".msyt", ".msbt"), defaultOut + "\\" + InSessionFileName.Replace(".msyt", ".msbt"), true);
            }
            else
            {
                SaveFileDialog export = new();
                export.Filter = "MSYT|*.msyt|MSBT|*.msbt";
                export.FileName = InSessionFileName.Replace("msyt", "msbt");
                export.FilterIndex = 2;

                if (export.ShowDialog() == true)
                {
                    if (export.FileName.EndsWith(".msyt"))
                    {
                        File.WriteAllText(export.FileName, rtbMain.Text);
                    }
                    else if (export.FileName.EndsWith(".msbt"))
                    {
                        File.WriteAllText("x64\\.app\\" + export.SafeFileName + ".msyt", rtbMain.Text);

                        Process proc = new();
                        proc.StartInfo.FileName = "x64\\msyt.exe";
                        proc.StartInfo.Arguments = "create \"x64\\.app\\" + export.SafeFileName + ".msyt\" --output \"x64\\.app\\" + export.SafeFileName + "\" --platform " + ExportFormat;
                        proc.StartInfo.CreateNoWindow = true;

                        proc.Start();
                        await proc.WaitForExitAsync();

                        Directory.Delete("x64\\.app\\" + export.SafeFileName + ".bak");
                        File.Delete("x64\\.app\\" + export.SafeFileName + ".msyt");
                        File.Move("x64\\.app\\" + export.SafeFileName, export.FileName, true);
                    }
                }
            }
        }

        private async void rtbMain_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true) && e.Data.GetData(DataFormats.FileDrop, true) is string[] fileNames)
            {
                string[] str = fileNames[0].Split('.');
                string extension = str[str.Length - 1];

                if (extension == "msyt")
                {
                    rtbMain.Text = File.ReadAllText(fileNames[0]);
                    //rtbMain.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(fileNames[0]))));
                }
                else if (extension == "msbt")
                {
                    Process proc = new();
                    proc.StartInfo.FileName = "x64\\msyt.exe";
                    proc.StartInfo.Arguments = "export \"" + fileNames[0] + "\"";
                    proc.StartInfo.CreateNoWindow = true;

                    proc.Start();
                    await proc.WaitForExitAsync();
                    rtbMain.Text = File.ReadAllText(fileNames[0].Replace(".msbt", ".msyt"));
                    File.Delete(fileNames[0].Replace(".msbt", ".msyt"));
                }
            }
        }

        #endregion
    }
}
