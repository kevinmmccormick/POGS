namespace Pogs.PogsMain
{
    partial class ClientSecurityDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientSecurityDialog));
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.clientSecurityDialogViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.securityEditorView1 = new Pogs.Views.SecurityEditorView();
            this.commandManager1 = new Pogs.ViewModels.CommandManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.clientSecurityDialogViewModelBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.saveButton, "SaveCommand");
            this.saveButton.Location = new System.Drawing.Point(154, 330);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(235, 330);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.clientSecurityDialogViewModelBindingSource, "UseDefaultSecurity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(179, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Use the default security settings.";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // clientSecurityDialogViewModelBindingSource
            // 
            this.clientSecurityDialogViewModelBindingSource.DataSource = typeof(Pogs.ViewModels.ClientSecurityDialogViewModel);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.securityEditorView1);
            this.groupBox1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.clientSecurityDialogViewModelBindingSource, "DescriptorsEditable", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(298, 287);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom Security Settings";
            // 
            // securityEditorView1
            // 
            this.securityEditorView1.DataBindings.Add(new System.Windows.Forms.Binding("DataContext", this.clientSecurityDialogViewModelBindingSource, "Descriptors", true));
            this.securityEditorView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.securityEditorView1.Location = new System.Drawing.Point(8, 21);
            this.securityEditorView1.Name = "securityEditorView1";
            this.securityEditorView1.Size = new System.Drawing.Size(282, 258);
            this.securityEditorView1.TabIndex = 0;
            // 
            // commandManager1
            // 
            this.commandManager1.DataSource = this.clientSecurityDialogViewModelBindingSource;
            // 
            // ClientSecurityDialog
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(322, 365);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.clientSecurityDialogViewModelBindingSource, "Title", true));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainBindingSource = this.clientSecurityDialogViewModelBindingSource;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClientSecurityDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security";
            ((System.ComponentModel.ISupportInitialize)(this.clientSecurityDialogViewModelBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private Pogs.Views.SecurityEditorView securityEditorView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource clientSecurityDialogViewModelBindingSource;
        private Pogs.ViewModels.CommandManager commandManager1;
    }
}