using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NowPlayingForSpotify {
    public partial class AboutForm : Form {
        public AboutForm() {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e) {
            ApplyInformation();
        }

        private void repoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(repoLinkLabel.Text);
        }

        private void twitterLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(twitterLinkLabel.Text);
        }

        private void ApplyInformation() {
            nameLabel.Text = Information.Name;
            verLabel.Text = Information.Version;
            developerLabel.Text = Information.Developer;
            repoLinkLabel.Text = Information.RepositoryUrl;
            twitterLinkLabel.Text = Information.DeveloperUrl;
        }
    }
}
