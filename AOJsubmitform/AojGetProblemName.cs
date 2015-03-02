using System;
using System.IO;
using System.Net;
using System.Text;

namespace AOJsubmitform
{
  public class AojGetProblemName
  {
    private string ExtractProblemName(string responseString)
    {
      const string runIdStartMark = "<name>\n";
      int problemNameStartIndex = responseString.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) +
                    runIdStartMark.Length;
      if (responseString.IndexOf(runIdStartMark, 0, StringComparison.Ordinal) == -1)
      {
        return "";
      }
      int problemNamEndIndex = responseString.IndexOf("\n", problemNameStartIndex, StringComparison.Ordinal);
      return responseString.Substring(problemNameStartIndex, problemNamEndIndex - problemNameStartIndex);
    }

    public string Getproblemname(string problemnunber)
    {
      string problemInfoUrl = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/problem?id=" + problemnunber +
        "&status=false";
      var request = WebRequest.Create(problemInfoUrl) as HttpWebRequest;
      request.Timeout = 1000000000;

      string responseString;
      using (var reader = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.Default))
      {
        responseString = reader.ReadToEnd();
      }
      return ExtractProblemName(responseString);

    }
  }
}