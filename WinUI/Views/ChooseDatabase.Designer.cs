namespace Pogs.PogsMain
{
    partial class ChooseDatabase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.databaseComboBox = new System.Windows.Forms.ComboBox();
            this.checkServerLink = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(9, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 13);
            label1.TabIndex = 2;
            label1.Text = "Server Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 48);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(56, 13);
            label2.TabIndex = 3;
            label2.Text = "Database:";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(127, 81);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(208, 81);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(87, 17);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(143, 20);
            this.serverNameTextBox.TabIndex = 0;
            // 
            // databaseComboBox
            // 
            this.databaseComboBox.FormattingEnabled = true;
            this.databaseComboBox.Location = new System.Drawing.Point(87, 45);
            this.databaseComboBox.Name = "databaseComboBox";
            this.databaseComboBox.Size = new System.Drawing.Size(196, 21);
            this.databaseComboBox.TabIndex = 2;
            this.databaseComboBox.DropDown += new System.EventHandler(this.databaseComboBox_DropDown);
            this.databaseComboBox.TextChanged += new System.EventHandler(this.databaseComboBox_TextChanged);
            // 
            // checkServerLink
            // 
            this.checkServerLink.AutoSize = true;
            this.checkServerLink.Location = new System.Drawing.Point(236, 20);
            this.checkServerLink.Name = "checkServerLink";
            this.checkServerLink.Size = new System.Drawing.Size(47, 13);
            this.checkServerLink.TabIndex = 1;
            this.checkServerLink.TabStop = true;
            this.checkServerLink.Text = "Check...";
            this.checkServerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.checkServerLink_LinkClicked);
            // 
            // ChooseDatabase
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(295, 116);
            this.Controls.Add(this.checkServerLink);
            this.Controls.Add(this.databaseComboBox);
            this.Controls.Add(this.serverNameTextBox);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseDatabase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.ComboBox databaseComboBox;
        private System.Windows.Forms.LinkLabel checkServerLink;
    }
}