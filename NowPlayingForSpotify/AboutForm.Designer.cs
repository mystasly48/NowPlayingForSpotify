namespace NowPlayingForSpotify {
    partial class AboutForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.verLabelTitle = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.verLabel = new System.Windows.Forms.Label();
            this.developerLabel = new System.Windows.Forms.Label();
            this.developerLabelTitle = new System.Windows.Forms.Label();
            this.repoLinkLabel = new System.Windows.Forms.LinkLabel();
            this.repoLinkLabelTitle = new System.Windows.Forms.Label();
            this.twitterLinkLabelTitle = new System.Windows.Forms.Label();
            this.twitterLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // verLabelTitle
            // 
            resources.ApplyResources(this.verLabelTitle, "verLabelTitle");
            this.verLabelTitle.Name = "verLabelTitle";
            // 
            // nameLabel
            // 
            resources.ApplyResources(this.nameLabel, "nameLabel");
            this.nameLabel.Name = "nameLabel";
            // 
            // verLabel
            // 
            resources.ApplyResources(this.verLabel, "verLabel");
            this.verLabel.Name = "verLabel";
            // 
            // developerLabel
            // 
            resources.ApplyResources(this.developerLabel, "developerLabel");
            this.developerLabel.Name = "developerLabel";
            // 
            // developerLabelTitle
            // 
            resources.ApplyResources(this.developerLabelTitle, "developerLabelTitle");
            this.developerLabelTitle.Name = "developerLabelTitle";
            // 
            // repoLinkLabel
            // 
            resources.ApplyResources(this.repoLinkLabel, "repoLinkLabel");
            this.repoLinkLabel.Name = "repoLinkLabel";
            this.repoLinkLabel.TabStop = true;
            this.repoLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.repoLinkLabel_LinkClicked);
            // 
            // repoLinkLabelTitle
            // 
            resources.ApplyResources(this.repoLinkLabelTitle, "repoLinkLabelTitle");
            this.repoLinkLabelTitle.Name = "repoLinkLabelTitle";
            // 
            // twitterLinkLabelTitle
            // 
            resources.ApplyResources(this.twitterLinkLabelTitle, "twitterLinkLabelTitle");
            this.twitterLinkLabelTitle.Name = "twitterLinkLabelTitle";
            // 
            // twitterLinkLabel
            // 
            resources.ApplyResources(this.twitterLinkLabel, "twitterLinkLabel");
            this.twitterLinkLabel.Name = "twitterLinkLabel";
            this.twitterLinkLabel.TabStop = true;
            this.twitterLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.twitterLinkLabel_LinkClicked);
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.twitterLinkLabelTitle);
            this.Controls.Add(this.twitterLinkLabel);
            this.Controls.Add(this.repoLinkLabelTitle);
            this.Controls.Add(this.repoLinkLabel);
            this.Controls.Add(this.developerLabel);
            this.Controls.Add(this.developerLabelTitle);
            this.Controls.Add(this.verLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.verLabelTitle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label verLabelTitle;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label verLabel;
        private System.Windows.Forms.Label developerLabel;
        private System.Windows.Forms.Label developerLabelTitle;
        private System.Windows.Forms.LinkLabel repoLinkLabel;
        private System.Windows.Forms.Label repoLinkLabelTitle;
        private System.Windows.Forms.Label twitterLinkLabelTitle;
        private System.Windows.Forms.LinkLabel twitterLinkLabel;
    }
}