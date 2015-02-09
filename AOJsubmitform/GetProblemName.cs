using System;
using System.Net;
using System.IO;
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
			Int32 problemNameStartIndex = problemInfoResString.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) +
										runIdStartMark.Length;
			if (problemNameStartIndex == -1)
			{
				return "";
			}
			Int32 problemNamEndIndex = problemInfoResString.IndexOf("\n", problemNameStartIndex, StringComparison.Ordinal);
			return problemInfoResString.Substring(problemNameStartIndex, problemNamEndIndex - problemNameStartIndex);
		}
	}
}