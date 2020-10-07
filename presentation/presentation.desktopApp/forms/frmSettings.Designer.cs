namespace Presentation.DesktopApp.forms {
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
            this.grpUserInterface = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblSettingsDescription = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tapGeneral = new System.Windows.Forms.TabPage();
            this.grpPreferences = new System.Windows.Forms.GroupBox();
            this.chkSilentConnection = new System.Windows.Forms.CheckBox();
            this.grpStartup = new System.Windows.Forms.GroupBox();
            this.chkLaunchOnStartup = new System.Windows.Forms.CheckBox();
            this.tapAdvanced = new System.Windows.Forms.TabPage();
            this.grbLogFiles = new System.Windows.Forms.GroupBox();
            this.btnLogFolder = new System.Windows.Forms.Button();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.lblLogFolder = new System.Windows.Forms.Label();
            this.tapDNS = new System.Windows.Forms.TabPage();
            this.grbDnsList = new System.Windows.Forms.GroupBox();
            this.grdDNSAddress = new System.Windows.Forms.DataGridView();
            this.grbConfigureDNS = new System.Windows.Forms.GroupBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.tapSchedule = new System.Windows.Forms.TabPage();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.ipTextBox = new IPTextBox.IPTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.grpUserInterface.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tapGeneral.SuspendLayout();
            this.grpPreferences.SuspendLayout();
            this.grpStartup.SuspendLayout();
            this.tapAdvanced.SuspendLayout();
            this.grbLogFiles.SuspendLayout();
            this.tapDNS.SuspendLayout();
            this.grbDnsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDNSAddress)).BeginInit();
            this.grbConfigureDNS.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpUserInterface
            // 
            resources.ApplyResources(this.grpUserInterface, "grpUserInterface");
            this.grpUserInterface.Controls.Add(this.cmbLanguage);
            this.grpUserInterface.Controls.Add(this.lblLanguage);
            this.grpUserInterface.Name = "grpUserInterface";
            this.grpUserInterface.TabStop = false;
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
            this.tapGeneral.Controls.Add(this.grpPreferences);
            this.tapGeneral.Controls.Add(this.grpStartup);
            this.tapGeneral.Controls.Add(this.grpUserInterface);
            resources.ApplyResources(this.tapGeneral, "tapGeneral");
            this.tapGeneral.Name = "tapGeneral";
            this.tapGeneral.UseVisualStyleBackColor = true;
            // 
            // grpPreferences
            // 
            resources.ApplyResources(this.grpPreferences, "grpPreferences");
            this.grpPreferences.Controls.Add(this.chkSilentConnection);
            this.grpPreferences.Name = "grpPreferences";
            this.grpPreferences.TabStop = false;
            // 
            // chkSilentConnection
            // 
            resources.ApplyResources(this.chkSilentConnection, "chkSilentConnection");
            this.chkSilentConnection.Name = "chkSilentConnection";
            this.chkSilentConnection.UseVisualStyleBackColor = true;
            // 
            // grpStartup
            // 
            resources.ApplyResources(this.grpStartup, "grpStartup");
            this.grpStartup.Controls.Add(this.chkLaunchOnStartup);
            this.grpStartup.Name = "grpStartup";
            this.grpStartup.TabStop = false;
            // 
            // chkLaunchOnStartup
            // 
            resources.ApplyResources(this.chkLaunchOnStartup, "chkLaunchOnStartup");
            this.chkLaunchOnStartup.Name = "chkLaunchOnStartup";
            this.chkLaunchOnStartup.UseVisualStyleBackColor = true;
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
            this.grbLogFiles.Controls.Add(this.txtLogFolder);
            this.grbLogFiles.Controls.Add(this.lblLogFolder);
            this.grbLogFiles.Name = "grbLogFiles";
            this.grbLogFiles.TabStop = false;
            // 
            // btnLogFolder
            // 
            resources.ApplyResources(this.btnLogFolder, "btnLogFolder");
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.UseVisualStyleBackColor = true;
            // 
            // txtLogFolder
            // 
            resources.ApplyResources(this.txtLogFolder, "txtLogFolder");
            this.txtLogFolder.Name = "txtLogFolder";
            // 
            // lblLogFolder
            // 
            resources.ApplyResources(this.lblLogFolder, "lblLogFolder");
            this.lblLogFolder.Name = "lblLogFolder";
            // 
            // tapDNS
            // 
            this.tapDNS.Controls.Add(this.grbDnsList);
            this.tapDNS.Controls.Add(this.grbConfigureDNS);
            resources.ApplyResources(this.tapDNS, "tapDNS");
            this.tapDNS.Name = "tapDNS";
            this.tapDNS.UseVisualStyleBackColor = true;
            // 
            // grbDnsList
            // 
            this.grbDnsList.Controls.Add(this.grdDNSAddress);
            resources.ApplyResources(this.grbDnsList, "grbDnsList");
            this.grbDnsList.Name = "grbDnsList";
            this.grbDnsList.TabStop = false;
            // 
            // grdDNSAddress
            // 
            resources.ApplyResources(this.grdDNSAddress, "grdDNSAddress");
            this.grdDNSAddress.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdDNSAddress.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdDNSAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDNSAddress.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colIP,
            this.colTag});
            this.grdDNSAddress.Name = "grdDNSAddress";
            this.grdDNSAddress.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // grbConfigureDNS
            // 
            this.grbConfigureDNS.Controls.Add(this.cmbType);
            this.grbConfigureDNS.Controls.Add(this.btnSave);
            this.grbConfigureDNS.Controls.Add(this.ipTextBox);
            this.grbConfigureDNS.Controls.Add(this.lblIP);
            resources.ApplyResources(this.grbConfigureDNS, "grbConfigureDNS");
            this.grbConfigureDNS.Name = "grbConfigureDNS";
            this.grbConfigureDNS.TabStop = false;
            // 
            // lblIP
            // 
            resources.ApplyResources(this.lblIP, "lblIP");
            this.lblIP.Name = "lblIP";
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
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // ipTextBox
            // 
            this.ipTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ipTextBox, "ipTextBox");
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Value = "...";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colIP
            // 
            resources.ApplyResources(this.colIP, "colIP");
            this.colIP.Name = "colIP";
            this.colIP.ReadOnly = true;
            // 
            // colTag
            // 
            resources.ApplyResources(this.colTag, "colTag");
            this.colTag.Name = "colTag";
            this.colTag.ReadOnly = true;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            resources.GetString("cmbType.Items"),
            resources.GetString("cmbType.Items1")});
            resources.ApplyResources(this.cmbType, "cmbType");
            this.cmbType.Name = "cmbType";
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
            this.Activated += new System.EventHandler(this.frmSettings_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.grpUserInterface.ResumeLayout(false);
            this.grpUserInterface.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tapGeneral.ResumeLayout(false);
            this.grpPreferences.ResumeLayout(false);
            this.grpPreferences.PerformLayout();
            this.grpStartup.ResumeLayout(false);
            this.grpStartup.PerformLayout();
            this.tapAdvanced.ResumeLayout(false);
            this.grbLogFiles.ResumeLayout(false);
            this.grbLogFiles.PerformLayout();
            this.tapDNS.ResumeLayout(false);
            this.grbDnsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDNSAddress)).EndInit();
            this.grbConfigureDNS.ResumeLayout(false);
            this.grbConfigureDNS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUserInterface;
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
        private System.Windows.Forms.GroupBox grpStartup;
        private System.Windows.Forms.CheckBox chkLaunchOnStartup;
        private System.Windows.Forms.GroupBox grpPreferences;
        private System.Windows.Forms.CheckBox chkSilentConnection;
        private System.Windows.Forms.TabPage tapAdvanced;
        private System.Windows.Forms.GroupBox grbLogFiles;
        private System.Windows.Forms.Button btnLogFolder;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Label lblLogFolder;
        private System.Windows.Forms.GroupBox grbDnsList;
        private System.Windows.Forms.DataGridView grdDNSAddress;
        private System.Windows.Forms.GroupBox grbConfigureDNS;
        private System.Windows.Forms.Label lblIP;
        private IPTextBox.IPTextBox ipTextBox;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTag;
        private System.Windows.Forms.ComboBox cmbType;
    }
}