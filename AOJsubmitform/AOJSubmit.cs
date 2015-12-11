using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

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

    private string GetResult()
    {
      string res = "";
      using (XmlReader reader = XmlReader.Create(ResponseUrl))
      {
        if (reader.ReadToFollowing("status") && reader.ReadToFollowing("status"))
        {
          res = reader.ReadString().Replace("\n", "");
        }
      }
      return res;
    }

    private string GetLastRunId()
    {
      string res = "";
      using (XmlReader reader = XmlReader.Create(ResponseUrl))
      {
        if (reader.ReadToFollowing("run_id"))
        {
          res = reader.ReadString().Replace("\n", "");
        }
      }
      return res;
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
        if (lastRunId != GetLastRunId())
        {
          success = true;
          break;
        }
        Thread.Sleep(200);
        challanged++;
      }
      if (!success)
      {
        throw new AojSubmitException();
      }

      return GetResult();
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