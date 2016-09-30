using System;
using System.Windows.Forms;

namespace NowPlayingForSpotify {
    public partial class FormatsForm : Form {
        public FormatsForm() {
            InitializeComponent();
        }

        private void FormatsForm_Load(object sender, EventArgs e) {
            ActiveControl = label1;
        }

        private void textBox1_Click(object sender, EventArgs e) {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e) {
            textBox2.SelectAll();
        }

        private void textBox3_Click(object sender, EventArgs e) {
            textBox3.SelectAll();
        }

        private void textBox4_Click(object sender, EventArgs e) {
            textBox4.SelectAll();
        }

        private void textBox5_Click(object sender, EventArgs e) {
            textBox5.SelectAll();
        }

        private void textBox6_Click(object sender, EventArgs e) {
            textBox6.SelectAll();
        }

        private void textBox7_Click(object sender, EventArgs e) {
            textBox7.SelectAll();
        }

        private void textBox8_Click(object sender, EventArgs e) {
            textBox8.SelectAll();
        }
    }
}
