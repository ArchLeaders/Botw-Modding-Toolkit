using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace Mod_Manager_UI.Views
{
    /// <summary>
    /// Interaction logic for Window_NewMod.xaml
    /// </summary>
    public partial class Error : Window
    {
        public Error()
        {
            InitializeComponent();

            btnError_OK.Focus();
            btnError_Close.Click += (s, e) => Close();
        }

        #region UI Fix

        private const uint WM_NCRBUTTONDOWN = 0x0a4;
        private const uint HTCAPTION = 0x02;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            HwndSource hwndSource = HwndSource.FromHwnd(windowHandle);
            hwndSource.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //Find the offset for RightClick when open?
            if ((msg == WM_NCRBUTTONDOWN) && (wParam.ToInt32() == HTCAPTION))
            {
                ShowContextMenu();

                handled = true;
            }

            return IntPtr.Zero;
        }

        private void ShowContextMenu()
        {
            var contextMenu = Resources["WindowContextMenu"] as ContextMenu;
            contextMenu.IsOpen = true;
        }

        private void ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
