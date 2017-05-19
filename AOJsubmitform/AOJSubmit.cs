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
    private readonly AojAccount account_;
    private readonly TimeSpan timeout_ = new TimeSpan(0, 0, 10);
    private readonly TimeSpan maxwatingjudgetime_ = new TimeSpan(0, 0, 20);
    private readonly Encoding encoding_ = Encoding.GetEncoding("Shift_JIS");
    private readonly string submitendpoint_ = "http://judge.u-aizu.ac.jp/onlinejudge/servlet/Submit";
    private readonly string responseendpoint_ = "http://judge.u-aizu.ac.jp/onlinejudge/webservice/status_log?user_id=";

    private string responseurl_
    {
      get
      {
        return responseendpoint_ + account_.GetUserName();
      }
    }

    public AojSubmit(AojAccount aojAccount)
    {
      account_ = aojAccount;
    }

    private byte[] BuildSubmitData(string problemNo, string language, string sourceCode)
    {
      Hashtable submitConfig = new Hashtable
      {
        ["userID"    ] = WebUtility.UrlEncode(account_.GetUserName()),
        ["sourceCode"] = WebUtility.UrlEncode(sourceCode),
        ["problemNO" ] = WebUtility.UrlEncode(problemNo),
        ["language"  ] = WebUtility.UrlEncode(language),
        ["password"  ] = WebUtility.UrlEncode(account_.GetUserPass())
      };


      var submitParam = submitConfig.Keys.Cast<string>().Aggregate("", (current, key) => current + string.Format("{0}={1}&", key, submitConfig[key]));

			return encoding_.GetBytes(submitParam);
    }

    private HttpWebRequest BuildRequest(byte[] data)
    {
      HttpWebRequest request = WebRequest.Create(submitendpoint_) as HttpWebRequest;

      request.Method        = "POST";
      request.Timeout       = (int)timeout_.TotalMilliseconds;
      request.ContentType   = "application/x-www-form-urlencoded";
      request.ContentLength = data.Length;

      return request;
    }

    private string GetResult()
    {
      string res = "";
      using (XmlReader reader = XmlReader.Create(responseurl_))
      {
        if (reader.ReadToFollowing("status") && reader.ReadToFollowing("status"))
        {
          res = reader.ReadString().Replace("\n", "");
        }
      }
      return res;
    }

    //ユーザーの直近の提出IDを取得
    private string GetLastRunId()
    {
      string res = "";
      using (XmlReader reader = XmlReader.Create(responseurl_))
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
			int requestCount = 0;

	    //200ms * 100 = 20000ms→20sより100回試行
      while (requestCount <= (maxwatingjudgetime_.TotalMilliseconds / 200))
      {
        if (lastRunId != GetLastRunId())
        {
	        return GetResult();
        }
        Thread.Sleep(200);
        requestCount++;
      }

      throw new AojSubmitException();
    }

    public JudgeStatus Submit(string problemNo, string language, string sourceCode)
    {
      var data          = BuildSubmitData(problemNo, language, sourceCode);
      var submitRequest = BuildRequest(data);

      
      var lastRunId = GetLastRunId();
      using (Stream submitReqStream = submitRequest.GetRequestStream())
      {
        submitReqStream.Write(data, 0, data.Length);
      }

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