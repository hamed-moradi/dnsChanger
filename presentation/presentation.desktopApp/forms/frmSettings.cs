using domain.office.containers;
using domain.office.entities;
using presentation.desktopApp.helper;
using presentation.desktopApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentation.desktopApp.forms {
    public partial class frmSettings: Form {
        #region ctor
        private AppConfiguration _savedConf;
        private readonly AppConfigurationContainer _appConfContainer;
        public frmSettings() {
            _savedConf = new AppConfiguration();
            _appConfContainer = new AppConfigurationContainer();

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
    }
}
