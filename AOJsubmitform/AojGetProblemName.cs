using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace AOJsubmitform
{
  public class AojGetProblemName
  {
    public string Getproblemname(string problemnunber)
    {
      string res = "";
      using (XmlReader reader = XmlReader.Create("http://judge.u-aizu.ac.jp/onlinejudge/webservice/problem?id=" + problemnunber + "&status=false"))
      {
        if (reader.ReadToFollowing("name"))
        {
          res = reader.ReadString().Replace("\n", "");
        }
      }
      return res;

    }
  }
}