using System;
using System.Windows.Forms;
using NowPlayingForSpotify.Properties;

namespace NowPlayingForSpotify {
    public partial class FormatCustomizeForm : Form {
        public FormatCustomizeForm() {
            InitializeComponent();
        }

        Settings settings = new Settings();
        string defaultFormat = "%artist% - %track% #NowPlaying #Spotify %trackurl%";

        private void FormatCustomizeForm_Load(object sender, EventArgs e) {
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
            Close();
        }

        // Format
        private void button3_Click(object sender, EventArgs e) {
            var form = new Format();
            form.ShowDialog();
        }

        // Default
        private void button4_Click(object sender, EventArgs e) {
            var result = MessageBox.Show(Resources.ChangeToDefaultFormat, Information.Name, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) {
                textBox1.Text = defaultFormat;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            
        }

        private void FormatCustomizeForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (textBox1.Text != settings.TweetFormat) {
                var result = MessageBox.Show(Resources.ExitWithoutApply, Information.Name, MessageBoxButtons.YesNo);
                if (result == DialogResult.No) {
                    e.Cancel = true;
                }
            }
        }
    }
}
