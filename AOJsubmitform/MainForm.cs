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

    public Config Config = new Config();

		public static string ProblemName = "";
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
      Config.Load();
      if (Config.TwitterToken != "" && Config.TwitterTokenSecret != "")
        TwitterService.AuthenticateWith(Config.TwitterToken, Config.TwitterTokenSecret);
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
			AojAccount aojuserAccount = new AojAccount(Config.Usename, Config.Password);
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
			if (Config.SaveDirectory != "") {
				Config.SaveDirectory += @"\\";
			}
			//提出処理
			AojSubmit aojSubmit = new AojSubmit(aojuserAccount);
      JudgeStatus status;
      try
      {
        status = aojSubmit.Submit(_problemNumber, LanguageBox.Text, SourceCodeBox.Text);
      }
      catch 
      {
        MessageBox.Show("Submit Error occured!");
        return;
      }
			string directoryName = Config.SaveDirectory + @"Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2) + @"\\";
			string fileName = _problemNumber;
			if (Config.IsSaveProblemName)
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
      if (status == JudgeStatus.Accepted || status == JudgeStatus.ParialPoints || Config.IsTweetAll)
      {
        TwitterService.SendTweet(new SendTweetOptions
        {
          Status =
            Config.Usename + "がAOJ" + _problemNumber + ":" + ProblemName + "を言語:" + LanguageBox.Text +
            "で" + status.ToAbbreviation() + "しました!\nhttp://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=" +
            _problemNumber + "&lang=jp\n#AOJWAinfo #AOJ_WA #AOJsubmitinfo"
        });
      }
			Close();
		}
		
		private void SourceCodeChanged(object sender, EventArgs e) {
		}
		private void ConfigButtonClick(object sender, EventArgs e) {
			ConfigForm configForm2 = new ConfigForm(Config);
			configForm2.Show();
		}

	}
}
