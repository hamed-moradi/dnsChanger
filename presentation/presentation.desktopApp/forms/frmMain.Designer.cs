namespace Presentation.DesktopApp {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnTray = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.grbConnection = new System.Windows.Forms.GroupBox();
            this.picConnection = new System.Windows.Forms.PictureBox();
            this.lblNetworkConnection = new System.Windows.Forms.Label();
            this.cmbNetworkConnection = new System.Windows.Forms.ComboBox();
            this.grbDNS = new System.Windows.Forms.GroupBox();
            this.picLink = new System.Windows.Forms.PictureBox();
            this.iptxtAlternateDNS = new IPComboBox.IPComboBox();
            this.iptxtPreferredDNS = new IPComboBox.IPComboBox();
            this.btnAction = new System.Windows.Forms.Button();
            this.lblAlternateDNS = new System.Windows.Forms.Label();
            this.lblPreferredDNS = new System.Windows.Forms.Label();
            this.grbInfo = new System.Windows.Forms.GroupBox();
            this.lblCurrentAlternateIP = new System.Windows.Forms.Label();
            this.lblCurrentPreferredIP = new System.Windows.Forms.Label();
            this.lblCurrentAlternateDNS = new System.Windows.Forms.Label();
            this.lblCurrentPreferredDNS = new System.Windows.Forms.Label();
            this.grbConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnection)).BeginInit();
            this.grbDNS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLink)).BeginInit();
            this.grbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTray
            // 
            resources.ApplyResources(this.btnTray, "btnTray");
            this.btnTray.FlatAppearance.BorderSize = 0;
            this.btnTray.Name = "btnTray";
            this.btnTray.UseVisualStyleBackColor = true;
            this.btnTray.Click += new System.EventHandler(this.BtnTray_Click);
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // grbConnection
            // 
            resources.ApplyResources(this.grbConnection, "grbConnection");
            this.grbConnection.Controls.Add(this.picConnection);
            this.grbConnection.Controls.Add(this.lblNetworkConnection);
            this.grbConnection.Controls.Add(this.cmbNetworkConnection);
            this.grbConnection.Name = "grbConnection";
            this.grbConnection.TabStop = false;
            // 
            // picConnection
            // 
            resources.ApplyResources(this.picConnection, "picConnection");
            this.picConnection.Name = "picConnection";
            this.picConnection.TabStop = false;
            // 
            // lblNetworkConnection
            // 
            resources.ApplyResources(this.lblNetworkConnection, "lblNetworkConnection");
            this.lblNetworkConnection.Name = "lblNetworkConnection";
            // 
            // cmbNetworkConnection
            // 
            this.cmbNetworkConnection.BackColor = System.Drawing.Color.White;
            this.cmbNetworkConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetworkConnection.DropDownWidth = 183;
            resources.ApplyResources(this.cmbNetworkConnection, "cmbNetworkConnection");
            this.cmbNetworkConnection.FormattingEnabled = true;
            this.cmbNetworkConnection.Name = "cmbNetworkConnection";
            // 
            // grbDNS
            // 
            resources.ApplyResources(this.grbDNS, "grbDNS");
            this.grbDNS.Controls.Add(this.picLink);
            this.grbDNS.Controls.Add(this.iptxtAlternateDNS);
            this.grbDNS.Controls.Add(this.iptxtPreferredDNS);
            this.grbDNS.Controls.Add(this.btnAction);
            this.grbDNS.Controls.Add(this.lblAlternateDNS);
            this.grbDNS.Controls.Add(this.lblPreferredDNS);
            this.grbDNS.Name = "grbDNS";
            this.grbDNS.TabStop = false;
            // 
            // picLink
            // 
            resources.ApplyResources(this.picLink, "picLink");
            this.picLink.Name = "picLink";
            this.picLink.TabStop = false;
            this.picLink.Click += new System.EventHandler(this.picLink_Click);
            // 
            // iptxtAlternateDNS
            // 
            resources.ApplyResources(this.iptxtAlternateDNS, "iptxtAlternateDNS");
            this.iptxtAlternateDNS.Name = "iptxtAlternateDNS";
            this.iptxtAlternateDNS.Value = "...";
            // 
            // iptxtPreferredDNS
            // 
            resources.ApplyResources(this.iptxtPreferredDNS, "iptxtPreferredDNS");
            this.iptxtPreferredDNS.Name = "iptxtPreferredDNS";
            this.iptxtPreferredDNS.Value = "...";
            // 
            // btnAction
            // 
            resources.ApplyResources(this.btnAction, "btnAction");
            this.btnAction.FlatAppearance.BorderSize = 0;
            this.btnAction.Name = "btnAction";
            this.btnAction.Tag = "";
            this.btnAction.UseVisualStyleBackColor = true;
            // 
            // lblAlternateDNS
            // 
            resources.ApplyResources(this.lblAlternateDNS, "lblAlternateDNS");
            this.lblAlternateDNS.Name = "lblAlternateDNS";
            // 
            // lblPreferredDNS
            // 
            resources.ApplyResources(this.lblPreferredDNS, "lblPreferredDNS");
            this.lblPreferredDNS.Name = "lblPreferredDNS";
            // 
            // grbInfo
            // 
            resources.ApplyResources(this.grbInfo, "grbInfo");
            this.grbInfo.Controls.Add(this.lblCurrentAlternateIP);
            this.grbInfo.Controls.Add(this.lblCurrentPreferredIP);
            this.grbInfo.Controls.Add(this.lblCurrentAlternateDNS);
            this.grbInfo.Controls.Add(this.lblCurrentPreferredDNS);
            this.grbInfo.Name = "grbInfo";
            this.grbInfo.TabStop = false;
            // 
            // lblCurrentAlternateIP
            // 
            resources.ApplyResources(this.lblCurrentAlternateIP, "lblCurrentAlternateIP");
            this.lblCurrentAlternateIP.Name = "lblCurrentAlternateIP";
            // 
            // lblCurrentPreferredIP
            // 
            resources.ApplyResources(this.lblCurrentPreferredIP, "lblCurrentPreferredIP");
            this.lblCurrentPreferredIP.Name = "lblCurrentPreferredIP";
            // 
            // lblCurrentAlternateDNS
            // 
            resources.ApplyResources(this.lblCurrentAlternateDNS, "lblCurrentAlternateDNS");
            this.lblCurrentAlternateDNS.Name = "lblCurrentAlternateDNS";
            // 
            // lblCurrentPreferredDNS
            // 
            resources.ApplyResources(this.lblCurrentPreferredDNS, "lblCurrentPreferredDNS");
            this.lblCurrentPreferredDNS.Name = "lblCurrentPreferredDNS";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.grbInfo);
            this.Controls.Add(this.grbDNS);
            this.Controls.Add(this.grbConnection);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnTray);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.grbConnection.ResumeLayout(false);
            this.grbConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnection)).EndInit();
            this.grbDNS.ResumeLayout(false);
            this.grbDNS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLink)).EndInit();
            this.grbInfo.ResumeLayout(false);
            this.grbInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTray;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.GroupBox grbConnection;
        private System.Windows.Forms.Label lblNetworkConnection;
        private System.Windows.Forms.ComboBox cmbNetworkConnection;
        private System.Windows.Forms.GroupBox grbDNS;
        private System.Windows.Forms.Label lblAlternateDNS;
        private System.Windows.Forms.Label lblPreferredDNS;
        private System.Windows.Forms.Button btnAction;
        private IPComboBox.IPComboBox iptxtPreferredDNS;
        private IPComboBox.IPComboBox iptxtAlternateDNS;
        private System.Windows.Forms.PictureBox picConnection;
        private System.Windows.Forms.GroupBox grbInfo;
        private System.Windows.Forms.Label lblCurrentAlternateIP;
        private System.Windows.Forms.Label lblCurrentPreferredIP;
        private System.Windows.Forms.Label lblCurrentAlternateDNS;
        private System.Windows.Forms.Label lblCurrentPreferredDNS;
        private System.Windows.Forms.PictureBox picLink;
    }
}

