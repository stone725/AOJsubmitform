using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AOJsubmitform {
	public partial class SubmitForm : Form {
		private bool _notChanged = true;
		private string _problemNumber;
		public static string UserName = "";
		public static string UserPassWord = "";

		public SubmitForm() {
			InitializeComponent();
			if (File.Exists("Config.txt")) {
				StreamReader configFileReader = new StreamReader("Config.txt");

				UserName = configFileReader.ReadLine();
				UserPassWord = configFileReader.ReadLine();

				configFileReader.Close();
			}
		}


		private void ProblemNumberBoxChanged(object sender, EventArgs e) {
			_problemNumber = ProblemNumberBox.Text;
		}

		private void LanguageboxChanged(object sender, EventArgs e) {

		}

		private void SubmitButtonClick(object sender, EventArgs e) {
			if (_notChanged) {
				MessageBox.Show(@"一切入力していないソースコードは提出できません！");
				return;
			}

			if (_problemNumber.Length < 4) {
				MessageBox.Show(@"問題番号を入力してください!");
				return;
			}
			/*提出設定*/
			HttpWebRequest submitRequest = (HttpWebRequest)WebRequest.Create("http://judge.u-aizu.ac.jp/onlinejudge/servlet/Submit");
			Encoding enc = Encoding.ASCII;
			Hashtable submitPostHashtable = new Hashtable();
			submitPostHashtable["userID"] = WebUtility.UrlEncode(UserName);
			submitPostHashtable["sourceCode"] = WebUtility.UrlEncode(SourceCodeBox.Text);
			submitPostHashtable["problemNO"] = WebUtility.UrlEncode(ProblemNumberBox.Text);
			submitPostHashtable["language"] = WebUtility.UrlEncode(LanguageBox.Text);
			submitPostHashtable["password"] = WebUtility.UrlEncode(UserPassWord);
			submitRequest.Method = "POST";

			String submitParam = "";
			submitRequest.Timeout = 1000000000;
			foreach (String k in submitPostHashtable.Keys) {
				submitParam += String.Format("{0}={1}&", k, submitPostHashtable[k]);
			}
			Byte[] submitData = Encoding.ASCII.GetBytes(submitParam);
			submitRequest.ContentType = "application/x-www-form-urlencoded";
			submitRequest.ContentLength = submitData.Length;
			submitPostHashtable.Clear();


			/*これから行う提出の一つ前の提出の提出番号を取得する*/
			String responseUrl = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/status_log?user_id=" + UserName;
			HttpWebRequest lastRunIdRequest = (HttpWebRequest)WebRequest.Create(responseUrl);
			lastRunIdRequest.Timeout = 1000000000;
			WebResponse lastRunIdresponse = lastRunIdRequest.GetResponse();
			Stream lastrRunIdResstream = lastRunIdresponse.GetResponseStream();
			StreamReader lastRunIdStreamReader = new StreamReader(lastrRunIdResstream, enc);
			String lastSubmitResponse = lastRunIdStreamReader.ReadToEnd();
			lastRunIdStreamReader.Close();
			lastrRunIdResstream.Close();
			lastRunIdresponse.Close();
			const string runIdStartMark = "<run_id>\n";
			Int32 lastRunIdStartIndex = lastSubmitResponse.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) + runIdStartMark.Length;
			Int32 lastRunIdEndIndex = lastSubmitResponse.IndexOf("\n", lastRunIdStartIndex, StringComparison.Ordinal);
			String lastRunId = lastSubmitResponse.Substring(lastRunIdStartIndex, lastRunIdEndIndex - lastRunIdStartIndex);


			/*ポストデータの書き込み*/
			Stream submitReqStream = submitRequest.GetRequestStream();
			submitReqStream.Write(submitData, 0, submitData.Length);
			submitReqStream.Close();

			/*レスポンスの取得と読み込み*/

			/*提出結果を格納する変数*/
			string submitResponse = "";
			int challanged = 0;
			bool success = false;
			while (challanged < 1000) {
				HttpWebRequest runIdRequest = (HttpWebRequest)WebRequest.Create(responseUrl);
				runIdRequest.Timeout = 1000000000;
				WebResponse runIdResponse = runIdRequest.GetResponse();
				Stream runIdResStream = runIdResponse.GetResponseStream();
				StreamReader runIdStreamReader = new StreamReader(runIdResStream, enc);
				submitResponse = runIdStreamReader.ReadToEnd();
				runIdStreamReader.Close();
				runIdResStream.Close();
				runIdResponse.Close();
				Int32 runIdStart = submitResponse.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) + runIdStartMark.Length;
				Int32 runIdEnd = submitResponse.IndexOf("\n", runIdStart, StringComparison.Ordinal);
				if (lastRunId != submitResponse.Substring(runIdStart, runIdEnd - runIdStart)) {
					success = true;
					break;
				}
				Thread.Sleep(200);
				challanged++;
			}
			/*提出結果の取得*/
			const string statusStartMark = "<status>\n";
			Int32 statusStartIndex = submitResponse.IndexOf(statusStartMark, submitResponse.IndexOf(statusStartMark, StringComparison.Ordinal) + statusStartMark.Length, StringComparison.Ordinal) + statusStartMark.Length;
			Int32 statusIdEndIndex = submitResponse.IndexOf("\n</status>", statusStartIndex, StringComparison.Ordinal);
			String submitResult = submitResponse.Substring(statusStartIndex, statusIdEndIndex - statusStartIndex);

			/*提出結果の表示*/
			string extension = @".txt";
			if (success) {
				MessageBox.Show(submitResult);
				/*提出結果がACだった場合ファイルに保存する*/
				if (submitResult == "Accepted") {

					switch (LanguageBox.Text) {
					case "C++":
					case "C++11":
						extension = @".cpp";
						break;
					case "C":
						extension = @".c";
						break;
					case "JAVA":
						extension = @".java";
						break;
					case "C#":
						extension = @".cs";
						break;
					case "D":

						extension = @".d";
						break;
					case "Ruby":

						extension = @".rb";
						break;
					case "Python":
					case "Python3":

						extension = @".py";
						break;
					case "PHP":
						extension = @".php";
						break;
					case "JavaScript":
						extension = @".js";
						break;
					}
					if (File.Exists(@"AOJ-SourceCode\\Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2)) == false) {
						Directory.CreateDirectory(@"AOJ-SourceCode\\Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2));
					}
					StreamWriter sourceCodeFileWriter = new StreamWriter(@"AOJ-SourceCode\\Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2) + @"\\" + _problemNumber + extension, false, Encoding.Default);
					sourceCodeFileWriter.Write(SourceCodeBox.Text);
					sourceCodeFileWriter.Close();
				}
			}
			else {
				MessageBox.Show(@"Submit Error!");
			}
			Close();
		}

		private void SourceCodeChanged(object sender, EventArgs e) {
			_notChanged = false;
		}
		private void ConfigButtonClick(object sender, EventArgs e) {
			ConfigForm configForm2 = new ConfigForm();
			configForm2.Show();
		}

	}
}
