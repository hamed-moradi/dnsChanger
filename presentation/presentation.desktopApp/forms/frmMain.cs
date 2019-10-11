using domain.office.containers;
using domain.office.entities;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.WindowsAPICodePack.Net;
using presentation.desktopApp.forms;
using presentation.desktopApp.helper;
using presentation.desktopApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentation.desktopApp {
    public partial class MainForm: Form {
        #region ctor
        private frmSettings _frmSettings;
        private NetworkAdapter _connectedNetwork;
        private readonly List<NetworkAdapter> _networkAdapters;

        public MainForm() {
            _networkAdapters = new List<NetworkAdapter>();

            InitializeComponent();

            PreparingForm();
            EventBinder();
            SettingFormInit();

            GetWindowsNetworkConnection();
        }
        #endregion

        #region private
        private void EventBinder() {
            NotifyIconHandler.Instance.NotifyIcon.Click += NotifyIcon_Click;
            FormClosed += MainForm_FormClosed;
            btnAction.Click += BtnAction_Click;
        }

        private ToolStripItem[] SettingUpContextMenu() {
            var contextMenu = new ToolStripItem[2];

            var toolStripSettings = new ToolStripMenuItem {
                Text = Properties.Resources.Settings
            };
            toolStripSettings.Click += ToolStripSettings_Click;
            contextMenu[0] = toolStripSettings;

            var toolStripExit = new ToolStripMenuItem {
                Text = Properties.Resources.Exit
            };
            toolStripExit.Click += ToolStripExit_Click;
            contextMenu[1] = toolStripExit;

            return contextMenu;
        }
        private void PreparingForm() {
            NotifyIconHandler.Instance.NotifyIcon.ContextMenuStrip = new ContextMenuStrip();
            NotifyIconHandler.Instance.NotifyIcon.ContextMenuStrip.SuspendLayout();
            NotifyIconHandler.Instance.NotifyIcon.ContextMenuStrip.Items.AddRange(SettingUpContextMenu());
            NotifyIconHandler.Instance.NotifyIcon.ContextMenuStrip.ResumeLayout(false);
        }
        private void ShowSettings() {
            Hide();
            SettingFormInit();
            if(_frmSettings.Visible) {
                _frmSettings.Focus();
            }
            else {
                _frmSettings.Show();
            }
        }
        private void SettingFormInit() {
            if(_frmSettings == null) {
                _frmSettings = new frmSettings();
            }
        }

        private void GetWindowsNetworkConnection() {
            byte priority = 1;
            var networkCollection = NetworkListManager.GetNetworks(NetworkConnectivityLevels.All);
            foreach(var network in networkCollection) {
                var netAdapter = new NetworkAdapter { Name = network.Name, NetworkId = network.NetworkId };
                cmbNetworkConnection.Items.Add(network.Name);
                if(network.IsConnected && network.IsConnectedToInternet) {
                    netAdapter.Priority = priority;
                    priority++;
                }
                _networkAdapters.Add(netAdapter);
            }

            _connectedNetwork = _networkAdapters.SingleOrDefault(s => s.Priority == 1);
            if(_connectedNetwork == null) {
                picConnection.Image = new Icon(Resources.disconnected, 21, 21).ToBitmap();
            }
            else {
                picConnection.Image = Bitmap.FromHicon(new Icon(Resources.connected, 21, 21).Handle);
                cmbNetworkConnection.SelectedItem = _connectedNetwork.Name;
            }
        }

        private void ChangeEnable(bool status) {
            cmbNetworkConnection.Enabled = status;
            iptxtPreferredDNS.Enabled = status;
            iptxtAlternateDNS.Enabled = status;
        }
        #endregion

        private void ToolStripSettings_Click(object sender, EventArgs e) {
            ShowSettings();
        }

        public void NotifyIcon_Click(object sender, EventArgs e) {
            var mouseClick = (MouseEventArgs)e;
            if(mouseClick.Button == MouseButtons.Right) {
                return;
            }
            if(Visible) {
                Hide();
            }
            else {
                int iconWidth = NotifyIconHandler.Instance.Icon.Width,
                    iconHeight = NotifyIconHandler.Instance.Icon.Height,
                    formLocationX = MousePosition.X - (Width / 2) - (iconWidth / 2),
                    formLocationY = MousePosition.Y - Height - iconHeight;

                var point = NotifyIconHandler.Instance.GetLocation(false);
                if(point.HasValue) {
                    formLocationX = point.Value.X - (Width / 2) + (iconWidth / 4);
                    formLocationY = point.Value.Y - Height - (iconHeight / 4);
                }
                SetDesktopLocation(formLocationX, formLocationY);
                Show();
            }
        }

        private void BtnAction_Click(object sender, EventArgs e) {
            switch(btnAction.Tag.ToString().ToLower()) {
                case "connect":
                    if(_connectedNetwork == null) {
                        // todo: show message "You're not connected to the internet"
                        break;
                    }
                    var ips = new List<string>();
                    var isPreferredValid = IPAddress.TryParse(iptxtPreferredDNS.Value, out var preferredDNSIP);
                    var isAlternatedValid = IPAddress.TryParse(iptxtAlternateDNS.Value, out var alternatedDNSIP);
                    if(isPreferredValid) {
                        ips.Add(preferredDNSIP.ToString());
                    }
                    if(isAlternatedValid) {
                        ips.Add(alternatedDNSIP.ToString());
                    }
                    if(ips.Any()) {
                        DNS.Set(ips.ToArray());
                        // todo: change app icon to green one
                        btnAction.Text = Resources.Disconnect;
                        btnAction.Tag = "disconnect";
                        ChangeEnable(false);
                    }
                    break;
                case "disconnect":
                    DNS.Set(null);
                    // todo: change app icon to red one
                    btnAction.Text = Resources.Connect;
                    btnAction.Tag = "connect";
                    ChangeEnable(true);
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            NotifyIconHandler.Instance.NotifyIcon.Visible = false;
            //Application.Exit();
        }

        private void BtnTray_Click(object sender, EventArgs e) {
            Hide();
        }

        private void ToolStripExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void BtnSettings_Click(object sender, EventArgs e) {
            ShowSettings();
        }
    }
}
