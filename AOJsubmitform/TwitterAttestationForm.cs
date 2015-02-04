//Twitter認証
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AOJsubmitform {
	public partial class TwitterAttestationForm : Form {
		//初期化
		public TwitterAttestationForm() {
			InitializeComponent();
		}
		
		private void FormLoad(object sender, EventArgs e) {
			
			MainForm.TwitterRequestToken = MainForm.TwitterService.GetRequestToken();
			Uri uri = MainForm.TwitterService.GetAuthenticationUrl(MainForm.TwitterRequestToken);
			TwitterAttestationBrowser.Url = uri;
		}
		//トークンを所得
		private void GetToken() {
			MainForm.TwitterAccess = MainForm.TwitterService.GetAccessToken(MainForm.TwitterRequestToken, MainForm.TwitterVerifier);
			MainForm.TwitterService.AuthenticateWith(MainForm.TwitterAccess.Token, MainForm.TwitterAccess.TokenSecret);
			MainForm.TwitterToken = MainForm.TwitterAccess.Token;
			MainForm.TwitterTokenSecret = MainForm.TwitterAccess.TokenSecret;
			StreamWriter configFileWriter = new StreamWriter(@"TwitterConfig.txt", false, Encoding.Default);
			configFileWriter.WriteLine(MainForm.TwitterToken);
			configFileWriter.WriteLine(MainForm.TwitterTokenSecret);
			configFileWriter.Close();
			Close();
		}

		private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			string code = TwitterAttestationBrowser.DocumentText;
			//認証コードが掲示されたページだった場合認証コードを自動取得する
			if (-1 != code.IndexOf("<CODE>", StringComparison.Ordinal))
			{
				TwitterAttestationBrowser.Visible = false;
				int loc = code.IndexOf("<CODE>", StringComparison.Ordinal);
				MainForm.TwitterVerifier = code.Substring(loc, 13);
				MainForm.TwitterVerifier = MainForm.TwitterVerifier.Replace("<CODE>", "");
				GetToken();
			}
		}

		


	}
}
