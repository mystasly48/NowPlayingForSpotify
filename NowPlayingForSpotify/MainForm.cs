using System;
using System.ComponentModel;
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

    SpotifyLocalAPI spotify = new SpotifyLocalAPI();
    Tokens twitter;
    SplashForm splash = new SplashForm();
    Track currentTrack;
    DateTime StartTime;
    bool IsPlaying = false;
    bool IsLocalTrack = false;
    string LastTweet = "";

    #endregion

    #region Other

    private void ShowSplash() {
      splash.Show();
      splash.Refresh();
    }

    private void CloseSplash() {
      splash.Close();
      splash.Dispose();
      Activate();
    }

    private void OpenLink(LinkLabel label) {
      if (!string.IsNullOrEmpty(label.Tag.ToString())) {
        Process.Start(label.Tag.ToString());
      }
    }

    private void init() {
      SettingsInit();
      TwitterInit();
      SpotifyInit();
    }

    private void SettingsInit() {
      Settings.Default.SettingsSaving += SettingsSaving;
      LastTweet = Settings.Default.LastTweet;
      Location = Settings.Default.Location;
      TopMost = Settings.Default.TopMost;
    }

    private void SaveSettings() {
      Settings.Default.LastTweet = LastTweet;
      if (WindowState != FormWindowState.Minimized)
        Settings.Default.Location = Location;
      Settings.Default.Save();
    }

    private void EnvironmentExit() {
      Environment.Exit(0);
    }

    private void Exit() {
      Application.Exit();
    }

    private void Message(string msg) {
      MessageBox.Show(msg, Information.Name);
    }

    private void Message(string msg, string title) {
      MessageBox.Show(msg, title);
    }

    private void HideNotifyIcon() {
      Show();
      notifyIcon1.Visible = false;
    }

    private void ShowNotifyIcon() {
      Hide();
      notifyIcon1.Visible = true;
    }

    private void CreateNowPlayingFile() {
      if (Settings.Default.CreateText) {
        var path = Information.NowPlaying + ".txt";
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
      ActiveControl = albumPicture;
    }

    private void MainForm_Shown(object sender, EventArgs e) {

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

    private void hideBtn_Click(object sender, EventArgs e) {
      ShowNotifyIcon();
    }

    private void settingsBtn_Click(object sender, EventArgs e) {
      var settingsForm = new SettingsForm();
      settingsForm.ShowDialog();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
      SaveSettings();
      notifyIcon1.Visible = false;
      notifyIcon1.Dispose();
    }

    private void aboutBtn_Click(object sender, EventArgs e) {
      var aboutForm = new AboutForm();
      aboutForm.ShowDialog();
    }

    #endregion

    #region Twitter

    private void TwitterInit() {
      if (string.IsNullOrEmpty(Settings.Default.AccessToken) || string.IsNullOrEmpty(Settings.Default.AccessSecret)) {
        START:
        var session = OAuth.Authorize(SecretKeys.ConsumerKey, SecretKeys.ConsumerSecret);
        var browser = new AuthBrowser();
        browser.URL = session.AuthorizeUri.AbsoluteUri;
        browser.ShowDialog();
        if (browser.Success) {
          twitter = session.GetTokens(browser.PIN);
        } else {
          var result = MessageBox.Show(Resources.ThereIsNotTwitterAccount + Environment.NewLine + Resources.AreYouWantToReAuthentication + "(" + Resources.WhenYouSelectYesProgramWillExit + ")", Information.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
          switch (result) {
            case DialogResult.Yes:
              goto START;
            case DialogResult.No:
              Environment.Exit(0);
              break;
          }
        }
        browser.Dispose();
        Settings.Default.AccessToken = twitter.AccessToken;
        Settings.Default.AccessSecret = twitter.AccessTokenSecret;
        Settings.Default.Save();
      } else {
        twitter = Tokens.Create(SecretKeys.ConsumerKey, SecretKeys.ConsumerSecret, Settings.Default.AccessToken, Settings.Default.AccessSecret);
      }
      if (string.IsNullOrEmpty(LastTweet)) {
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
      var tweet = Settings.Default.TweetFormat;

      track = ShortenTweet(track, 30);
      artist = ShortenTweet(artist, 30);
      album = ShortenTweet(album, 30);
      // url 60
      // other 30
      // format 50

      tweet = ReplaceFormat(tweet, "%track%", track);
      tweet = ReplaceFormat(tweet, "%artist%", artist);
      tweet = ReplaceFormat(tweet, "%album%", album);
      tweet = ReplaceFormat(tweet, "%datetime%", datetime);
      tweet = ReplaceFormat(tweet, "%newline%", newline);
      if (IsLocalTrack) {
        tweet = tweet.Replace("%trackurl%", "");
        tweet = tweet.Replace("%artisturl%", "");
        tweet = tweet.Replace("%albumurl%", "");
      } else {
        tweet = ReplaceFormat(tweet, "%trackurl%", trackurl);
        tweet = ReplaceFormat(tweet, "%artisturl%", artisturl);
        tweet = ReplaceFormat(tweet, "%albumurl%", albumurl);
      }

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
        CloseSplash();
        Message(Resources.SpotifyDoesNotRunning);
        EnvironmentExit();
        return;
      }
      if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning()) {
        SpotifyLocalAPI.RunSpotifyWebHelper();
      }

      var successful = spotify.Connect();
      if (successful) {
        spotify.ListenForEvents = true;
      } else {
        CloseSplash();
        Message(Resources.CantConnectToSpotify + Environment.NewLine + Resources.PleaseTryAgainLater);
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

      IsLocalTrack = false;

      currentTrack = track;

      if (track.TrackType == "normal") {
        albumPicture.Image = await track.GetAlbumArtAsync(AlbumArtSize.Size160);
        trackLink.Text = track.TrackResource.Name;
        trackLink.Tag = track.TrackResource.Uri;
        artistLink.Text = track.ArtistResource.Name;
        artistLink.Tag = track.ArtistResource.Uri;
        albumLink.Text = track.AlbumResource.Name;
        albumLink.Tag = track.AlbumResource.Uri;
      } else if (track.TrackType == "local") {
        LocalUpdate(track);
        IsLocalTrack = true;
      } else if (track.IsAd()) {
        AdUpdate();
        return;
      }

      CreateNowPlayingFile();

      if (IsPlaying) {
        StartTime = DateTime.Now;
        timer1.Start();
      }
    }

    private void AdUpdate() {
      trackLink.Text = Information.Spotify;
      trackLink.Tag = "";
      artistLink.Text = Information.Spotify;
      artistLink.Tag = "";
      albumLink.Text = Information.Spotify;
      albumLink.Tag = "";
      albumPicture.Image = Resources.NoImage;
      timer1.Stop();
    }

    private void LocalUpdate(Track track) {
      trackLink.Text = track.TrackResource.Name;
      trackLink.Tag = "";
      artistLink.Text = track.ArtistResource.Name;
      artistLink.Tag = "";
      albumLink.Text = track.AlbumResource.Name;
      albumLink.Tag = "";
      albumPicture.Image = Resources.NoImage;
    }

    #endregion

    #region Settings

    private void SettingsSaving(object sender, CancelEventArgs e) {
      TopMost = Settings.Default.TopMost; // Always update
    }

    #endregion
  }
}
