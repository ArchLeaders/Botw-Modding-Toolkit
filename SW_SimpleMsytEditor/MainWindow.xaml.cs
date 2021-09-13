using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using SW_SimpleMsytEditor.Highlighting;
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

namespace SW_SimpleMsytEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Control Lists / Themes

        List<Button> buttons = new();
        List<Border> marks = new();
        List<Button> windowButtons = new();

        public void SetTheme(string _btnForeground, string _mainBackground, string _leftBackground, string _chromeBackground, string _markBackground, string _winForeground)
        {
            Brush btnForeground = (SolidColorBrush)new BrushConverter().ConvertFrom(_btnForeground);
            Brush mainBackground = (SolidColorBrush)new BrushConverter().ConvertFrom(_mainBackground);
            Brush leftBackground = (SolidColorBrush)new BrushConverter().ConvertFrom(_leftBackground);
            Brush chromeBackground = (SolidColorBrush)new BrushConverter().ConvertFrom(_chromeBackground);
            Brush markBackground = (SolidColorBrush)new BrushConverter().ConvertFrom(_markBackground);
            Brush winForeground = (SolidColorBrush)new BrushConverter().ConvertFrom(_winForeground);

            foreach (Button button in buttons)
            {
                button.Foreground = btnForeground;
            }
            foreach (Button button in windowButtons)
            {
                button.Foreground = winForeground;
            }
            foreach (Border border in marks)
            {
                border.Background = markBackground;
            }

            rtbMain.Background = mainBackground;
            LeftToolPanel.Background = leftBackground;
            winChromeColor.Background = chromeBackground;
        }

        #endregion

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

        public MainWindow()
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

            buttons.Add(btnShowEditor);
            buttons.Add(btnTranslate);
            buttons.Add(btnImport);
            buttons.Add(btnExport);
            buttons.Add(btnSettings);

            windowButtons.Add(btnMinimize);
            windowButtons.Add(btnFullScreen);
            windowButtons.Add(btnExitApplication);

            marks.Add(mk1_1);
            marks.Add(mk1_2);
            marks.Add(mk1_3);
            marks.Add(mk1_4);

            marks.Add(mk2_1);
            marks.Add(mk2_2);
            marks.Add(mk2_3);
            marks.Add(mk2_4);

            if (File.Exists("Theme.ini"))
            {
                string[] file = File.ReadAllLines("Theme.ini");

                SetTheme(file[0], file[1], file[2], file[3], file[4], file[5]);
            }

            using (var stream = File.OpenRead("Highlighting\\MSYT.xshd"))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    rtbMain.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }
        }

        private void btnShowEditor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {

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
                    //rtbMain.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(openMsyt.FileName))));
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
                File.Move("out\\" + InSessionFileName.Replace(".msyt", ".msbt"), "out\\" + InSessionFileName.Replace(".msyt", ".msbt.bck"), true);
                File.WriteAllText("out\\" + InSessionFileName.Replace(".msbt", ".msyt"), rtbMain.Text);

                Process proc = new();
                proc.StartInfo.FileName = "x64\\msyt.exe";
                proc.StartInfo.Arguments = "create \"out\\" + InSessionFileName.Replace(".msbt", ".msyt") + "\" --output \"out\\" + InSessionFileName.Replace(".msyt", ".msbt") + "\" --platform " + ExportFormat;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                await proc.WaitForExitAsync();

                Directory.Delete("out\\" + InSessionFileName.Replace(".msyt", ".msbt") + ".bak");
                File.Delete("out\\" + InSessionFileName.Replace(".msbt", ".msyt"));
            }
            else
            {
                SaveFileDialog export = new();
                export.Filter = "MSYT|*.msyt|MSBT|*.msbt";
                export.FileName = InSessionFileName;

                if (export.ShowDialog() == true)
                {
                    if (export.FileName.EndsWith(".msyt"))
                    {
                        File.WriteAllText(export.FileName, rtbMain.Text);
                    }
                    else if (export.FileName.EndsWith(".msbt"))
                    {
                        File.WriteAllText(export.FileName.Replace(".msbt", ".msyt"), rtbMain.Text);

                        Process proc = new();
                        proc.StartInfo.FileName = "x64\\msyt.exe";
                        proc.StartInfo.Arguments = "create \"" + export.FileName.Replace(".msbt", ".msyt") + "\" --output \"" + export.FileName + "\" --platform " + ExportFormat;
                        proc.StartInfo.CreateNoWindow = true;

                        proc.Start();
                        await proc.WaitForExitAsync();

                        Directory.Delete(export.FileName + ".bak");
                        File.Delete(export.FileName.Replace(".msbt", ".msyt"));
                    }
                }
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
