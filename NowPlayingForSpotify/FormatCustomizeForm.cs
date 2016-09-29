using System;
using System.Windows.Forms;
using NowPlayingForSpotify.Properties;

namespace NowPlayingForSpotify {
    public partial class FormatCustomize : Form {
        public FormatCustomize() {
            InitializeComponent();
        }

        Settings settings = new Settings();
        String defaultFormat = "%artist% - %track% #NowPlaying #Spotify %trackurl%";
        String AppName = "NowPlaying for Spotify";

        private void FormatCustomize_Load(object sender, EventArgs e) {
            textBox1.Text = settings.TweetFormat;
        }

        // Apply
        private void button1_Click(object sender, EventArgs e) {
            button1.Enabled = false;
            settings.TweetFormat = textBox1.Text;
            settings.Save();
            button1.Enabled = true;
        }

        // Cancel
        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        // Format
        private void button3_Click(object sender, EventArgs e) {
            Format form = new Format();
            form.ShowDialog();
        }

        // Default
        private void button4_Click(object sender, EventArgs e) {
            var result = MessageBox.Show("Are you sure you want to change back to the default format?", AppName, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                textBox1.Text = defaultFormat;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            
        }

        private void FormatCustomize_FormClosing(object sender, FormClosingEventArgs e) {
            if (textBox1.Text != settings.TweetFormat) {
                var result = MessageBox.Show("Are you sure you want to exit without applying your changes?", AppName, MessageBoxButtons.YesNo);
                if (result == DialogResult.No) {
                    e.Cancel = true;
                }
            }
        }
    }
}
