namespace presentation.desktopApp.forms {
    partial class frmSettings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.grbUserInterface = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblSettingsDescription = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tapGeneral = new System.Windows.Forms.TabPage();
            this.tapDNS = new System.Windows.Forms.TabPage();
            this.tapSchedule = new System.Windows.Forms.TabPage();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.grbStartup = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grbPreferences = new System.Windows.Forms.GroupBox();
            this.chbSilentConnection = new System.Windows.Forms.CheckBox();
            this.tapAdvanced = new System.Windows.Forms.TabPage();
            this.grbLogFiles = new System.Windows.Forms.GroupBox();
            this.lblLogFolder = new System.Windows.Forms.Label();
            this.txbLogFolder = new System.Windows.Forms.TextBox();
            this.btnLogFolder = new System.Windows.Forms.Button();
            this.grbUserInterface.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tapGeneral.SuspendLayout();
            this.grbStartup.SuspendLayout();
            this.grbPreferences.SuspendLayout();
            this.tapAdvanced.SuspendLayout();
            this.grbLogFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbUserInterface
            // 
            resources.ApplyResources(this.grbUserInterface, "grbUserInterface");
            this.grbUserInterface.Controls.Add(this.cmbLanguage);
            this.grbUserInterface.Controls.Add(this.lblLanguage);
            this.grbUserInterface.Name = "grbUserInterface";
            this.grbUserInterface.TabStop = false;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLanguage, "cmbLanguage");
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Sorted = true;
            // 
            // lblLanguage
            // 
            resources.ApplyResources(this.lblLanguage, "lblLanguage");
            this.lblLanguage.Name = "lblLanguage";
            // 
            // lblSettingsDescription
            // 
            resources.ApplyResources(this.lblSettingsDescription, "lblSettingsDescription");
            this.lblSettingsDescription.Name = "lblSettingsDescription";
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tapGeneral);
            this.tabControl.Controls.Add(this.tapAdvanced);
            this.tabControl.Controls.Add(this.tapDNS);
            this.tabControl.Controls.Add(this.tapSchedule);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tapGeneral
            // 
            this.tapGeneral.Controls.Add(this.grbPreferences);
            this.tapGeneral.Controls.Add(this.grbStartup);
            this.tapGeneral.Controls.Add(this.grbUserInterface);
            resources.ApplyResources(this.tapGeneral, "tapGeneral");
            this.tapGeneral.Name = "tapGeneral";
            this.tapGeneral.UseVisualStyleBackColor = true;
            // 
            // tapDNS
            // 
            resources.ApplyResources(this.tapDNS, "tapDNS");
            this.tapDNS.Name = "tapDNS";
            this.tapDNS.UseVisualStyleBackColor = true;
            // 
            // tapSchedule
            // 
            resources.ApplyResources(this.tapSchedule, "tapSchedule");
            this.tapSchedule.Name = "tapSchedule";
            this.tapSchedule.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // grbStartup
            // 
            resources.ApplyResources(this.grbStartup, "grbStartup");
            this.grbStartup.Controls.Add(this.checkBox1);
            this.grbStartup.Name = "grbStartup";
            this.grbStartup.TabStop = false;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // grbPreferences
            // 
            resources.ApplyResources(this.grbPreferences, "grbPreferences");
            this.grbPreferences.Controls.Add(this.chbSilentConnection);
            this.grbPreferences.Name = "grbPreferences";
            this.grbPreferences.TabStop = false;
            // 
            // chbSilentConnection
            // 
            resources.ApplyResources(this.chbSilentConnection, "chbSilentConnection");
            this.chbSilentConnection.Name = "chbSilentConnection";
            this.chbSilentConnection.UseVisualStyleBackColor = true;
            // 
            // tapAdvanced
            // 
            this.tapAdvanced.Controls.Add(this.grbLogFiles);
            resources.ApplyResources(this.tapAdvanced, "tapAdvanced");
            this.tapAdvanced.Name = "tapAdvanced";
            this.tapAdvanced.UseVisualStyleBackColor = true;
            // 
            // grbLogFiles
            // 
            resources.ApplyResources(this.grbLogFiles, "grbLogFiles");
            this.grbLogFiles.Controls.Add(this.btnLogFolder);
            this.grbLogFiles.Controls.Add(this.txbLogFolder);
            this.grbLogFiles.Controls.Add(this.lblLogFolder);
            this.grbLogFiles.Name = "grbLogFiles";
            this.grbLogFiles.TabStop = false;
            // 
            // lblLogFolder
            // 
            resources.ApplyResources(this.lblLogFolder, "lblLogFolder");
            this.lblLogFolder.Name = "lblLogFolder";
            // 
            // txbLogFolder
            // 
            resources.ApplyResources(this.txbLogFolder, "txbLogFolder");
            this.txbLogFolder.Name = "txbLogFolder";
            // 
            // btnLogFolder
            // 
            resources.ApplyResources(this.btnLogFolder, "btnLogFolder");
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblSettingsDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.grbUserInterface.ResumeLayout(false);
            this.grbUserInterface.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tapGeneral.ResumeLayout(false);
            this.grbStartup.ResumeLayout(false);
            this.grbStartup.PerformLayout();
            this.grbPreferences.ResumeLayout(false);
            this.grbPreferences.PerformLayout();
            this.tapAdvanced.ResumeLayout(false);
            this.grbLogFiles.ResumeLayout(false);
            this.grbLogFiles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbUserInterface;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblSettingsDescription;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tapGeneral;
        private System.Windows.Forms.TabPage tapDNS;
        private System.Windows.Forms.TabPage tapSchedule;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox grbStartup;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox grbPreferences;
        private System.Windows.Forms.CheckBox chbSilentConnection;
        private System.Windows.Forms.TabPage tapAdvanced;
        private System.Windows.Forms.GroupBox grbLogFiles;
        private System.Windows.Forms.Button btnLogFolder;
        private System.Windows.Forms.TextBox txbLogFolder;
        private System.Windows.Forms.Label lblLogFolder;
    }
}