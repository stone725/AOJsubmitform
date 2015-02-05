using System.Net;
using System.IO;
using System;
using System.Threading;
using System.Collections;
using System.Text;

namespace AOJsubmitform
{
	public class AOJSubmit
	{
		private readonly AOJAccount _account;

		public AOJSubmit(AOJAccount aojAccount)
		{
			_account = aojAccount;
		}



		public int Submit(string problemNO, string language, string sourceCode)
		{
			HttpWebRequest submitRequest =
				(HttpWebRequest) WebRequest.Create("http://judge.u-aizu.ac.jp/onlinejudge/servlet/Submit");
			Encoding enc = Encoding.GetEncoding("Shift_JIS");
			Hashtable submitConfig = new Hashtable();
			submitConfig["userID"] = WebUtility.UrlEncode(_account.getUserName());
			submitConfig["sourceCode"] = WebUtility.UrlEncode(sourceCode);
			submitConfig["problemNO"] = WebUtility.UrlEncode(problemNO);
			submitConfig["language"] = WebUtility.UrlEncode(language);
			submitConfig["password"] = WebUtility.UrlEncode(_account.getUserPass());
			submitRequest.Method = "POST";

			String submitParam = "";
			submitRequest.Timeout = 1000000000;
			foreach (String key in submitConfig.Keys)
			{
				submitParam += String.Format("{0}={1}&", key, submitConfig[key]);
			}
			Byte[] submitData = Encoding.GetEncoding("Shift_JIS").GetBytes(submitParam);
			submitRequest.ContentType = "application/x-www-form-urlencoded";
			submitRequest.ContentLength = submitData.Length;
			submitConfig.Clear();


			/*これから行う提出の一つ前の提出の提出番号を取得する*/
			String responseUrl = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/status_log?user_id=" + _account.getUserName();
			HttpWebRequest lastRunIdRequest = (HttpWebRequest) WebRequest.Create(responseUrl);
			lastRunIdRequest.Timeout = 1000000000;
			WebResponse lastRunIdresponse = lastRunIdRequest.GetResponse();
			Stream lastrRunIdResstream = lastRunIdresponse.GetResponseStream();
			StreamReader lastRunIdStreamReader = new StreamReader(lastrRunIdResstream, enc);
			String lastSubmitResponse = lastRunIdStreamReader.ReadToEnd();
			lastRunIdStreamReader.Close();
			lastrRunIdResstream.Close();
			lastRunIdresponse.Close();
			const string runIdStartMark = "<run_id>\n";
			Int32 lastRunIdStartIndex = lastSubmitResponse.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) +
			                            runIdStartMark.Length;
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

			//200ms * 100 = 20000ms→20sより100回試行
			while (challanged <= 100)
			{
				HttpWebRequest runIdRequest = (HttpWebRequest) WebRequest.Create(responseUrl);
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
				if (lastRunId != submitResponse.Substring(runIdStart, runIdEnd - runIdStart))
				{
					success = true;
					break;
				}
				Thread.Sleep(200);
				challanged++;
			}
			if (!success)
			{
				return -1;
			}
			/*提出結果の取得*/
			const string statusStartMark = "<status>\n";
			Int32 statusStartIndex =
				submitResponse.IndexOf(statusStartMark,
					submitResponse.IndexOf(statusStartMark, StringComparison.Ordinal) + statusStartMark.Length,
					StringComparison.Ordinal) + statusStartMark.Length;
			Int32 statusIdEndIndex = submitResponse.IndexOf("\n</status>", statusStartIndex, StringComparison.Ordinal);
			String submitResult = submitResponse.Substring(statusStartIndex, statusIdEndIndex - statusStartIndex);

			/*提出結果の表示*/
			switch (submitResult)
			{
				case "Accepted":
					return 0;
				case "Partial Points":
					return 1;
				case "Wrong Answer":
					return 2;
				case "Runtime Error":
					return 3;
				case "Time Limit Exceeded":
					return 4;
				case "Memory Limit Exceeded":
					return 5;
				case "Compile Error":
					return 6;
				case "Presentation Error":
					return 7;
				case "Output Limit Exceeded":
					return 8;
				case "Judge Not Available":
					return 9;
			}
			return -1;
		}
	}
}