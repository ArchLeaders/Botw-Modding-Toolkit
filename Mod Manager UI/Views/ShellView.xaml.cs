using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public Color background { get; set; } 

        public static DispatcherTimer timer = new DispatcherTimer();
        public bool back = false;

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

        public ShellView()
        {
            SoundPlayer splayer = new(@"C:\Users\HP USER\Downloads\winxp.wav");
            splayer.Play();

            InitializeComponent();

            SourceInitialized += (s, e) =>
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };

            btnMinimize.Click += (s, e) => WindowState = WindowState.Minimized;
            btnFullScreen.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            btnExitApplication.Click += (s, e) => Close();
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

        #region Menu Items

        private void btnExpandMenu_Click(object sender, RoutedEventArgs e)
        {
            //Show menu window or resume edit window
            InstallPane.Visibility = Visibility.Collapsed;
            ToolPane.Visibility = Visibility.Collapsed;
            EditPane.Visibility = Visibility.Collapsed;
            FilePane.Visibility = Visibility.Collapsed;


        }

        private void btnFileMenu_Click(object sender, RoutedEventArgs e)
        {
            //Show Open, New | Save, Save As
            if (FilePane.Visibility == Visibility.Collapsed)
            {
                //Hide
                InstallPane.Visibility = Visibility.Collapsed;
                ToolPane.Visibility = Visibility.Collapsed;
                EditPane.Visibility = Visibility.Collapsed;

                //Show
                FilePane.Visibility = Visibility.Visible;
            }
            else
            {
                FilePane.Visibility = Visibility.Collapsed;
            }
        }

        private void btnEditMenu_Click(object sender, RoutedEventArgs e)
        {
            //Show Undo, Redo | Copy, Cut, Paste | Select All
            if (EditPane.Visibility == Visibility.Collapsed)
            {
                //Hide
                InstallPane.Visibility = Visibility.Collapsed;
                ToolPane.Visibility = Visibility.Collapsed;
                FilePane.Visibility = Visibility.Collapsed;
                
                //Show
                EditPane.Visibility = Visibility.Visible;
            }
            else
            {
                EditPane.Visibility = Visibility.Collapsed;
            }
        }

        private void btnToolMenu_Click(object sender, RoutedEventArgs e)
        {
            //Show New Mod | Collision Tool, Dea to FBX, Actor Tool, All (->)
            if (ToolPane.Visibility == Visibility.Collapsed)
            {
                //Hide
                InstallPane.Visibility = Visibility.Collapsed;
                EditPane.Visibility = Visibility.Collapsed;
                FilePane.Visibility = Visibility.Collapsed;

                //Show
                ToolPane.Visibility = Visibility.Visible;
            }
            else
            {
                ToolPane.Visibility = Visibility.Collapsed;
            }
        }

        private void btnInstallMenu_Click(object sender, RoutedEventArgs e)
        {
            //Show Ice-Spears (Silent Princess), botw-actor-tool, Cemu, All (->)
            if (InstallPane.Visibility == Visibility.Collapsed)
            {
                //Hide
                EditPane.Visibility = Visibility.Collapsed;
                ToolPane.Visibility = Visibility.Collapsed;
                FilePane.Visibility = Visibility.Collapsed;

                //Show
                InstallPane.Visibility = Visibility.Visible;
            }
            else
            {
                InstallPane.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        private void ShowHidePreview_Click(object sender, RoutedEventArgs e)
        {
            if (Preview.Width == 240)
            {
                DataStack.Visibility = Visibility.Visible;
                Preview.Width = 610;
            }
            else if (Preview.Width == 610)
            {
                Preview.Width = 240;
                DataStack.Visibility = Visibility.Collapsed;
            }
        }

        private void btnExpandGBPosts_MouseEnter(object sender, MouseEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            back = false;

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //1140
            if (back == false && this.Width <= 1400)
            {
                this.Width = this.Width + 20;
            }
            else if (back == true && this.Width >= 1140)
            {
                this.Width = this.Width - 20;
            }
            else { timer.Stop(); }
        }

        private void ExpandGBPosts_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            back = true;

            timer.Start();
        }
    }
}
