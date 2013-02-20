namespace Pogs.PogsMain
{
    partial class DatabaseSecurityDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.userImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAdminDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.typeImageDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAdminDataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.databaseSecurityDialogViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAdminDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.typeImageDataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.typeNameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAdminDataGridViewCheckBoxColumn4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.removeGroupButton = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.membersGroupBox = new System.Windows.Forms.GroupBox();
            this.membersDataGridView = new System.Windows.Forms.DataGridView();
            this.memberImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.typeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeImageDataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.typeNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAdminDataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.memberPrincipalViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.removeMemberButton = new System.Windows.Forms.Button();
            this.addMemberButton = new System.Windows.Forms.Button();
            this.addGroupButton = new System.Windows.Forms.Button();
            this.removeUserButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.resetPinButton = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.securityEditorView1 = new Pogs.Views.SecurityEditorView();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.commandManager1 = new Pogs.ViewModels.CommandManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseSecurityDialogViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsBindingSource)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.membersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.membersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memberPrincipalViewModelBindingSource)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.okButton, "Ok");
            this.okButton.Location = new System.Drawing.Point(226, 402);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // usersDataGridView
            // 
            this.usersDataGridView.AllowUserToAddRows = false;
            this.usersDataGridView.AllowUserToDeleteRows = false;
            this.usersDataGridView.AllowUserToResizeRows = false;
            this.usersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.usersDataGridView.AutoGenerateColumns = false;
            this.usersDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.usersDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userImageColumn,
            this.nameDataGridViewTextBoxColumn,
            this.isAdminDataGridViewCheckBoxColumn,
            this.typeImageDataGridViewImageColumn,
            this.typeNameDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn2,
            this.isAdminDataGridViewCheckBoxColumn2});
            this.usersDataGridView.DataSource = this.usersBindingSource;
            this.usersDataGridView.Location = new System.Drawing.Point(6, 6);
            this.usersDataGridView.MultiSelect = false;
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.RowHeadersVisible = false;
            this.usersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.usersDataGridView.Size = new System.Drawing.Size(269, 346);
            this.usersDataGridView.TabIndex = 5;
            this.usersDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.usersDataGridView_CurrentCellDirtyStateChanged);
            this.usersDataGridView.SelectionChanged += new System.EventHandler(this.usersDataGridView_SelectionChanged);
            // 
            // userImageColumn
            // 
            this.userImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.userImageColumn.DataPropertyName = "TypeImage";
            this.userImageColumn.HeaderText = "";
            this.userImageColumn.Name = "userImageColumn";
            this.userImageColumn.ReadOnly = true;
            this.userImageColumn.Width = 20;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "User";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // isAdminDataGridViewCheckBoxColumn
            // 
            this.isAdminDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isAdminDataGridViewCheckBoxColumn.DataPropertyName = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn.HeaderText = "Admin";
            this.isAdminDataGridViewCheckBoxColumn.Name = "isAdminDataGridViewCheckBoxColumn";
            this.isAdminDataGridViewCheckBoxColumn.ToolTipText = "When a user is an administrator, they will have complete access to all settings a" +
                "nd records in Pogs.";
            this.isAdminDataGridViewCheckBoxColumn.Width = 42;
            // 
            // typeImageDataGridViewImageColumn
            // 
            this.typeImageDataGridViewImageColumn.DataPropertyName = "TypeImage";
            this.typeImageDataGridViewImageColumn.HeaderText = "TypeImage";
            this.typeImageDataGridViewImageColumn.Name = "typeImageDataGridViewImageColumn";
            this.typeImageDataGridViewImageColumn.ReadOnly = true;
            this.typeImageDataGridViewImageColumn.Visible = false;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            this.nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn2.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            this.nameDataGridViewTextBoxColumn2.Visible = false;
            // 
            // isAdminDataGridViewCheckBoxColumn2
            // 
            this.isAdminDataGridViewCheckBoxColumn2.DataPropertyName = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn2.HeaderText = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn2.Name = "isAdminDataGridViewCheckBoxColumn2";
            this.isAdminDataGridViewCheckBoxColumn2.Visible = false;
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.databaseSecurityDialogViewModelBindingSource;
            // 
            // databaseSecurityDialogViewModelBindingSource
            // 
            this.databaseSecurityDialogViewModelBindingSource.DataSource = typeof(Pogs.ViewModels.DatabaseSecurityDialogViewModel);
            // 
            // groupsDataGridView
            // 
            this.groupsDataGridView.AllowUserToAddRows = false;
            this.groupsDataGridView.AllowUserToDeleteRows = false;
            this.groupsDataGridView.AllowUserToResizeRows = false;
            this.groupsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupsDataGridView.AutoGenerateColumns = false;
            this.groupsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.groupsDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.groupsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.groupImageColumn,
            this.nameDataGridViewTextBoxColumn1,
            this.isAdminDataGridViewCheckBoxColumn1,
            this.typeImageDataGridViewImageColumn2,
            this.typeNameDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn4,
            this.isAdminDataGridViewCheckBoxColumn4});
            this.groupsDataGridView.DataSource = this.groupsBindingSource;
            this.groupsDataGridView.Location = new System.Drawing.Point(6, 6);
            this.groupsDataGridView.Name = "groupsDataGridView";
            this.groupsDataGridView.RowHeadersVisible = false;
            this.groupsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.groupsDataGridView.Size = new System.Drawing.Size(269, 175);
            this.groupsDataGridView.TabIndex = 2;
            this.groupsDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.groupsDataGridView_CurrentCellDirtyStateChanged);
            this.groupsDataGridView.SelectionChanged += new System.EventHandler(this.groupsDataGridView_SelectionChanged);
            // 
            // groupImageColumn
            // 
            this.groupImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.groupImageColumn.DataPropertyName = "TypeImage";
            this.groupImageColumn.HeaderText = "";
            this.groupImageColumn.Name = "groupImageColumn";
            this.groupImageColumn.ReadOnly = true;
            this.groupImageColumn.Width = 20;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Group";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // isAdminDataGridViewCheckBoxColumn1
            // 
            this.isAdminDataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isAdminDataGridViewCheckBoxColumn1.DataPropertyName = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn1.HeaderText = "Admin";
            this.isAdminDataGridViewCheckBoxColumn1.Name = "isAdminDataGridViewCheckBoxColumn1";
            this.isAdminDataGridViewCheckBoxColumn1.Visible = false;
            // 
            // typeImageDataGridViewImageColumn2
            // 
            this.typeImageDataGridViewImageColumn2.DataPropertyName = "TypeImage";
            this.typeImageDataGridViewImageColumn2.HeaderText = "TypeImage";
            this.typeImageDataGridViewImageColumn2.Name = "typeImageDataGridViewImageColumn2";
            this.typeImageDataGridViewImageColumn2.ReadOnly = true;
            this.typeImageDataGridViewImageColumn2.Visible = false;
            // 
            // typeNameDataGridViewTextBoxColumn2
            // 
            this.typeNameDataGridViewTextBoxColumn2.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn2.HeaderText = "TypeName";
            this.typeNameDataGridViewTextBoxColumn2.Name = "typeNameDataGridViewTextBoxColumn2";
            this.typeNameDataGridViewTextBoxColumn2.ReadOnly = true;
            this.typeNameDataGridViewTextBoxColumn2.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn4
            // 
            this.nameDataGridViewTextBoxColumn4.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn4.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn4.Name = "nameDataGridViewTextBoxColumn4";
            this.nameDataGridViewTextBoxColumn4.Visible = false;
            // 
            // isAdminDataGridViewCheckBoxColumn4
            // 
            this.isAdminDataGridViewCheckBoxColumn4.DataPropertyName = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn4.HeaderText = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn4.Name = "isAdminDataGridViewCheckBoxColumn4";
            this.isAdminDataGridViewCheckBoxColumn4.Visible = false;
            // 
            // groupsBindingSource
            // 
            this.groupsBindingSource.DataMember = "Groups";
            this.groupsBindingSource.DataSource = this.databaseSecurityDialogViewModelBindingSource;
            // 
            // removeGroupButton
            // 
            this.removeGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.removeGroupButton, "RemoveSelectedGroup");
            this.removeGroupButton.Enabled = false;
            this.removeGroupButton.Location = new System.Drawing.Point(281, 35);
            this.removeGroupButton.Name = "removeGroupButton";
            this.removeGroupButton.Size = new System.Drawing.Size(75, 23);
            this.removeGroupButton.TabIndex = 1;
            this.removeGroupButton.Text = "Remove";
            this.removeGroupButton.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.membersGroupBox);
            this.tabPage1.Controls.Add(this.groupsDataGridView);
            this.tabPage1.Controls.Add(this.removeGroupButton);
            this.tabPage1.Controls.Add(this.addGroupButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 358);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Groups";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // membersGroupBox
            // 
            this.membersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.membersGroupBox.Controls.Add(this.membersDataGridView);
            this.membersGroupBox.Controls.Add(this.removeMemberButton);
            this.membersGroupBox.Controls.Add(this.addMemberButton);
            this.membersGroupBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.groupsBindingSource, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, "(no group selected)"));
            this.membersGroupBox.Location = new System.Drawing.Point(6, 187);
            this.membersGroupBox.Name = "membersGroupBox";
            this.membersGroupBox.Size = new System.Drawing.Size(350, 165);
            this.membersGroupBox.TabIndex = 3;
            this.membersGroupBox.TabStop = false;
            this.membersGroupBox.Text = "(no group selected)";
            // 
            // membersDataGridView
            // 
            this.membersDataGridView.AllowUserToAddRows = false;
            this.membersDataGridView.AllowUserToDeleteRows = false;
            this.membersDataGridView.AllowUserToResizeColumns = false;
            this.membersDataGridView.AllowUserToResizeRows = false;
            this.membersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.membersDataGridView.AutoGenerateColumns = false;
            this.membersDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.membersDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.membersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.membersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.memberImageColumn,
            this.typeNameColumn,
            this.typeImageDataGridViewImageColumn1,
            this.typeNameDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn3,
            this.isAdminDataGridViewCheckBoxColumn3});
            this.membersDataGridView.DataSource = this.memberPrincipalViewModelBindingSource;
            this.membersDataGridView.Location = new System.Drawing.Point(6, 19);
            this.membersDataGridView.Name = "membersDataGridView";
            this.membersDataGridView.ReadOnly = true;
            this.membersDataGridView.RowHeadersVisible = false;
            this.membersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.membersDataGridView.Size = new System.Drawing.Size(263, 140);
            this.membersDataGridView.TabIndex = 4;
            this.membersDataGridView.SelectionChanged += new System.EventHandler(this.membersDataGridView_SelectionChanged);
            // 
            // memberImageColumn
            // 
            this.memberImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.memberImageColumn.DataPropertyName = "TypeImage";
            this.memberImageColumn.HeaderText = "";
            this.memberImageColumn.Name = "memberImageColumn";
            this.memberImageColumn.ReadOnly = true;
            this.memberImageColumn.Width = 20;
            // 
            // typeNameColumn
            // 
            this.typeNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.typeNameColumn.DataPropertyName = "TypeName";
            this.typeNameColumn.HeaderText = "Type";
            this.typeNameColumn.Name = "typeNameColumn";
            this.typeNameColumn.ReadOnly = true;
            this.typeNameColumn.Visible = false;
            // 
            // typeImageDataGridViewImageColumn1
            // 
            this.typeImageDataGridViewImageColumn1.DataPropertyName = "TypeImage";
            this.typeImageDataGridViewImageColumn1.HeaderText = "TypeImage";
            this.typeImageDataGridViewImageColumn1.Name = "typeImageDataGridViewImageColumn1";
            this.typeImageDataGridViewImageColumn1.ReadOnly = true;
            this.typeImageDataGridViewImageColumn1.Visible = false;
            // 
            // typeNameDataGridViewTextBoxColumn1
            // 
            this.typeNameDataGridViewTextBoxColumn1.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn1.HeaderText = "TypeName";
            this.typeNameDataGridViewTextBoxColumn1.Name = "typeNameDataGridViewTextBoxColumn1";
            this.typeNameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.typeNameDataGridViewTextBoxColumn1.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn3
            // 
            this.nameDataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn3.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn3.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn3.Name = "nameDataGridViewTextBoxColumn3";
            this.nameDataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // isAdminDataGridViewCheckBoxColumn3
            // 
            this.isAdminDataGridViewCheckBoxColumn3.DataPropertyName = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn3.HeaderText = "IsAdmin";
            this.isAdminDataGridViewCheckBoxColumn3.Name = "isAdminDataGridViewCheckBoxColumn3";
            this.isAdminDataGridViewCheckBoxColumn3.ReadOnly = true;
            this.isAdminDataGridViewCheckBoxColumn3.Visible = false;
            // 
            // memberPrincipalViewModelBindingSource
            // 
            this.memberPrincipalViewModelBindingSource.DataMember = "MemberViewModels";
            this.memberPrincipalViewModelBindingSource.DataSource = this.groupsBindingSource;
            // 
            // removeMemberButton
            // 
            this.removeMemberButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.removeMemberButton, "RemoveSelectedGroupMember");
            this.removeMemberButton.Enabled = false;
            this.removeMemberButton.Location = new System.Drawing.Point(275, 48);
            this.removeMemberButton.Name = "removeMemberButton";
            this.removeMemberButton.Size = new System.Drawing.Size(75, 23);
            this.removeMemberButton.TabIndex = 3;
            this.removeMemberButton.Text = "Remove";
            this.removeMemberButton.UseVisualStyleBackColor = true;
            // 
            // addMemberButton
            // 
            this.addMemberButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.addMemberButton, "AddNewGroupMember");
            this.addMemberButton.Enabled = false;
            this.addMemberButton.Location = new System.Drawing.Point(275, 19);
            this.addMemberButton.Name = "addMemberButton";
            this.addMemberButton.Size = new System.Drawing.Size(75, 23);
            this.addMemberButton.TabIndex = 2;
            this.addMemberButton.Text = "Add";
            this.addMemberButton.UseVisualStyleBackColor = true;
            // 
            // addGroupButton
            // 
            this.addGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.addGroupButton, "AddNewGroup");
            this.addGroupButton.Location = new System.Drawing.Point(281, 6);
            this.addGroupButton.Name = "addGroupButton";
            this.addGroupButton.Size = new System.Drawing.Size(75, 23);
            this.addGroupButton.TabIndex = 0;
            this.addGroupButton.Text = "Add";
            this.addGroupButton.UseVisualStyleBackColor = true;
            // 
            // removeUserButton
            // 
            this.removeUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.removeUserButton, "RemoveSelectedUser");
            this.removeUserButton.Enabled = false;
            this.removeUserButton.Location = new System.Drawing.Point(281, 35);
            this.removeUserButton.Name = "removeUserButton";
            this.removeUserButton.Size = new System.Drawing.Size(75, 23);
            this.removeUserButton.TabIndex = 4;
            this.removeUserButton.Text = "Remove";
            this.removeUserButton.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(370, 384);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.resetPinButton);
            this.tabPage2.Controls.Add(this.usersDataGridView);
            this.tabPage2.Controls.Add(this.removeUserButton);
            this.tabPage2.Controls.Add(this.addUserButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 358);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Users";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // resetPinButton
            // 
            this.resetPinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.resetPinButton, "ChangeSelectedUserPin");
            this.resetPinButton.Enabled = false;
            this.resetPinButton.Location = new System.Drawing.Point(281, 83);
            this.resetPinButton.Name = "resetPinButton";
            this.resetPinButton.Size = new System.Drawing.Size(75, 23);
            this.resetPinButton.TabIndex = 6;
            this.resetPinButton.Text = "Reset PIN...";
            this.resetPinButton.UseVisualStyleBackColor = true;
            // 
            // addUserButton
            // 
            this.addUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManager1.SetCommandName(this.addUserButton, "AddNewUser");
            this.addUserButton.Location = new System.Drawing.Point(281, 6);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(75, 23);
            this.addUserButton.TabIndex = 3;
            this.addUserButton.Text = "Add";
            this.addUserButton.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.securityEditorView1);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(362, 358);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Default Security";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // securityEditorView1
            // 
            this.securityEditorView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.securityEditorView1.DataBindings.Add(new System.Windows.Forms.Binding("DataContext", this.databaseSecurityDialogViewModelBindingSource, "DefaultSecurity", true));
            this.securityEditorView1.Location = new System.Drawing.Point(6, 51);
            this.securityEditorView1.Name = "securityEditorView1";
            this.securityEditorView1.Size = new System.Drawing.Size(353, 287);
            this.securityEditorView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "These settings will be applied to all clients by default.  You can override these" +
                " settings by editing the Security of an individual client.";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(307, 402);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // commandManager1
            // 
            this.commandManager1.DataSource = this.databaseSecurityDialogViewModelBindingSource;
            // 
            // DatabaseSecurityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 437);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancelButton);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.databaseSecurityDialogViewModelBindingSource, "Title", true));
            this.MainBindingSource = this.databaseSecurityDialogViewModelBindingSource;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(410, 475);
            this.Name = "DatabaseSecurityDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Security";
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.databaseSecurityDialogViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsBindingSource)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.membersGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.membersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memberPrincipalViewModelBindingSource)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.DataGridView groupsDataGridView;
        private System.Windows.Forms.Button removeGroupButton;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button addGroupButton;
        private System.Windows.Forms.Button removeUserButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.BindingSource usersBindingSource;
        private System.Windows.Forms.BindingSource groupsBindingSource;
        private System.Windows.Forms.GroupBox membersGroupBox;
        private System.Windows.Forms.DataGridView membersDataGridView;
        private System.Windows.Forms.Button removeMemberButton;
        private System.Windows.Forms.Button addMemberButton;
        private System.Windows.Forms.BindingSource memberPrincipalViewModelBindingSource;
        private System.Windows.Forms.Button resetPinButton;
        private System.Windows.Forms.TabPage tabPage3;
        private Pogs.Views.SecurityEditorView securityEditorView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource databaseSecurityDialogViewModelBindingSource;
        private Pogs.ViewModels.CommandManager commandManager1;
        private System.Windows.Forms.DataGridViewImageColumn userImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAdminDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn typeImageDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAdminDataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn memberImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameColumn;
        private System.Windows.Forms.DataGridViewImageColumn typeImageDataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAdminDataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewImageColumn groupImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAdminDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewImageColumn typeImageDataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAdminDataGridViewCheckBoxColumn4;
    }
}