using System;
using System.Windows.Forms;

namespace NowPlayingForSpotify {
    public partial class Format : Form {
        public Format() {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e) {
            textBox1.SelectAll();
        }
    }
}
