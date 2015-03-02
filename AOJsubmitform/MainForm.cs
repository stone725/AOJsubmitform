using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TweetSharp;

namespace AOJsubmitform {
	public partial class MainForm : Form {
		private string _problemNumber = "";
		public static TwitterService TwitterService = new TwitterService("pE7l8lWq5dtB8kpx4TmeCC52M", "y11AazZjk43jHOAK2TzVX4nk1R397kjWVNmtRHs94e5HgwuLFy");
		public static OAuthRequestToken TwitterRequestToken;
		public static string TwitterVerifier;
		public static OAuthAccessToken TwitterAccess;
		public static string TwitterToken;
		public static string TwitterTokenSecret;
		public static string TwitterTokenUserName = "";
		public static string UserName = "";
		public static string UserPassWord = "";
		public static string WriteDirectory = "";
		public static string ProblemName = "";
		public static bool SaveProblemName = false;
		public static bool TweetAll = false;
		private readonly FileWriter _fileWriter = new FileWriter();
		public MainForm() {
			InitializeComponent();
			//引数付きで実行されたとき引数のPATHを捜索しファイルを読み込む
			if (Program.FileName != "")
			{
				if (File.Exists(Program.FileName))
				{
					StreamReader sourceCodeReader = new StreamReader(Program.FileName);
					SourceCodeBox.Text = sourceCodeReader.ReadToEnd();
					sourceCodeReader.Close();
				}
				else
				{
					MessageBox.Show(@"読み込むファイルがありません!");
				}
			}
			//新しい方式の設定記録ファイル（無意味なバイナリ式）から設定を呼び出す
			if (File.Exists("UserName.bin") && File.Exists("UserPassWord.bin") && File.Exists("WriteDirectory.bin") && File.Exists("SaveProblemName.bin") && File.Exists("TweetAll.bin"))
			{
				UserName = Encoding.Unicode.GetString(File.ReadAllBytes("UserName.bin"));
				UserPassWord = Encoding.Unicode.GetString(File.ReadAllBytes("UserPassWord.bin"));
				WriteDirectory = Encoding.Unicode.GetString(File.ReadAllBytes("WriteDirectory.bin"));
				SaveProblemName = Encoding.Unicode.GetString(File.ReadAllBytes("UserPassWord.bin")) == "save";
				TweetAll = Encoding.Unicode.GetString(File.ReadAllBytes("TweetAll.bin")) == "tweet";
			}
			//旧形式の設定記録ファイルから設定を呼び出して新形式の設定記録方式に変更する
			else if (File.Exists("Config.txt")) {
				StreamReader configFileReader = new StreamReader("Config.txt");
				UserName = configFileReader.ReadLine();
				File.WriteAllBytes("UserName.bin", Encoding.Unicode.GetBytes(UserName)); 
				UserPassWord = configFileReader.ReadLine();
				File.WriteAllBytes("UserPassWord.bin", Encoding.Unicode.GetBytes(UserPassWord)); 
				WriteDirectory = configFileReader.ReadLine();
				File.WriteAllBytes("WriteDirectory.bin", Encoding.Unicode.GetBytes(WriteDirectory)); 
				SaveProblemName = configFileReader.ReadLine() == "save";
				File.WriteAllBytes("SaveProblemName.bin", Encoding.Unicode.GetBytes(SaveProblemName ? "save" : "")); 
				configFileReader.Close();
				//旧形式の設定ファイルを削除する
				File.Delete(@"Config.txt");
				File.WriteAllBytes("TweetAll.bin", Encoding.Unicode.GetBytes(TweetAll ? "tweet" : ""));
			}
			//Twitterにおける設定も同様に行う
			if (File.Exists("TwitterToken.bin") && File.Exists("TwitterTokenSecret.bin"))
			{
				TwitterToken = Encoding.Unicode.GetString(File.ReadAllBytes("TwitterToken.bin"));
				TwitterTokenSecret = Encoding.Unicode.GetString(File.ReadAllBytes("TwitterTokenSecret.bin"));
				TwitterService.AuthenticateWith(TwitterToken, TwitterTokenSecret);
			}
			else if (File.Exists("TwitterConfig.txt"))
			{
				StreamReader twitterConfigFileReader = new StreamReader("TwitterConfig.txt");
				TwitterToken = twitterConfigFileReader.ReadLine();
				File.WriteAllBytes("TwitterToken.bin", Encoding.Unicode.GetBytes(TwitterToken));
				TwitterTokenSecret = twitterConfigFileReader.ReadLine();
				File.WriteAllBytes("TwitterTokenSecret.bin", Encoding.Unicode.GetBytes(TwitterTokenSecret)); 
				twitterConfigFileReader.Close();
				File.Delete(@"TwitterConfig.txt");
				TwitterService.AuthenticateWith(TwitterToken, TwitterTokenSecret);
				
			}
			//言語ボックスの初期化
			LanguageBox.Text = "C++";
		}

		//問題番号が変更されたとき
		private void ProblemNumberBoxChanged(object sender, EventArgs e) {
			_problemNumber = ProblemNumberBox.Text;
		}
		
		private void LanguageboxChanged(object sender, EventArgs e) {

		}

		private void SubmitButtonClick(object sender, EventArgs e) {
			//ソースコードが入力されていなかった場合
			if (SourceCodeBox.Text == "") {
				MessageBox.Show(@"一切入力していないソースコードは提出できません！");
				return;
			}
			//問題番号が不十分な場合
			if (_problemNumber.Length < 4) {
				MessageBox.Show(@"問題番号を入力してください!");
				return;
			}
			ProblemName = GetProblemName.Getproblemname(_problemNumber);
			//問題が存在していなかった場合
			if (ProblemName == "")
			{
				MessageBox.Show(@"正しい問題番号を入力してください!");
				return;
			}
			//AOJアカウントの取得
			AojAccount aojuserAccount = new AojAccount(UserName, UserPassWord);
			//ユーザー名が入力されていなかった場合
			if (aojuserAccount.GetUserName() == "")
			{
				MessageBox.Show(@"アカウント名を入力してください！");
				return;
			}
			//ユーザーのパスワードが入力されていなかった場合
			if (aojuserAccount.GetUserPass() == "")
			{
				MessageBox.Show(@"パスワードを入力してください!");
				return;
			}
			//保存するディレクトリ名が空ではなかった場合そのディレクトリにフォルダを保存するようにする
			if (WriteDirectory != "") {
				WriteDirectory += @"\\";
			}
			//提出処理
			AojSubmit aojSubmit = new AojSubmit(aojuserAccount);
			var status = aojSubmit.Submit(_problemNumber, LanguageBox.Text, SourceCodeBox.Text);
			string directoryName = WriteDirectory + @"Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2) + @"\\";
			string fileName = _problemNumber;
			if (SaveProblemName)
			{
				fileName += " " + ProblemName;
			}
			//問題名を保存する設定にしていて使用できない文字が含まれていた場合その文字を排除する
			string[] CannotUseName = new[] { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };
			foreach (string c in CannotUseName) {
				fileName = fileName.Replace(c, "");
			}
			fileName += GetExtension.getExtension(LanguageBox.Text);
			TopMost = true;
			MessageBox.Show(status.ToDisplayString());
			if (status == JudgeStatus.Accepted || status == JudgeStatus.ParialPoints || TweetAll)
      {
				TwitterService.SendTweet(new SendTweetOptions
				{
					Status =
						UserName + "がAOJ" + _problemNumber + ":" + ProblemName + "を言語:" + LanguageBox.Text +
						"で" + status.ToAbbreviation() + "しました!\nhttp://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=" + 
            _problemNumber + "&lang=jp\n#AOJWAinfo #AOJ_WA #AOJsubmitinfo"
				});
			}
			Close();
		}
		
		private void SourceCodeChanged(object sender, EventArgs e) {
		}
		private void ConfigButtonClick(object sender, EventArgs e) {
			ConfigForm configForm2 = new ConfigForm();
			configForm2.Show();
		}

	}
}
