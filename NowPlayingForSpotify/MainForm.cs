using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using CoreTweet;
using NowPlayingForSpotify.Properties;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;

namespace NowPlayingForSpotify {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        #region Reference

        public static string AppName = "NowPlaying for Spotify";
        Settings settings = new Settings();
        string Spotify = "Spotify";
        Tokens twitter;
        SpotifyLocalAPI spotify = new SpotifyLocalAPI();
        Track currentTrack;
        bool IsPlaying = false;
        String LastTweet = "";
        Boolean CreateTextFile = false;
        DateTime StartTime;
        SplashForm splash = new SplashForm();

        #endregion

        #region Other

        private void ShowSplash() {
            splash.Show();
            splash.Refresh();
        }

        private void CloseSplash() {
            splash.Close();
            splash.Dispose();
            this.Activate();
        }

        private void OpenLink(LinkLabel label) {
            Process.Start(label.Tag.ToString());
        }

        private void init() {
            SettingsInit();
            TwitterInit();
            SpotifyInit();
        }

        private void SettingsInit() {
            LastTweet = settings.LastTweet;
            CreateTextFile = settings.CreateTextFile;

            this.Location = settings.StartPosition;
            checkBox1.Checked = CreateTextFile;
        }

        private void SaveSettings() {
            settings.LastTweet = LastTweet;
            //settings.CreateTextFile = CreateTextFile;
            settings.StartPosition = this.Location;
            settings.Save();
        }

        private void EnvironmentExit() {
            Environment.Exit(0);
        }

        private void Exit() {
            Application.Exit();
        }

        private void Message(string msg) {
            MessageBox.Show(msg, AppName);
        }

        private void Message(string msg, string title) {
            MessageBox.Show(msg, title);
        }

        private void HideNotifyIcon() {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void ShowNotifyIcon() {
            this.Hide();
            notifyIcon1.Visible = true;
        }

        private void CreateNowPlayingFile() {
            if (CreateTextFile) {
                var path = "NowPlaying.txt";
                var space = "     ";
                var str = artistLink.Text + " - " + trackLink.Text + space;
                using (var sr = new StreamWriter(path, false, Encoding.UTF8)) {
                    sr.Write(str);
                }
            }
        }

        #endregion

        #region Form

        private void MainForm_Load(object sender, EventArgs e) {
            ShowSplash();
            init();
            CloseSplash();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            SendTweet();
            timer1.Stop();
        }

        private void titleLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenLink(trackLink);
        }

        private void artistLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenLink(artistLink);
        }

        private void albumLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenLink(albumLink);
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) {
            HideNotifyIcon();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Exit();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            HideNotifyIcon();
        }

        private void button1_Click(object sender, EventArgs e) {
            ShowNotifyIcon();
        }

        private void settingsBtn_Click(object sender, EventArgs e) {
            FormatCustomize form = new FormatCustomize();
            form.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            SaveSettings();
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            CreateTextFile = checkBox1.Checked;
            CreateNowPlayingFile();
        }

        #endregion

        #region Twitter

        private void TwitterInit() {
            if (String.IsNullOrEmpty(settings.AccessToken) || String.IsNullOrEmpty(settings.AccessSecret)) {
                var session = OAuth.Authorize(Keys.ConsumerKey, Keys.ConsumerSecret);
                Process.Start(session.AuthorizeUri.AbsoluteUri);
                InputBox input = new InputBox();
                while (true) {
                    input.ShowDialog();
                    var pin = input.Pin;
                    if (pin.Equals("exit")) {
                        Environment.Exit(0);
                    }
                    try {
                        twitter = session.GetTokens(pin);
                        break;
                    } catch (TwitterException ex) {
                        if (ex.Message.Equals("Error processing your OAuth request: Invalid oauth_verifier parameter")) {
                            continue;
                        }
                        throw;
                    }
                }
                settings.AccessToken = twitter.AccessToken;
                settings.AccessSecret = twitter.AccessTokenSecret;
                settings.Save();
            } else {
                twitter = Tokens.Create(Keys.ConsumerKey, Keys.ConsumerSecret, settings.AccessToken, settings.AccessSecret);
            }
            if (String.IsNullOrEmpty(LastTweet)) {
                var timeline = twitter.Statuses.UserTimeline(screen_name: twitter.Account.UpdateProfile().ScreenName, count: 1, exclude_replies: true, include_rts: false);
                foreach (var tweet in timeline) {
                    LastTweet = tweet.Text;
                }
            }
        }

        private void SendTweet() {
            var track = trackLink.Text;
            var artist = artistLink.Text;
            var album = albumLink.Text;
            var datetime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            var trackurl = trackLink.Tag.ToString().Replace("spotify:track:", "https://open.spotify.com/track/");
            var artisturl = artistLink.Tag.ToString().Replace("spotify:artist:", "https://open.spotify.com/artist/");
            var albumurl = albumLink.Tag.ToString().Replace("spotify:album:", "https://open.spotify.com/album/");
            var newline = Environment.NewLine;
            var tweet = settings.TweetFormat;

            track = ShortenTweet(track, 30);
            artist = ShortenTweet(artist, 30);
            album = ShortenTweet(album, 30);
            // url 60
            // other 30
            // format 50

            tweet = ReplaceFormat(tweet, "%track%", track);
            tweet = ReplaceFormat(tweet, "%artist%", artist);
            tweet = ReplaceFormat(tweet, "%album%", album);
            tweet = ReplaceFormat(tweet, "%trackurl%", trackurl);
            tweet = ReplaceFormat(tweet, "%artisturl%", artisturl);
            tweet = ReplaceFormat(tweet, "%albumurl%", albumurl);
            tweet = ReplaceFormat(tweet, "%datetime%", datetime);
            tweet = ReplaceFormat(tweet, "%newline%", newline);

            tweet = ShortenTweet(tweet, 140);

            try {
                if (!LastTweet.Equals(artist + "|" + track + "|" + trackurl)) {
                    twitter.Statuses.Update(status: tweet);
                    LastTweet = artist + "|" + track + "|" + trackurl;
                }
            } catch (TwitterException ex) {
                if (ex.Message.Equals("Status is a duplicate.")) {
                    return;
                }
                throw;
            } catch (WebException ex) {
                if (ex.Message.Equals("要求は中止されました: SSL/TLS のセキュリティで保護されているチャネルを作成できませんでした")) {
                    // 不明
                }
                throw;
            } catch (NullReferenceException ex) {
                if (ex.Message.Equals("オブジェクト参照がオブジェクト インスタンスに設定されていません。")) {
                    // 不明？
                }
                throw;
            } catch (Exception ex) {
                if (ex.Message.Contains("またはその依存関係の１つが読み込めませんでした。見つかったアセンブリのマニフェスト定義またはアセンブリ参照に一致しません。")) {
                    Message(ex.Message, "Error: Newtonsoft.Json");
                    // 不明
                }
                throw;
            }
        }

        private string ReplaceFormat(string tweet, string par2, string par3) {
            if (tweet.Contains(par2)) {
                tweet = tweet.Replace(par2, par3);
            }
            return tweet;
        }

        private string ShortenTweet(string tweet, int length) {
            if (tweet.Length > length) {
                tweet = tweet.Substring(0, length - 2) + "..";
            }
            return tweet;
        }

        #endregion

        #region Spotify

        private void SpotifyInit() {
            spotify.OnTrackChange += OnTrackChange;
            spotify.OnPlayStateChange += OnPlayStateChange;

            if (!SpotifyLocalAPI.IsSpotifyRunning()) {
                Message("Spotify が起動していません。");
                EnvironmentExit();
                return;
            }
            if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning()) {
                Message("SpotifyWebHelper が起動していません。");
                EnvironmentExit();
                return;
            }

            var successful = spotify.Connect();
            if (successful) {
                spotify.ListenForEvents = true;
            } else {
                Message("Spotify に接続できませんでした。" + Environment.NewLine + "後ほどお試しください。");
                EnvironmentExit();
            }

            var status = spotify.GetStatus();
            if (status == null) {
                return;
            }

            if (status.Track != null) {
                IsPlaying = status.Playing;
                UpdateTrack(status.Track);
            }
        }

        private void OnTrackChange(object sender, TrackChangeEventArgs e) {
            UpdateTrack(e.NewTrack);
        }

        private void OnPlayStateChange(object sender, PlayStateEventArgs e) {
            IsPlaying = e.Playing;
            if (IsPlaying) {
                UpdateTrack(currentTrack);
            } else {
                timer1.Stop();
            }
        }

        private delegate void CallbackTrack(Track track);
        private async void UpdateTrack(Track track) {
            if (InvokeRequired) {
                var method = new CallbackTrack(UpdateTrack);
                Invoke(method, new object[] { track });
            }

            currentTrack = track;

            if (track.IsAd()) {
                AdUpdate();
                return;
            }

            albumPicture.Image = await track.GetAlbumArtAsync(AlbumArtSize.Size160);
            trackLink.Text = track.TrackResource.Name;
            trackLink.Tag = track.TrackResource.Uri;
            artistLink.Text = track.ArtistResource.Name;
            artistLink.Tag = track.ArtistResource.Uri;
            albumLink.Text = track.AlbumResource.Name;
            albumLink.Tag = track.AlbumResource.Uri;

            CreateNowPlayingFile();

            if (IsPlaying) {
                StartTime = DateTime.Now;
                timer1.Start();
            }
        }

        private void AdUpdate() {
            trackLink.Text = Spotify;
            trackLink.Tag = "";
            artistLink.Text = Spotify;
            artistLink.Tag = "";
            albumLink.Text = Spotify;
            albumLink.Tag = "";
            albumPicture.Image = null;
            timer1.Stop();
        }

        #endregion

        #region Log

        #endregion
    }
}
