using System;
using System.IO;
using System.Net;
using System.Text;

namespace AOJsubmitform
{
	public class GetProblemName
	{
		public static string Getproblemname(string problemnunber)
		{
			string problemInfoUrl = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/problem?id=" + problemnunber + 
				"&status=false";
			HttpWebRequest problemInfoRequest = (HttpWebRequest)WebRequest.Create(problemInfoUrl);
			problemInfoRequest.Timeout = 1000000000;
			WebResponse problemInfoResponse = problemInfoRequest.GetResponse();
			Stream problemInfoResStream = problemInfoResponse.GetResponseStream();
			StreamReader problemInfoStreamReader = new StreamReader(problemInfoResStream, Encoding.Default);
			String problemInfoResString = problemInfoStreamReader.ReadToEnd();
			problemInfoStreamReader.Close();
			problemInfoResStream.Close();
			problemInfoResponse.Close();
			const string runIdStartMark = "<name>\n";
			int problemNameStartIndex = problemInfoResString.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) +
										runIdStartMark.Length;
			if (problemInfoResString.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) == -1)
			{
				return "";
			}
			int problemNamEndIndex = problemInfoResString.IndexOf("\n", problemNameStartIndex, StringComparison.Ordinal);
			return problemInfoResString.Substring(problemNameStartIndex, problemNamEndIndex - problemNameStartIndex);
		}
	}
}