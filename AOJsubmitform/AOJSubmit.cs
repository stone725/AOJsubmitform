using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace AOJsubmitform
{
  public class AojSubmitException : Exception
  {
    public AojSubmitException() : base() { }
    public AojSubmitException(string message) : base(message) { }
    public AojSubmitException(string message, Exception innerException) : base(message, innerException) { }
  }

  public class AojSubmit
  {
    private readonly AojAccount _account;
    private readonly TimeSpan _timeout = new TimeSpan(0, 0, 10);
    private readonly TimeSpan maxWatingJudgeTime = new TimeSpan(0, 0, 20);
    private readonly Encoding _encoding = Encoding.GetEncoding("Shift_JIS");
    private readonly string _submitEndpoint = "http://judge.u-aizu.ac.jp/onlinejudge/servlet/Submit";
    private readonly string _responseEndpoint = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/status_log?user_id=";
    private readonly string _runIdStartMark = "<run_id>\n";
    private readonly string _statusStartMark = "<status>\n";

    private string ResponseUrl
    {
      get
      {
        return _responseEndpoint + _account.GetUserName();
      }
    }

    public AojSubmit(AojAccount aojAccount)
    {
      _account = aojAccount;
    }

    private byte[] BuildSubmitData(string problemNo, string language, string sourceCode)
    {
      Hashtable submitConfig = new Hashtable();
      submitConfig["userID"] = WebUtility.UrlEncode(_account.GetUserName());
      submitConfig["sourceCode"] = WebUtility.UrlEncode(sourceCode);
      submitConfig["problemNO"] = WebUtility.UrlEncode(problemNo);
      submitConfig["language"] = WebUtility.UrlEncode(language);
      submitConfig["password"] = WebUtility.UrlEncode(_account.GetUserPass());
      var submitParam = submitConfig.Keys.Cast<string>().Aggregate("", (current, key) => current + String.Format("{0}={1}&", key, submitConfig[key]));
      return _encoding.GetBytes(submitParam);
    }

    private HttpWebRequest BuildRequest(byte[] data)
    {
      HttpWebRequest request = WebRequest.Create(_submitEndpoint) as HttpWebRequest;

      request.Method = "POST";
      request.Timeout = (int)_timeout.TotalMilliseconds;
      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = data.Length;

      return request;
    }

    private string ExtractLastSubmitId(string submitResponse)
    {
      Int32 lastRunIdStartIndex, lastRunIdEndIndex;
      String lastRunId = "";
      if (submitResponse.IndexOf(_runIdStartMark, 0, StringComparison.Ordinal) != -1)
      {
        lastRunIdStartIndex = submitResponse.IndexOf(_runIdStartMark, 0, StringComparison.Ordinal) +
          _runIdStartMark.Length;
        lastRunIdEndIndex = submitResponse.IndexOf("\n", lastRunIdStartIndex, StringComparison.Ordinal);
        lastRunId = submitResponse.Substring(lastRunIdStartIndex, lastRunIdEndIndex - lastRunIdStartIndex);
      }
      return lastRunId;
    }

    private string ExtractLastSubmitResult(string submitResponse)
    {
      var start = submitResponse.IndexOf(_statusStartMark,
        submitResponse.IndexOf(_statusStartMark, StringComparison.Ordinal) + _statusStartMark.Length,
        StringComparison.Ordinal
      ) + _statusStartMark.Length;
      var end = submitResponse.IndexOf("\n</status>", start, StringComparison.Ordinal);
      return submitResponse.Substring(start, end - start);
    }

    private string GetSubmit()
    {
      var runIdRequest = WebRequest.Create(ResponseUrl) as HttpWebRequest;
      runIdRequest.Timeout = (int)_timeout.TotalMilliseconds;
      var lastrRunIdResstream = runIdRequest.GetResponse().GetResponseStream();
      using (var reader = new StreamReader(lastrRunIdResstream, _encoding))
      {
        return reader.ReadToEnd();
      }
    }

    private string GetLastRunId()
    {
      return ExtractLastSubmitId(GetSubmit());
    }

    private string GetJudgeResult(string lastRunId)
    {

      /*提出結果を格納する変数*/
      string submitResponse = "";
      int challanged = 0;
      bool success = false;

      //200ms * 100 = 20000ms→20sより100回試行
      while (challanged <= (maxWatingJudgeTime.TotalMilliseconds / 200))
      {
        submitResponse = GetSubmit();
        
        if (lastRunId != ExtractLastSubmitId(submitResponse))
        {
          success = true;
          break;
        }Thread.Sleep(200);
        challanged++;
      }
      if (!success)
      {
        throw new AojSubmitException();
      }

      return ExtractLastSubmitResult(GetSubmit());
    }

    public JudgeStatus Submit(string problemNo, string language, string sourceCode)
    {
      var data = BuildSubmitData(problemNo, language, sourceCode);
      var submitRequest = BuildRequest(data);

      
      var lastRunId = GetLastRunId();
      Stream submitReqStream = submitRequest.GetRequestStream();
      submitReqStream.Write(data, 0, data.Length);
      submitReqStream.Close();

      try
      {
        return JudgeStatusHelper.FromString(GetJudgeResult(lastRunId));
      }
      catch (Exception e)
      {
        throw new AojSubmitException("", e);
      }
    }
  }
}