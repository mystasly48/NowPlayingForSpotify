namespace NowPlayingForSpotify {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.albumPicture = new System.Windows.Forms.PictureBox();
            this.trackLabel = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.albumLabel = new System.Windows.Forms.Label();
            this.trackLink = new System.Windows.Forms.LinkLabel();
            this.artistLink = new System.Windows.Forms.LinkLabel();
            this.albumLink = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.settingsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.albumPicture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // albumPicture
            // 
            this.albumPicture.Location = new System.Drawing.Point(12, 13);
            this.albumPicture.Name = "albumPicture";
            this.albumPicture.Size = new System.Drawing.Size(100, 100);
            this.albumPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumPicture.TabIndex = 1;
            this.albumPicture.TabStop = false;
            // 
            // trackLabel
            // 
            this.trackLabel.AutoSize = true;
            this.trackLabel.Location = new System.Drawing.Point(119, 13);
            this.trackLabel.Name = "trackLabel";
            this.trackLabel.Size = new System.Drawing.Size(36, 12);
            this.trackLabel.TabIndex = 2;
            this.trackLabel.Text = "Track:";
            // 
            // artistLabel
            // 
            this.artistLabel.AutoSize = true;
            this.artistLabel.Location = new System.Drawing.Point(118, 36);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(40, 12);
            this.artistLabel.TabIndex = 3;
            this.artistLabel.Text = "Artist: ";
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Location = new System.Drawing.Point(118, 59);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(43, 12);
            this.albumLabel.TabIndex = 4;
            this.albumLabel.Text = "Album: ";
            // 
            // trackLink
            // 
            this.trackLink.AutoSize = true;
            this.trackLink.Location = new System.Drawing.Point(162, 13);
            this.trackLink.Name = "trackLink";
            this.trackLink.Size = new System.Drawing.Size(0, 12);
            this.trackLink.TabIndex = 6;
            this.trackLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.titleLink_LinkClicked);
            // 
            // artistLink
            // 
            this.artistLink.AutoSize = true;
            this.artistLink.Location = new System.Drawing.Point(162, 36);
            this.artistLink.Name = "artistLink";
            this.artistLink.Size = new System.Drawing.Size(0, 12);
            this.artistLink.TabIndex = 7;
            this.artistLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.artistLink_LinkClicked);
            // 
            // albumLink
            // 
            this.albumLink.AutoSize = true;
            this.albumLink.Location = new System.Drawing.Point(162, 59);
            this.albumLink.Name = "albumLink";
            this.albumLink.Size = new System.Drawing.Size(0, 12);
            this.albumLink.TabIndex = 8;
            this.albumLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.albumLink_LinkClicked);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "NowPlaying for Spotify";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // hideBtn
            // 
            this.hideBtn.Location = new System.Drawing.Point(316, 90);
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.Size = new System.Drawing.Size(42, 23);
            this.hideBtn.TabIndex = 9;
            this.hideBtn.Text = "Hide";
            this.hideBtn.UseVisualStyleBackColor = true;
            this.hideBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(121, 96);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(70, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Text File";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Location = new System.Drawing.Point(249, 90);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(61, 23);
            this.settingsBtn.TabIndex = 11;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 123);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.hideBtn);
            this.Controls.Add(this.albumLink);
            this.Controls.Add(this.artistLink);
            this.Controls.Add(this.trackLink);
            this.Controls.Add(this.albumLabel);
            this.Controls.Add(this.artistLabel);
            this.Controls.Add(this.trackLabel);
            this.Controls.Add(this.albumPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "NowPlaying for Spotify";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.albumPicture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox albumPicture;
        private System.Windows.Forms.Label trackLabel;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.LinkLabel trackLink;
        private System.Windows.Forms.LinkLabel artistLink;
        private System.Windows.Forms.LinkLabel albumLink;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button hideBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button settingsBtn;
    }
}

