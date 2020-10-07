namespace IPComboBox {
    partial class IPComboBox {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cmbIPs = new System.Windows.Forms.ComboBox();
            this.ipTextBox = new IPTextBox.IPTextBox();
            this.SuspendLayout();
            // 
            // cmbIPs
            // 
            this.cmbIPs.BackColor = System.Drawing.Color.White;
            this.cmbIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIPs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbIPs.Location = new System.Drawing.Point(0, 0);
            this.cmbIPs.Name = "cmbIPs";
            this.cmbIPs.Size = new System.Drawing.Size(157, 21);
            this.cmbIPs.TabIndex = 4;
            this.cmbIPs.TabStop = false;
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(0, 0);
            this.ipTextBox.MaximumSize = new System.Drawing.Size(140, 21);
            this.ipTextBox.MinimumSize = new System.Drawing.Size(140, 21);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(140, 21);
            this.ipTextBox.TabIndex = 5;
            this.ipTextBox.Value = "...";
            // 
            // IPComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.cmbIPs);
            this.MaximumSize = new System.Drawing.Size(157, 21);
            this.MinimumSize = new System.Drawing.Size(157, 21);
            this.Name = "IPComboBox";
            this.Size = new System.Drawing.Size(157, 21);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox cmbIPs;
        private IPTextBox.IPTextBox ipTextBox;
    }
}
