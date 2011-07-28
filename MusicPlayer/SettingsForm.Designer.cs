namespace Music
{
    partial class SettingsForm
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
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEnableScrobbling = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLastFm = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteFolder = new System.Windows.Forms.Button();
            this.listCurrentFolders = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnNewFolder = new System.Windows.Forms.Button();
            this.newFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.tabSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPage1);
            this.tabSettings.Controls.Add(this.tabPage2);
            this.tabSettings.Location = new System.Drawing.Point(0, 0);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(441, 371);
            this.tabSettings.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(433, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkEnableScrobbling);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblLastFm);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 78);
            this.panel1.TabIndex = 2;
            // 
            // chkEnableScrobbling
            // 
            this.chkEnableScrobbling.AutoSize = true;
            this.chkEnableScrobbling.Location = new System.Drawing.Point(10, 41);
            this.chkEnableScrobbling.Name = "chkEnableScrobbling";
            this.chkEnableScrobbling.Size = new System.Drawing.Size(112, 17);
            this.chkEnableScrobbling.TabIndex = 3;
            this.chkEnableScrobbling.Text = "Enable Scrobbling";
            this.chkEnableScrobbling.UseVisualStyleBackColor = true;
            this.chkEnableScrobbling.CheckedChanged += new System.EventHandler(this.chkEnableScrobbling_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            // 
            // lblLastFm
            // 
            this.lblLastFm.AutoSize = true;
            this.lblLastFm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastFm.Location = new System.Drawing.Point(7, 6);
            this.lblLastFm.Name = "lblLastFm";
            this.lblLastFm.Size = new System.Drawing.Size(129, 17);
            this.lblLastFm.TabIndex = 2;
            this.lblLastFm.Text = "Last.FM Scrobbling";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(275, 47);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(132, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(137, 47);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(132, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteFolder);
            this.tabPage2.Controls.Add(this.listCurrentFolders);
            this.tabPage2.Controls.Add(this.btnNewFolder);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(433, 345);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Library";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteFolder
            // 
            this.btnDeleteFolder.Location = new System.Drawing.Point(283, 35);
            this.btnDeleteFolder.Name = "btnDeleteFolder";
            this.btnDeleteFolder.Size = new System.Drawing.Size(142, 23);
            this.btnDeleteFolder.TabIndex = 1;
            this.btnDeleteFolder.Text = "Delete Folder";
            this.btnDeleteFolder.UseVisualStyleBackColor = true;
            this.btnDeleteFolder.Click += new System.EventHandler(this.btnDeleteFolder_Click);
            // 
            // listCurrentFolders
            // 
            this.listCurrentFolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listCurrentFolders.FullRowSelect = true;
            this.listCurrentFolders.GridLines = true;
            this.listCurrentFolders.Location = new System.Drawing.Point(8, 6);
            this.listCurrentFolders.MultiSelect = false;
            this.listCurrentFolders.Name = "listCurrentFolders";
            this.listCurrentFolders.ShowGroups = false;
            this.listCurrentFolders.Size = new System.Drawing.Size(269, 330);
            this.listCurrentFolders.TabIndex = 2;
            this.listCurrentFolders.UseCompatibleStateImageBehavior = false;
            this.listCurrentFolders.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Folder Path";
            this.columnHeader1.Width = 265;
            // 
            // btnNewFolder
            // 
            this.btnNewFolder.Location = new System.Drawing.Point(283, 6);
            this.btnNewFolder.Name = "btnNewFolder";
            this.btnNewFolder.Size = new System.Drawing.Size(142, 23);
            this.btnNewFolder.TabIndex = 1;
            this.btnNewFolder.Text = "Add New Folder";
            this.btnNewFolder.UseVisualStyleBackColor = true;
            this.btnNewFolder.Click += new System.EventHandler(this.btnNewFolder_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 370);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.tabSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnNewFolder;
        private System.Windows.Forms.ListView listCurrentFolders;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.FolderBrowserDialog newFolderBrowser;
        private System.Windows.Forms.Button btnDeleteFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkEnableScrobbling;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLastFm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
    }
}