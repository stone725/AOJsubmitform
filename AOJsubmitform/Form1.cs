using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace AOJsubmitform {
	public partial class Form1 : Form {
		private bool _notChanged = true;
		private string _problemNumber;
		public static string _userName = "";
		public static string _userPassWord = "";

		public Form1() {
			InitializeComponent();
			if (File.Exists("Config.txt")) {
				var configFile = new StreamReader("Config.txt");

				_userName = configFile.ReadLine();
				_userPassWord = configFile.ReadLine();

				configFile.Close();
			}
		}


		private void textBox1_TextChanged(object sender, EventArgs e) {
			_problemNumber = textBox1.Text;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void button1_Click(object sender, EventArgs e) {
			if (_notChanged) {
				MessageBox.Show(@"まったく同じソースコードは提出できません!");
				return;
			}

			if (_problemNumber.Length < 4) {
				MessageBox.Show(@"問題番号を入力してください!");
				return;
			}
			/*提出設定*/
			HttpWebRequest submitRequest = (HttpWebRequest)WebRequest.Create("http://judge.u-aizu.ac.jp/onlinejudge/servlet/Submit");
			Encoding enc = Encoding.ASCII;
			var submitPostHashtable = new Hashtable();
			submitPostHashtable["userID"] = WebUtility.UrlEncode(_userName);
			submitPostHashtable["sourceCode"] = WebUtility.UrlEncode(richTextBox1.Text);
			submitPostHashtable["problemNO"] = WebUtility.UrlEncode(textBox1.Text);
			submitPostHashtable["language"] = WebUtility.UrlEncode(comboBox1.Text);
			submitPostHashtable["password"] = WebUtility.UrlEncode(_userPassWord);
			submitRequest.Method = "POST";

			var submitParam = "";
			submitRequest.Timeout = 1000000000;
			foreach (string k in submitPostHashtable.Keys) {
				submitParam += String.Format("{0}={1}&", k, submitPostHashtable[k]);
			}
			var submitData = Encoding.ASCII.GetBytes(submitParam);
			submitRequest.ContentType = "application/x-www-form-urlencoded";
			submitRequest.ContentLength = submitData.Length;
			submitPostHashtable.Clear();
			/*これから行う提出の一つ前の提出の提出番号を取得する*/
			var responseUrl = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/status_log?user_id=" + _userName;
			var lastRunIdRequest = (HttpWebRequest)WebRequest.Create(responseUrl);
			lastRunIdRequest.Timeout = 1000000000;
			var lastRunIdresponse = lastRunIdRequest.GetResponse();
			var lastrRunIdResstream = lastRunIdresponse.GetResponseStream();
			var lastRunIdStreamReader = new StreamReader(lastrRunIdResstream, enc);
			var lastSubmitResponse = lastRunIdStreamReader.ReadToEnd();
			lastRunIdStreamReader.Close();
			lastrRunIdResstream.Close();
			lastRunIdresponse.Close();
			const string runIdStartMark = "<run_id>\n";
			var lastRunIdStartIndex = lastSubmitResponse.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) +
									  runIdStartMark.Length;
			var lastRunIdEndIndex = lastSubmitResponse.IndexOf("\n", lastRunIdStartIndex, StringComparison.Ordinal);
			var lastRunId = lastSubmitResponse.Substring(lastRunIdStartIndex, lastRunIdEndIndex - lastRunIdStartIndex);


			/*ポストデータの書き込み*/
			var submitReqStream = submitRequest.GetRequestStream();
			submitReqStream.Write(submitData, 0, submitData.Length);
			submitReqStream.Close();

			/*レスポンスの取得と読み込み*/

			/*提出結果を格納する変数*/
			string SubmitResponse = "";
			int challanged = 0;
			bool success = false;
			while (challanged < 1000) {
				var runIdRequest = (HttpWebRequest)WebRequest.Create(responseUrl);
				runIdRequest.Timeout = 1000000000;
				var runIdResponse = runIdRequest.GetResponse();
				var runIdResStream = runIdResponse.GetResponseStream();
				var runIdStreamReader = new StreamReader(runIdResStream, enc);
				SubmitResponse = runIdStreamReader.ReadToEnd();
				runIdStreamReader.Close();
				runIdResStream.Close();
				runIdResponse.Close();
				var runIdStart = SubmitResponse.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) + runIdStartMark.Length;
				var runIdEnd = SubmitResponse.IndexOf("\n", runIdStart, StringComparison.Ordinal);
				if (lastRunId != SubmitResponse.Substring(runIdStart, runIdEnd - runIdStart)) {
					success = true;
					break;
				}
				Thread.Sleep(200);
				challanged++;
			}
			/*提出結果の取得*/
			const string statusStartMark = "<status>\n";
			var statusStartIndex =
				SubmitResponse.IndexOf(statusStartMark,
					SubmitResponse.IndexOf(statusStartMark, StringComparison.Ordinal) + statusStartMark.Length,
					StringComparison.Ordinal) + statusStartMark.Length;
			var statusIdEndIndex = SubmitResponse.IndexOf("\n</status>", statusStartIndex, StringComparison.Ordinal);
			var submitResult = SubmitResponse.Substring(statusStartIndex, statusIdEndIndex - statusStartIndex);

			/*提出結果の表示*/
			string extension = @".txt";
			if (success) {
				MessageBox.Show(submitResult);
				if (submitResult == "Accepted") {

					switch (comboBox1.Text) {
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
					var sourceCode = new StreamWriter(@"AOJ-SourceCode\\Volume " + _problemNumber.Substring(0, _problemNumber.Length - 2) + @"\\" + _problemNumber + extension, false, Encoding.Default);
					sourceCode.Write(richTextBox1.Text);
					sourceCode.Close();
				}
			}
			else {
				MessageBox.Show(@"Submit Error!");
			}
			Close();
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e) {
			_notChanged = false;
		}
		private void button2_Click(object sender, EventArgs e) {
			Form2 configForm2 = new Form2();
			configForm2.Show();
		}

	}
}
