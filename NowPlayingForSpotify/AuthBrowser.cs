using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NowPlayingForSpotify {
    public partial class AuthBrowser : Form {
        public AuthBrowser() {
            InitializeComponent();
            Success = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            if (webBrowser1.Url.OriginalString == "https://api.twitter.com/oauth/authorize" || webBrowser1.Url.OriginalString == "https://twitter.com/oauth/authorize") {
                var r = new Regex(@"<CODE>(\d+)</CODE>");
                var m = r.Match(webBrowser1.DocumentText);
                PIN = m.Result("${1}");
                Success = true;
                Close();
            }
        }

        public bool Success { get; private set; }

        public string PIN { get; private set; }

        public string URL { get; set; }

        private void AuthBrowser_Load(object sender, EventArgs e) {
            webBrowser1.Navigate(new Uri(URL));
        }

        private void AuthBrowser_FormClosing(object sender, FormClosingEventArgs e) {
            webBrowser1.Dispose();
        }
    }
}
