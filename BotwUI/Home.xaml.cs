using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BotwUI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();

            #region Window Chrome Events
 
            homeBtnWindowExit.Click += (s, e) => { Hide(); Environment.Exit(1); };
            homeBtnWindowMin.Click += (s, e) => WindowState = WindowState.Minimized;
            homeBtnWindowSize.Click += (s, e) => {

                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            };

            homeWindow.StateChanged += (s, e) => {

                if (WindowState == WindowState.Normal) {
                    homeBtnWindowSize.Content = "'";
                    homeWindowDropShadow.Opacity = 0.3;
                }
                else {
                    homeBtnWindowSize.Content = "\"";
                    homeWindowDropShadow.Opacity = 0;
                }
            };

            #endregion

            mainPanel.Content = new Panels.HomePage();
        }
    }
}
