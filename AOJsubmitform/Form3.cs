using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AOJsubmitform {
	public partial class Form3 : Form {
		public Form3() {
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e) {
			
			SubmitForm.TwitterRequestToken = SubmitForm._twitterService.GetRequestToken();
			Uri uri = SubmitForm._twitterService.GetAuthenticationUrl(SubmitForm.TwitterRequestToken);
			webBrowser1.Url = uri;
		}
		private void GetToken() {
			SubmitForm.TwitterAccess = SubmitForm._twitterService.GetAccessToken(SubmitForm.TwitterRequestToken, SubmitForm.TwitterVerifier);
			SubmitForm._twitterService.AuthenticateWith(SubmitForm.TwitterAccess.Token, SubmitForm.TwitterAccess.TokenSecret);
			SubmitForm.TwitterToken = SubmitForm.TwitterAccess.Token;
			SubmitForm.TwitterTokenSecret = SubmitForm.TwitterAccess.TokenSecret;
			StreamWriter configFileWriter = new StreamWriter(@"TwitterConfig.txt", false, Encoding.Default);
			configFileWriter.WriteLine(SubmitForm.TwitterToken);
			configFileWriter.WriteLine(SubmitForm.TwitterTokenSecret);
			configFileWriter.Close();
			Close();
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			string code = webBrowser1.DocumentText;//codeにソースコードを入れる
			if (-1 != code.IndexOf("<CODE>"))//ソース内に「<code>」が存在するか
			{
				webBrowser1.Visible = false;//コードを取得したので,認証画面を消す
				int loc = code.IndexOf("<CODE>");//「<code>」の位置を取得.
				SubmitForm.TwitterVerifier = code.Substring(loc, 13);//「<code>」の位置から13文字分を取り出す.
				SubmitForm.TwitterVerifier = SubmitForm.TwitterVerifier.Replace("<CODE>", "");//「<code>」を削除
				SubmitForm.got_token = true;
				GetToken();//トーキンを取得.
			}
		}

		


	}
}
