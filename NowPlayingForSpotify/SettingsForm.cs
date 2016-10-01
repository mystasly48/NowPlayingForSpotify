using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NowPlayingForSpotify.Properties;
using System.Linq;

namespace NowPlayingForSpotify {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
        }

        List<string> before = new List<string>();

        // Customize Tweet Format
        private void button1_Click(object sender, EventArgs e) {
            var formatCustomizeForm = new FormatCustomizeForm();
            formatCustomizeForm.ShowDialog();
        }

        // Create "NowPlaying.txt" File
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.CreateText = checkBox1.Checked;
        }

        // Always stay on top
        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.TopMost = checkBox2.Checked;
        }

        // Automatically start with Windows
        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            
        }
        
        // Automatically show in notification area
        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.Visible = checkBox4.Checked;
        }

        // Apply
        private void button2_Click(object sender, EventArgs e) {
            Settings.Default.Save();
        }

        // Cancel
        private void button3_Click(object sender, EventArgs e) {
            var after = GetCurrentSettingValues();
            if (!after.SequenceEqual(before)) {
                var result = MessageBox.Show(Resources.ExitWithoutApply, Information.Name, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) {
                    Close();
                }
            }
        }

        // Save the before values
        private void SettingsForm_Load(object sender, EventArgs e) {
            before = GetCurrentSettingValues();
        }

        private List<string> GetCurrentSettingValues() {
            var list = new List<string>();
            list.Add(checkBox1.Checked.ToString());
            list.Add(checkBox2.Checked.ToString());
            list.Add(checkBox4.Checked.ToString());
            return list;
        }
    }
}
