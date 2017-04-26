using System;
using System.Windows.Forms;
using System.Xml;

namespace AOJsubmitform
{
  public partial class TwitterAttestationForm : Form
  {
    private Config config;
    public TwitterAttestationForm(Config config)
    {
      InitializeComponent();
      this.config = config;
    }

    private void Form3_Load(object sender, EventArgs e)
    {

      MainForm.TwitterRequestToken = MainForm.TwitterService.GetRequestToken();
      Uri uri = MainForm.TwitterService.GetAuthenticationUrl(MainForm.TwitterRequestToken);
      webBrowser1.Url = uri;
    }
    private void GetToken()
    {
      MainForm.TwitterAccess = MainForm.TwitterService.GetAccessToken(MainForm.TwitterRequestToken, MainForm.TwitterVerifier);
      MainForm.TwitterService.AuthenticateWith(MainForm.TwitterAccess.Token, MainForm.TwitterAccess.TokenSecret);
      config.twittertoken_ = MainForm.TwitterAccess.Token;
      config.twittertokensecret_ = MainForm.TwitterAccess.TokenSecret;
      config.SaveConfig();
      Close();
    }

    private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      string code = webBrowser1.DocumentText;//codeにソースコードを入れ
      if (-1 != code.IndexOf("<CODE>", StringComparison.Ordinal))//ソース内に「<code>」が存在するか
      {
        webBrowser1.Visible = false;//コードを取得したので,認証画面を消す
        int loc = code.IndexOf("<CODE>", StringComparison.Ordinal);//「<code>」の位置を取得.
        MainForm.TwitterVerifier = code.Substring(loc, 13);//「<code>」の位置から13文字分を取り出す.
        MainForm.TwitterVerifier = MainForm.TwitterVerifier.Replace("<CODE>", "");//「<code>」を削除
        GetToken();//トーキンを取得.
      }
      else if (-1 != code.IndexOf("<code>", StringComparison.Ordinal))//ソース内に「<code>」が存在するか
      {
        webBrowser1.Visible = false;//コードを取得したので,認証画面を消す
        int loc = code.IndexOf("<code>", StringComparison.Ordinal);//「<code>」の位置を取得.
        MainForm.TwitterVerifier = code.Substring(loc, 13);//「<code>」の位置から13文字分を取り出す.
        MainForm.TwitterVerifier = MainForm.TwitterVerifier.Replace("<code>", "");//「<code>」を削除
        GetToken();//トーキンを取得.
      }
    }
  }
}
