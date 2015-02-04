using System;
using System.IO;
using System.Windows.Forms;
using TweetSharp;

namespace AOJsubmitform {
	public partial class MainForm : Form {
		private string _problemNumber;
		
		public static TwitterService TwitterService = new TwitterService("pE7l8lWq5dtB8kpx4TmeCC52M", "y11AazZjk43jHOAK2TzVX4nk1R397kjWVNmtRHs94e5HgwuLFy");
		public static OAuthRequestToken TwitterRequestToken;
		public static string TwitterVerifier;
		public static OAuthAccessToken TwitterAccess;
		public static string TwitterToken;
		public static string TwitterTokenSecret;
		
		public static string UserName = "";
		public static string UserPassWord = "";
		
		public static string WriteDirectory = "";
		public static string Language = "";
		public static string SourceCode = "";
		
		private readonly GetExtension _extension = new GetExtension();
		private readonly FileWriter _fileWriter = new FileWriter();
		
		public MainForm() {
			InitializeComponent();

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
			if (File.Exists("Config.txt")) {
				StreamReader configFileReader = new StreamReader("Config.txt");

				UserName = configFileReader.ReadLine();
				UserPassWord = configFileReader.ReadLine();
				WriteDirectory = configFileReader.ReadLine();
				configFileReader.Close();
			}
			//ツイッターの設定ファイルの読み込み
			if (File.Exists("TwitterConfig.txt"))
			{
				StreamReader twitterConfigFileReader = new StreamReader("TwitterConfig.txt");
				TwitterToken = twitterConfigFileReader.ReadLine();
				TwitterTokenSecret = twitterConfigFileReader.ReadLine();
				twitterConfigFileReader.Close();
				TwitterService.AuthenticateWith(TwitterToken, TwitterTokenSecret);
				
			}
		}

		private void ProblemNumberBoxChanged(object sender, EventArgs e) {
			_problemNumber = ProblemNumberBox.Text;
		}

		private void LanguageboxChanged(object sender, EventArgs e)
		{
			Language = LanguageBox.Text;
		}
		private void SubmitButtonClick(object sender, EventArgs e) {
			if (SourceCode == "") {
				MessageBox.Show(@"一切入力していないソースコードは提出できません！");
				return;
			}
			if (_problemNumber	 == "") {
				MessageBox.Show(@"問題番号を入力してください!");
				return;
			}
			if (WriteDirectory != "") {
				WriteDirectory += @"\\";
			}
			AojAccount aojuserAccount = new AojAccount(UserName, UserPassWord);
			AojSubmit aojSubmit = new AojSubmit(aojuserAccount);
			int status = aojSubmit.Submit(_problemNumber, Language, SourceCode);
			string directoryName = WriteDirectory + @"Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2) + @"\\";
			string fileName = _problemNumber + _extension.getExtension(Language);
			switch (status)
			{
				case -1:
					MessageBox.Show(@"Submit Error!");
					break;
				case 2:
					MessageBox.Show(@"Wrong Answer");
					break;
				case 3:
					MessageBox.Show(@"Runtime Error");
					break;
				case 4:
					MessageBox.Show(@"Time Limit Exceeded");
					break;
				case 5:
					MessageBox.Show(@"Memory Limit Exceeded");
					break;
				case 6:
					MessageBox.Show(@"Compile Error");
					break;
				case 7:
					MessageBox.Show(@"Presentation Error");
					break;
				case 8:
					MessageBox.Show(@"Output Limit Exceeded");
					break;
				case 9:
					MessageBox.Show(@"Judge Not Available");
					break;
				case 1:
					MessageBox.Show(@"Partial Points");
					_fileWriter.Write(directoryName, fileName, "//Partial Points.\n" + SourceCodeBox.Text);
					break;
				case 0:
					MessageBox.Show(@"Accepted");
					_fileWriter.Write(directoryName, fileName, SourceCode);
					TwitterService.SendTweet(new SendTweetOptions { Status = UserName + "がAOJ" + _problemNumber + "を言語:" + Language + "でACしました!\n#AOJACinfo #AOJ_AC" });
					break;
			}
			Close();
		}

		private void SourceCodeChanged(object sender, EventArgs e)
		{
			SourceCode = SourceCodeBox.Text;
		}
		private void ConfigButtonClick(object sender, EventArgs e) {
			ConfigForm configForm2 = new ConfigForm();
			configForm2.Show();
		}

	}
}
