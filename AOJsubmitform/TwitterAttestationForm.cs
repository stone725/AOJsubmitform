using System;
using System.Windows.Forms;

namespace AOJsubmitform {
	public partial class TwitterAttestationForm : Form {
		public TwitterAttestationForm() {
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e) {
			
			MainForm.TwitterRequestToken = MainForm.TwitterService.GetRequestToken();
			Uri uri = MainForm.TwitterService.GetAuthenticationUrl(MainForm.TwitterRequestToken);
			webBrowser1.Url = uri;
		}
		private void GetToken() {
			MainForm.TwitterAccess = MainForm.TwitterService.GetAccessToken(MainForm.TwitterRequestToken, MainForm.TwitterVerifier);
			MainForm.TwitterService.AuthenticateWith(MainForm.TwitterAccess.Token, MainForm.TwitterAccess.TokenSecret);
			MainForm.TwitterToken = MainForm.TwitterAccess.Token;
			MainForm.TwitterTokenSecret = MainForm.TwitterAccess.TokenSecret;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.TwitterConfigWrite(MainForm.TwitterToken, MainForm.TwitterTokenSecret);
			Close();
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			string code = webBrowser1.DocumentText;//codeにソースコードを入れる
			if (-1 != code.IndexOf("<CODE>", StringComparison.Ordinal))//ソース内に「<code>」が存在するか
			{
				webBrowser1.Visible = false;//コードを取得したので,認証画面を消す
				int loc = code.IndexOf("<CODE>", StringComparison.Ordinal);//「<code>」の位置を取得.
				MainForm.TwitterVerifier = code.Substring(loc, 13);//「<code>」の位置から13文字分を取り出す.
				MainForm.TwitterVerifier = MainForm.TwitterVerifier.Replace("<CODE>", "");//「<code>」を削除
				GetToken();//トーキンを取得.
			}
		}

		


	}
}
