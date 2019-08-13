using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shell {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {
        #region ctor
        public MainWindow() {
            InitializeComponent();
            Initialize();
        }

        private void Initialize() {
            var notifyIcon = new System.Windows.Forms.NotifyIcon {
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name),
                Visible = true
            };
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }
        #endregion
        protected override void OnStateChanged(EventArgs e) {
            if(WindowState == WindowState.Minimized) {
                //ShowInTaskbar = false;
                Hide();
            }
            base.OnStateChanged(e);
        }
        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = true;
            Hide();
            //base.OnClosing(e);
        }
        private void NotifyIcon_DoubleClick(object sender, EventArgs e) {
            WindowState = WindowState.Normal;
            Show();
        }
    }
}
