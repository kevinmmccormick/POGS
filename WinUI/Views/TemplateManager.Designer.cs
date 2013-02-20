namespace Pogs.PogsMain
{
    partial class TemplateManager
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
            CustomDispose(disposing);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateManager));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 2);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("", 3);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("", 4);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("", 5);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("", 6);
            this.templateImages = new System.Windows.Forms.ImageList(this.components);
            this.chooseIconListView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.templateName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.removeFieldButton = new System.Windows.Forms.Button();
            this.buttonImages = new System.Windows.Forms.ImageList(this.components);
            this.addFieldButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.allowCustomFieldsCheckBox = new System.Windows.Forms.CheckBox();
            this.templateFieldsDataGridView = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addTemplateButton = new System.Windows.Forms.Button();
            this.deleteTemplateButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.templateDataGridView = new System.Windows.Forms.DataGridView();
            this.imageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.templateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.fieldViewsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.templateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.templateViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.typeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.entryFieldTypeViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fieldDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imageDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entryFieldViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateFieldsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldViewsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFieldTypeViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFieldViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // templateImages
            // 
            this.templateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("templateImages.ImageStream")));
            this.templateImages.TransparentColor = System.Drawing.Color.Transparent;
            this.templateImages.Images.SetKeyName(0, "Calculator.png");
            this.templateImages.Images.SetKeyName(1, "cellphone.png");
            this.templateImages.Images.SetKeyName(2, "folder.png");
            this.templateImages.Images.SetKeyName(3, "Journal.png");
            this.templateImages.Images.SetKeyName(4, "Network.png");
            this.templateImages.Images.SetKeyName(5, "phone.png");
            this.templateImages.Images.SetKeyName(6, "website.png");
            // 
            // chooseIconListView
            // 
            this.chooseIconListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseIconListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseIconListView.HideSelection = false;
            this.chooseIconListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.chooseIconListView.LabelWrap = false;
            this.chooseIconListView.LargeImageList = this.templateImages;
            this.chooseIconListView.Location = new System.Drawing.Point(55, 70);
            this.chooseIconListView.Name = "chooseIconListView";
            this.chooseIconListView.ShowGroups = false;
            this.chooseIconListView.Size = new System.Drawing.Size(427, 49);
            this.chooseIconListView.SmallImageList = this.templateImages;
            this.chooseIconListView.TabIndex = 10;
            this.chooseIconListView.TileSize = new System.Drawing.Size(50, 40);
            this.chooseIconListView.UseCompatibleStateImageBehavior = false;
            this.chooseIconListView.View = System.Windows.Forms.View.Tile;
            this.chooseIconListView.SelectedIndexChanged += new System.EventHandler(this.chooseIconListView_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Icon:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Default Fields:";
            // 
            // templateName
            // 
            this.templateName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.templateName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.templateViewBindingSource, "Name", true));
            this.templateName.Location = new System.Drawing.Point(55, 30);
            this.templateName.Name = "templateName";
            this.templateName.Size = new System.Drawing.Size(427, 20);
            this.templateName.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.removeFieldButton);
            this.groupBox1.Controls.Add(this.addFieldButton);
            this.groupBox1.Controls.Add(this.chooseIconListView);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.templateName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.allowCustomFieldsCheckBox);
            this.groupBox1.Controls.Add(this.templateFieldsDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(187, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 336);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template Settings";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(-175, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(169, 340);
            this.dataGridView2.TabIndex = 17;
            // 
            // removeFieldButton
            // 
            this.removeFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeFieldButton.ImageKey = "RemoveItem.png";
            this.removeFieldButton.ImageList = this.buttonImages;
            this.removeFieldButton.Location = new System.Drawing.Point(454, 180);
            this.removeFieldButton.Name = "removeFieldButton";
            this.removeFieldButton.Size = new System.Drawing.Size(28, 23);
            this.removeFieldButton.TabIndex = 12;
            this.removeFieldButton.UseVisualStyleBackColor = true;
            this.removeFieldButton.Click += new System.EventHandler(this.removeFieldButton_Click);
            // 
            // buttonImages
            // 
            this.buttonImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonImages.ImageStream")));
            this.buttonImages.TransparentColor = System.Drawing.Color.White;
            this.buttonImages.Images.SetKeyName(0, "AddItem.png");
            this.buttonImages.Images.SetKeyName(1, "RemoveItem.png");
            // 
            // addFieldButton
            // 
            this.addFieldButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFieldButton.ImageKey = "AddItem.png";
            this.addFieldButton.ImageList = this.buttonImages;
            this.addFieldButton.Location = new System.Drawing.Point(454, 151);
            this.addFieldButton.Name = "addFieldButton";
            this.addFieldButton.Size = new System.Drawing.Size(28, 23);
            this.addFieldButton.TabIndex = 11;
            this.addFieldButton.UseVisualStyleBackColor = true;
            this.addFieldButton.Click += new System.EventHandler(this.addFieldButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name:";
            // 
            // allowCustomFieldsCheckBox
            // 
            this.allowCustomFieldsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.allowCustomFieldsCheckBox.AutoSize = true;
            this.allowCustomFieldsCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.templateViewBindingSource, "AllowCustomFields", true));
            this.allowCustomFieldsCheckBox.Location = new System.Drawing.Point(97, 304);
            this.allowCustomFieldsCheckBox.Name = "allowCustomFieldsCheckBox";
            this.allowCustomFieldsCheckBox.Size = new System.Drawing.Size(302, 17);
            this.allowCustomFieldsCheckBox.TabIndex = 5;
            this.allowCustomFieldsCheckBox.Text = "Allow additional custom fields to entries using this template.";
            this.allowCustomFieldsCheckBox.UseVisualStyleBackColor = true;
            // 
            // templateFieldsDataGridView
            // 
            this.templateFieldsDataGridView.AllowUserToAddRows = false;
            this.templateFieldsDataGridView.AllowUserToDeleteRows = false;
            this.templateFieldsDataGridView.AllowUserToResizeRows = false;
            this.templateFieldsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.templateFieldsDataGridView.AutoGenerateColumns = false;
            this.templateFieldsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.templateFieldsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.typeColumn,
            this.defaultValueDataGridViewTextBoxColumn,
            this.fieldDataGridViewTextBoxColumn});
            this.templateFieldsDataGridView.DataSource = this.fieldViewsBindingSource;
            this.templateFieldsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.templateFieldsDataGridView.Location = new System.Drawing.Point(14, 151);
            this.templateFieldsDataGridView.Name = "templateFieldsDataGridView";
            this.templateFieldsDataGridView.RowHeadersVisible = false;
            this.templateFieldsDataGridView.Size = new System.Drawing.Size(434, 144);
            this.templateFieldsDataGridView.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(609, 358);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // addTemplateButton
            // 
            this.addTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addTemplateButton.Location = new System.Drawing.Point(12, 325);
            this.addTemplateButton.Name = "addTemplateButton";
            this.addTemplateButton.Size = new System.Drawing.Size(81, 23);
            this.addTemplateButton.TabIndex = 14;
            this.addTemplateButton.Text = "Add";
            this.addTemplateButton.UseVisualStyleBackColor = true;
            this.addTemplateButton.Click += new System.EventHandler(this.addTemplateButton_Click);
            // 
            // deleteTemplateButton
            // 
            this.deleteTemplateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteTemplateButton.Location = new System.Drawing.Point(100, 325);
            this.deleteTemplateButton.Name = "deleteTemplateButton";
            this.deleteTemplateButton.Size = new System.Drawing.Size(81, 23);
            this.deleteTemplateButton.TabIndex = 15;
            this.deleteTemplateButton.Text = "Delete";
            this.deleteTemplateButton.UseVisualStyleBackColor = true;
            this.deleteTemplateButton.Click += new System.EventHandler(this.deleteTemplateButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(528, 358);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 12;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // templateDataGridView
            // 
            this.templateDataGridView.AllowUserToAddRows = false;
            this.templateDataGridView.AllowUserToResizeColumns = false;
            this.templateDataGridView.AllowUserToResizeRows = false;
            this.templateDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.templateDataGridView.AutoGenerateColumns = false;
            this.templateDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.templateDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.templateDataGridView.ColumnHeadersVisible = false;
            this.templateDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.imageDataGridViewImageColumn,
            this.nameDataGridViewTextBoxColumn1});
            this.templateDataGridView.DataSource = this.templateViewBindingSource;
            this.templateDataGridView.Location = new System.Drawing.Point(12, 12);
            this.templateDataGridView.MultiSelect = false;
            this.templateDataGridView.Name = "templateDataGridView";
            this.templateDataGridView.ReadOnly = true;
            this.templateDataGridView.RowHeadersVisible = false;
            this.templateDataGridView.RowTemplate.Height = 40;
            this.templateDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.templateDataGridView.Size = new System.Drawing.Size(169, 307);
            this.templateDataGridView.TabIndex = 16;
            // 
            // imageColumn
            // 
            this.imageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageColumn.DataPropertyName = "Image";
            this.imageColumn.HeaderText = "";
            this.imageColumn.Name = "imageColumn";
            this.imageColumn.ReadOnly = true;
            this.imageColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.imageColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.imageColumn.Width = 45;
            // 
            // templateColumn
            // 
            this.templateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.templateColumn.DataPropertyName = "Name";
            this.templateColumn.HeaderText = "Template";
            this.templateColumn.Name = "templateColumn";
            this.templateColumn.ReadOnly = true;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn1.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn1.HeaderText = "Type";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Width = 37;
            // 
            // fieldViewsBindingSource
            // 
            this.fieldViewsBindingSource.DataMember = "FieldViews";
            this.fieldViewsBindingSource.DataSource = this.templateViewBindingSource;
            // 
            // templateDataGridViewTextBoxColumn
            // 
            this.templateDataGridViewTextBoxColumn.DataPropertyName = "Template";
            this.templateDataGridViewTextBoxColumn.HeaderText = "Template";
            this.templateDataGridViewTextBoxColumn.Name = "templateDataGridViewTextBoxColumn";
            this.templateDataGridViewTextBoxColumn.ReadOnly = true;
            this.templateDataGridViewTextBoxColumn.Visible = false;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn2.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn2.HeaderText = "Type";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Width = 37;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.FillWeight = 40F;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // defaultValueDataGridViewTextBoxColumn
            // 
            this.defaultValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.defaultValueDataGridViewTextBoxColumn.DataPropertyName = "DefaultValue";
            this.defaultValueDataGridViewTextBoxColumn.FillWeight = 60F;
            this.defaultValueDataGridViewTextBoxColumn.HeaderText = "Default Value";
            this.defaultValueDataGridViewTextBoxColumn.Name = "defaultValueDataGridViewTextBoxColumn";
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn3.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn3.HeaderText = "Type";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Width = 37;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn4.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn4.HeaderText = "Type";
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Width = 37;
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn5.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn5.HeaderText = "Type";
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            this.dataGridViewComboBoxColumn5.Width = 37;
            // 
            // dataGridViewComboBoxColumn6
            // 
            this.dataGridViewComboBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn6.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn6.HeaderText = "Type";
            this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
            this.dataGridViewComboBoxColumn6.Width = 37;
            // 
            // dataGridViewComboBoxColumn7
            // 
            this.dataGridViewComboBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewComboBoxColumn7.DataPropertyName = "Type";
            this.dataGridViewComboBoxColumn7.HeaderText = "Type";
            this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
            this.dataGridViewComboBoxColumn7.Width = 37;
            // 
            // templateViewBindingSource
            // 
            this.templateViewBindingSource.DataSource = typeof(Pogs.VisualModel.TemplateView);
            this.templateViewBindingSource.CurrentChanged += new System.EventHandler(this.templateViewBindingSource_CurrentChanged);
            // 
            // typeColumn
            // 
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.typeColumn.DataPropertyName = "Type";
            this.typeColumn.DataSource = this.entryFieldTypeViewBindingSource;
            this.typeColumn.DisplayMember = "Label";
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ValueMember = "EntryFieldType";
            this.typeColumn.Width = 37;
            // 
            // entryFieldTypeViewBindingSource
            // 
            this.entryFieldTypeViewBindingSource.DataSource = typeof(Pogs.VisualModel.EntryFieldTypeView);
            // 
            // fieldDataGridViewTextBoxColumn
            // 
            this.fieldDataGridViewTextBoxColumn.DataPropertyName = "Field";
            this.fieldDataGridViewTextBoxColumn.HeaderText = "Field";
            this.fieldDataGridViewTextBoxColumn.Name = "fieldDataGridViewTextBoxColumn";
            this.fieldDataGridViewTextBoxColumn.ReadOnly = true;
            this.fieldDataGridViewTextBoxColumn.Visible = false;
            // 
            // imageDataGridViewImageColumn
            // 
            this.imageDataGridViewImageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.imageDataGridViewImageColumn.DataPropertyName = "Image";
            this.imageDataGridViewImageColumn.FillWeight = 50F;
            this.imageDataGridViewImageColumn.HeaderText = "Image";
            this.imageDataGridViewImageColumn.Name = "imageDataGridViewImageColumn";
            this.imageDataGridViewImageColumn.ReadOnly = true;
            this.imageDataGridViewImageColumn.Width = 45;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // entryFieldViewBindingSource
            // 
            this.entryFieldViewBindingSource.DataSource = typeof(Pogs.VisualModel.EntryFieldView);
            // 
            // TemplateManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(696, 393);
            this.Controls.Add(this.deleteTemplateButton);
            this.Controls.Add(this.addTemplateButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.templateDataGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(712, 431);
            this.Name = "TemplateManager";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Templates";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateFieldsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fieldViewsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFieldTypeViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.entryFieldViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removeFieldButton;
        private System.Windows.Forms.ImageList templateImages;
        private System.Windows.Forms.Button addFieldButton;
        private System.Windows.Forms.ListView chooseIconListView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox templateName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox allowCustomFieldsCheckBox;
        private System.Windows.Forms.DataGridView templateFieldsDataGridView;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addTemplateButton;
        private System.Windows.Forms.Button deleteTemplateButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ImageList buttonImages;
        private System.Windows.Forms.DataGridView templateDataGridView;
        private System.Windows.Forms.BindingSource templateViewBindingSource;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewImageColumn imageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn templateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn templateDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource entryFieldViewBindingSource;
        private System.Windows.Forms.BindingSource entryFieldTypeViewBindingSource;
        private System.Windows.Forms.BindingSource fieldViewsBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private System.Windows.Forms.DataGridViewImageColumn imageDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
    }
}