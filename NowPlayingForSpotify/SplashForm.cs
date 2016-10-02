using System.Windows.Forms;
using NowPlayingForSpotify.Properties;

namespace NowPlayingForSpotify {
    public partial class SplashForm : Form {
        public SplashForm() {
            InitializeComponent();
        }

        private void SplashForm_Shown(object sender, System.EventArgs e) {
            if (Settings.Default.Hide)
                Hide();
        }
    }
}
