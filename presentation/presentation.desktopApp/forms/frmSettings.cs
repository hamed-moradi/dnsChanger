using Presentation.DesktopApp.Services;
using Presentation.DesktopApp.Entities;
using Presentation.DesktopApp.Helper;
using Presentation.DesktopApp.Properties;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Presentation.DesktopApp.forms {
    public partial class frmSettings: Form {
        #region ctor
        private int? selectedIP = null;
        private AppConfiguration _savedConf;
        private readonly AppConfigurationContainer _appConfContainer;
        private readonly DNSAddressContainer _dnsAddressContainer;

        public frmSettings() {
            _savedConf = new AppConfiguration();
            _appConfContainer = new AppConfigurationContainer();
            _dnsAddressContainer = new DNSAddressContainer();

            InitializeComponent();

            ChangeDetector();
        }
        #endregion

        #region private
        private void ChangeDetector() {
            foreach(var ctl in grpPreferences.Controls) {
                if(ctl is CheckBox) {
                    ((CheckBox)ctl).CheckedChanged += new EventHandler(Control_Changed);
                }
                else if(ctl is TextBox) {
                    ((TextBox)ctl).TextChanged += new EventHandler(Control_Changed);
                }
            }
        }
        #endregion

        private void Control_Changed(object sender, EventArgs e) {
            btnApply.Enabled = true;
        }

        private void BtnApply_Click(object sender, EventArgs e) {
            var changedConf = new AppConfiguration {
                Id = _savedConf.Id,
                Language = cmbLanguage.SelectedItem.ToString(),
                LaunchOnStartup = chkLaunchOnStartup.Checked,
                SilentConnection = chkSilentConnection.Checked,
                LogPath = txtLogFolder.Text
            };
            try {
                var result = _appConfContainer.Update(changedConf).Result;
                if(result > 0) {
                    btnApply.Enabled = false;
                }
                else {

                }
            }
            catch(Exception ex) {
                // todo: log on text file
                if(MyMessageBox.Show($"{Resources.UnknownErrorHappend} {Resources.ReportIssue}", Resources.Error,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
                    // todo: call web api
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e) {
            if(btnApply.Enabled)
                btnApply.PerformClick();
            btnCancel.PerformClick();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        private void FrmSettings_Load(object sender, EventArgs e) {
            cmbLanguage.Items.AddRange(new object[] { "EN", "FA" });
            _savedConf = _appConfContainer.Get(1).Result;
            switch(_savedConf.Language) {
                case "en-US":
                    cmbLanguage.SelectedItem = "EN";
                    break;
            }
            chkLaunchOnStartup.Checked = _savedConf.LaunchOnStartup;
            chkSilentConnection.Checked = _savedConf.SilentConnection;
            txtLogFolder.Text = Path.GetFullPath(_savedConf.LogPath);
        }

        private void frmSettings_Activated(object sender, EventArgs e) {
        }

        private async void btnSave_Click(object sender, EventArgs e) {
            if(btnSave.Text == "Add") {
                await _dnsAddressContainer.Add(new DNSAddress {
                    IP = ipTextBox.Value,
                    Type = 1,
                    CreatedAt = DateTime.Now
                });
            }
            else if(btnSave.Text == "Update") {
                var row = grdDNSAddress.CurrentRow;
                if(row != null) {
                    var cell = row.Cells["colId"]?.Value;
                    if(cell != null) {
                        await _dnsAddressContainer.Update(new DNSAddress {
                            Id = (int)cell,
                            IP = ipTextBox.Value,
                            Type = cmbType.SelectedItem.ToTypeNumber()
                        });
                    }
                }
            }
            else {

            }
        }
    }
}
