namespace Pogs.PogsMain
{
    partial class LockScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockScreen));
            this.closeButton = new System.Windows.Forms.Button();
            this.unlockButton = new System.Windows.Forms.Button();
            this.unlockInstructions = new System.Windows.Forms.Label();
            this.digit2TextBox = new System.Windows.Forms.TextBox();
            this.digit3TextBox = new System.Windows.Forms.TextBox();
            this.digit4TextBox = new System.Windows.Forms.TextBox();
            this.digit1TextBox = new System.Windows.Forms.TextBox();
            this.waitTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(196, 96);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 14;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // unlockButton
            // 
            this.unlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.unlockButton.Location = new System.Drawing.Point(115, 96);
            this.unlockButton.Name = "unlockButton";
            this.unlockButton.Size = new System.Drawing.Size(75, 23);
            this.unlockButton.TabIndex = 13;
            this.unlockButton.Text = "Unlock";
            this.unlockButton.UseVisualStyleBackColor = true;
            this.unlockButton.Click += new System.EventHandler(this.unlockButton_Click);
            // 
            // unlockInstructions
            // 
            this.unlockInstructions.AutoSize = true;
            this.unlockInstructions.Location = new System.Drawing.Point(6, 5);
            this.unlockInstructions.Name = "unlockInstructions";
            this.unlockInstructions.Size = new System.Drawing.Size(181, 13);
            this.unlockInstructions.TabIndex = 17;
            this.unlockInstructions.Text = "Enter your PIN to access \'{0}\' on {1}:";
            // 
            // digit2TextBox
            // 
            this.digit2TextBox.Font = new System.Drawing.Font("Courier New", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digit2TextBox.Location = new System.Drawing.Point(76, 26);
            this.digit2TextBox.MaxLength = 1;
            this.digit2TextBox.Multiline = true;
            this.digit2TextBox.Name = "digit2TextBox";
            this.digit2TextBox.PasswordChar = '●';
            this.digit2TextBox.Size = new System.Drawing.Size(61, 58);
            this.digit2TextBox.TabIndex = 10;
            this.digit2TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.digit2TextBox.WordWrap = false;
            this.digit2TextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.digit2TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.digit2TextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.digit2TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.digit2TextBox.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // digit3TextBox
            // 
            this.digit3TextBox.Font = new System.Drawing.Font("Courier New", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digit3TextBox.Location = new System.Drawing.Point(143, 26);
            this.digit3TextBox.MaxLength = 1;
            this.digit3TextBox.Multiline = true;
            this.digit3TextBox.Name = "digit3TextBox";
            this.digit3TextBox.PasswordChar = '●';
            this.digit3TextBox.Size = new System.Drawing.Size(61, 58);
            this.digit3TextBox.TabIndex = 11;
            this.digit3TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.digit3TextBox.WordWrap = false;
            this.digit3TextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.digit3TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.digit3TextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.digit3TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.digit3TextBox.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // digit4TextBox
            // 
            this.digit4TextBox.Font = new System.Drawing.Font("Courier New", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digit4TextBox.Location = new System.Drawing.Point(210, 26);
            this.digit4TextBox.MaxLength = 1;
            this.digit4TextBox.Multiline = true;
            this.digit4TextBox.Name = "digit4TextBox";
            this.digit4TextBox.PasswordChar = '●';
            this.digit4TextBox.Size = new System.Drawing.Size(61, 58);
            this.digit4TextBox.TabIndex = 12;
            this.digit4TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.digit4TextBox.WordWrap = false;
            this.digit4TextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.digit4TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.digit4TextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.digit4TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.digit4TextBox.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // digit1TextBox
            // 
            this.digit1TextBox.Font = new System.Drawing.Font("Courier New", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digit1TextBox.Location = new System.Drawing.Point(9, 26);
            this.digit1TextBox.MaxLength = 1;
            this.digit1TextBox.Multiline = true;
            this.digit1TextBox.Name = "digit1TextBox";
            this.digit1TextBox.PasswordChar = '●';
            this.digit1TextBox.Size = new System.Drawing.Size(61, 58);
            this.digit1TextBox.TabIndex = 9;
            this.digit1TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.digit1TextBox.WordWrap = false;
            this.digit1TextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.digit1TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.digit1TextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.digit1TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.digit1TextBox.Enter += new System.EventHandler(this.textBox_Enter);
            // 
            // waitTimer
            // 
            this.waitTimer.Interval = 3000;
            this.waitTimer.Tick += new System.EventHandler(this.waitTimer_Tick);
            // 
            // LockScreen
            // 
            this.AcceptButton = this.unlockButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 124);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.unlockButton);
            this.Controls.Add(this.unlockInstructions);
            this.Controls.Add(this.digit2TextBox);
            this.Controls.Add(this.digit3TextBox);
            this.Controls.Add(this.digit4TextBox);
            this.Controls.Add(this.digit1TextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LockScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pogs is Locked.";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button unlockButton;
        private System.Windows.Forms.Label unlockInstructions;
        private System.Windows.Forms.TextBox digit2TextBox;
        private System.Windows.Forms.TextBox digit3TextBox;
        private System.Windows.Forms.TextBox digit4TextBox;
        private System.Windows.Forms.TextBox digit1TextBox;
        private System.Windows.Forms.Timer waitTimer;
    }
}