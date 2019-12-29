using domain.office.entities;
using Microsoft.WindowsAPICodePack.Net;
using presentation.desktopApp.forms;
using presentation.desktopApp.helper;
using presentation.desktopApp.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using static presentation.desktopApp.helper.DNSService;

namespace presentation.desktopApp {
    public partial class MainForm: Form {
        #region ctor
        private frmSettings _frmSettings;
        private NetworkAdapter _connectedNetwork;
        private bool _linkedDNS = true;
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
            cmbNetworkConnection.SelectedIndexChanged += CmbNetworkConnection_SelectedIndexChanged;
        }

        private ToolStripItem[] SettingUpContextMenu() {
            var contextMenu = new ToolStripItem[2];

            var toolStripSettings = new ToolStripMenuItem {
                Text = Resources.Settings
            };
            toolStripSettings.Click += ToolStripSettings_Click;
            contextMenu[0] = toolStripSettings;

            var toolStripExit = new ToolStripMenuItem {
                Text = Resources.Exit
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

            picLink.Image = new Icon(Resources.link, 21, 21).ToBitmap();

            iptxtPreferredDNS.Items.AddRange(new object[] { "178.22.122.100" });
            iptxtAlternateDNS.Items.AddRange(new object[] { "185.51.200.2" });
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
            var dnsSrv = new DNSService();

            byte priority = 1;
            var networkCollection = NetworkListManager.GetNetworks(NetworkConnectivityLevels.All);
            foreach(var network in networkCollection) {
                var netAdapter = new NetworkAdapter { Name = network.Name, AdapterId = network.NetworkId, IsConnected = network.IsConnectedToInternet };
                cmbNetworkConnection.Items.Add(network.Name);
                if(network.IsConnected && network.IsConnectedToInternet) {
                    netAdapter.ConnectionCollection = network.Connections;
                    netAdapter.Description = dnsSrv.Current(network.Connections).Description;
                    netAdapter.DnsAddress = dnsSrv.Get(network.Connections);
                    netAdapter.Priority = priority;
                    priority++;
                }
                _networkAdapters.Add(netAdapter);
            }

            _connectedNetwork = _networkAdapters.SingleOrDefault(s => s.Priority == 1);
            SetCurrentDNSIPs(_connectedNetwork.DnsAddress);
            SetConnectionImage();
        }

        private string[] GetDNSIPs() {
            var isPreferredValid = IPAddress.TryParse(iptxtPreferredDNS.Value, out var preferredDNSIP);
            var isAlternatedValid = IPAddress.TryParse(iptxtAlternateDNS.Value, out var alternatedDNSIP);
            var ips = new List<string>();
            if(isPreferredValid) {
                ips.Add(preferredDNSIP.ToString());
            }
            if(isAlternatedValid) {
                ips.Add(alternatedDNSIP.ToString());
            }
            return ips.ToArray();
        }
        private void SetDNSIPs(string dns) {
            var dnsList = dns.Split(',');
            SetDNSIPs(dnsList);
        }
        private void SetDNSIPs(string[] dns) {
            if(!string.IsNullOrWhiteSpace(dns[0]))
                iptxtPreferredDNS.Value = dns[0];
            if(!string.IsNullOrWhiteSpace(dns[1]))
                iptxtAlternateDNS.Value = dns[1];
        }
        private void SetCurrentDNSIPs(string dns) {
            var dnsList = dns.Split(',');
            SetCurrentDNSIPs(dnsList);
        }
        private void SetCurrentDNSIPs(string[] dns) {
            if(!string.IsNullOrWhiteSpace(dns[0]))
                lblCurrentPreferredIP.Text = dns[0];
            if(!string.IsNullOrWhiteSpace(dns[1]))
                lblCurrentAlternateIP.Text = dns[1];
        }
        private void ChangeEnable(bool status) {
            cmbNetworkConnection.Enabled = status;
            iptxtPreferredDNS.Enabled = status;
            iptxtAlternateDNS.Enabled = status;
            NotifyIconHandler.Instance.Icon = status ? Resources.app_disconnected : Resources.app_connected;
        }

        private void SetConnectionImage() {
            if(_connectedNetwork == null) {
                picConnection.Image = new Icon(Resources.disconnected, 21, 21).ToBitmap();
            }
            else {
                picConnection.Image = Bitmap.FromHicon(new Icon(Resources.connected, 21, 21).Handle);
                cmbNetworkConnection.SelectedItem = _connectedNetwork.Name;
            }
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
            if(_connectedNetwork == null) {
                MyMessageBox.Show(this, Resources.NoConnection, Resources.Attention, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ips = GetDNSIPs();
            var returnValue = new DNSService().Set(_connectedNetwork.Description, ips);
            switch(returnValue) {
                case NetworkAdapterConfigurationReturnValue.SuccessfulCompletionNoRebootRequired:
                    SetCurrentDNSIPs(new DNSService().Get(_connectedNetwork.ConnectionCollection));
                    //ChangeEnable(false);
                    break;
                default:
                    MyMessageBox.Show(this, Resources.ResourceManager.GetString(returnValue.ToString("g")), Resources.Attention, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            Hide();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            NotifyIconHandler.Instance.NotifyIcon.Visible = false;
            Application.Exit();
        }

        private void BtnTray_Click(object sender, EventArgs e) {
            Hide();
        }

        private void ToolStripExit_Click(object sender, EventArgs e) {
            Close();
            Application.Exit();
        }

        private void BtnSettings_Click(object sender, EventArgs e) {
            ShowSettings();
        }

        private void CmbNetworkConnection_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedNetwork = _networkAdapters.SingleOrDefault(s => s.Name == cmbNetworkConnection.SelectedItem.ToString());
            if(selectedNetwork.IsConnected) {
                _connectedNetwork = selectedNetwork;
            }
            else {
                if(MyMessageBox.Show(Resources.YoureGoingToDC, Resources.AreYouSure,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    new DNSService().Set(_connectedNetwork.Description, null);
                    _connectedNetwork = null;
                    ChangeEnable(false);
                }
            }
            SetConnectionImage();
        }

        private void picLink_Click(object sender, EventArgs e) {
            if(_linkedDNS) {
                _linkedDNS = false;
                picLink.Image = new Icon(Resources.broken_link, 21, 21).ToBitmap();
            }
            else {
                _linkedDNS = true;
                picLink.Image = new Icon(Resources.link, 21, 21).ToBitmap();
            }
        }
    }
}
