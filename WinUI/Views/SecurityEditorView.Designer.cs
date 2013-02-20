namespace Pogs.Views
{
    partial class SecurityEditorView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.removePrincipalButton = new System.Windows.Forms.Button();
            this.addPrincipalButton = new System.Windows.Forms.Button();
            this.principalsDataGridView = new System.Windows.Forms.DataGridView();
            this.iconColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isViewerDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsEditor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.iconDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isEditorDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.descriptorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.securityEditorViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.commandManager1 = new Pogs.ViewModels.CommandManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.principalsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityEditorViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // removePrincipalButton
            // 
            this.removePrincipalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.removePrincipalButton, "RemoveSelectedDescriptor");
            this.removePrincipalButton.Location = new System.Drawing.Point(223, 261);
            this.removePrincipalButton.Name = "removePrincipalButton";
            this.removePrincipalButton.Size = new System.Drawing.Size(75, 23);
            this.removePrincipalButton.TabIndex = 13;
            this.removePrincipalButton.Text = "Remove";
            this.removePrincipalButton.UseVisualStyleBackColor = true;
            // 
            // addPrincipalButton
            // 
            this.addPrincipalButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.addPrincipalButton, "AddDescriptor");
            this.addPrincipalButton.Location = new System.Drawing.Point(142, 261);
            this.addPrincipalButton.Name = "addPrincipalButton";
            this.addPrincipalButton.Size = new System.Drawing.Size(75, 23);
            this.addPrincipalButton.TabIndex = 12;
            this.addPrincipalButton.Text = "Add...";
            this.addPrincipalButton.UseVisualStyleBackColor = true;
            // 
            // principalsDataGridView
            // 
            this.principalsDataGridView.AllowUserToAddRows = false;
            this.principalsDataGridView.AllowUserToDeleteRows = false;
            this.principalsDataGridView.AllowUserToResizeColumns = false;
            this.principalsDataGridView.AllowUserToResizeRows = false;
            this.principalsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.principalsDataGridView.AutoGenerateColumns = false;
            this.principalsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.principalsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.principalsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.principalsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.principalsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iconColumn,
            this.nameColumn,
            this.isViewerDataGridViewCheckBoxColumn,
            this.IsEditor,
            this.iconDataGridViewImageColumn,
            this.dataGridViewTextBoxColumn1,
            this.isEditorDataGridViewCheckBoxColumn});
            this.principalsDataGridView.DataSource = this.descriptorsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.principalsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.principalsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.principalsDataGridView.MultiSelect = false;
            this.principalsDataGridView.Name = "principalsDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.principalsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.principalsDataGridView.RowHeadersVisible = false;
            this.principalsDataGridView.RowTemplate.Height = 21;
            this.principalsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.principalsDataGridView.Size = new System.Drawing.Size(298, 255);
            this.principalsDataGridView.TabIndex = 9;
            this.principalsDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.principalsDataGridView_CurrentCellDirtyStateChanged);
            this.principalsDataGridView.SelectionChanged += new System.EventHandler(this.principalsDataGridView_SelectionChanged);
            // 
            // iconColumn
            // 
            this.iconColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.iconColumn.DataPropertyName = "Icon";
            this.iconColumn.HeaderText = "";
            this.iconColumn.Name = "iconColumn";
            this.iconColumn.ReadOnly = true;
            this.iconColumn.Width = 20;
            // 
            // nameColumn
            // 
            this.nameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // isViewerDataGridViewCheckBoxColumn
            // 
            this.isViewerDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.isViewerDataGridViewCheckBoxColumn.DataPropertyName = "IsViewer";
            this.isViewerDataGridViewCheckBoxColumn.HeaderText = "View";
            this.isViewerDataGridViewCheckBoxColumn.Name = "isViewerDataGridViewCheckBoxColumn";
            this.isViewerDataGridViewCheckBoxColumn.Width = 50;
            // 
            // IsEditor
            // 
            this.IsEditor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsEditor.DataPropertyName = "IsEditor";
            this.IsEditor.HeaderText = "Edit";
            this.IsEditor.Name = "IsEditor";
            this.IsEditor.Width = 50;
            // 
            // iconDataGridViewImageColumn
            // 
            this.iconDataGridViewImageColumn.DataPropertyName = "Icon";
            this.iconDataGridViewImageColumn.HeaderText = "Icon";
            this.iconDataGridViewImageColumn.Name = "iconDataGridViewImageColumn";
            this.iconDataGridViewImageColumn.ReadOnly = true;
            this.iconDataGridViewImageColumn.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // isEditorDataGridViewCheckBoxColumn
            // 
            this.isEditorDataGridViewCheckBoxColumn.DataPropertyName = "IsEditor";
            this.isEditorDataGridViewCheckBoxColumn.HeaderText = "IsEditor";
            this.isEditorDataGridViewCheckBoxColumn.Name = "isEditorDataGridViewCheckBoxColumn";
            this.isEditorDataGridViewCheckBoxColumn.Visible = false;
            // 
            // descriptorsBindingSource
            // 
            this.descriptorsBindingSource.DataMember = "Descriptors";
            this.descriptorsBindingSource.DataSource = this.securityEditorViewModelBindingSource;
            // 
            // securityEditorViewModelBindingSource
            // 
            this.securityEditorViewModelBindingSource.DataSource = typeof(Pogs.ViewModels.SecurityEditorViewModel);
            // 
            // commandManager1
            // 
            this.commandManager1.DataSource = this.securityEditorViewModelBindingSource;
            // 
            // SecurityEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.removePrincipalButton);
            this.Controls.Add(this.addPrincipalButton);
            this.Controls.Add(this.principalsDataGridView);
            this.MainBindingSource = this.securityEditorViewModelBindingSource;
            this.Name = "SecurityEditorView";
            this.Size = new System.Drawing.Size(298, 284);
            this.EnabledChanged += new System.EventHandler(this.SecurityEditorView_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.principalsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.securityEditorViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removePrincipalButton;
        private System.Windows.Forms.Button addPrincipalButton;
        private System.Windows.Forms.DataGridView principalsDataGridView;
        private Pogs.ViewModels.CommandManager commandManager1;
        private System.Windows.Forms.BindingSource securityEditorViewModelBindingSource;
        private System.Windows.Forms.BindingSource descriptorsBindingSource;
        private System.Windows.Forms.DataGridViewImageColumn iconColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isViewerDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsEditor;
        private System.Windows.Forms.DataGridViewImageColumn iconDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isEditorDataGridViewCheckBoxColumn;
    }
}
