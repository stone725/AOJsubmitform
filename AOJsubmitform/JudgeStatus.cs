using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOJsubmitform
{
  public enum JudgeStatus
  {
    WaitingJudge = 0,
    Accepted,
    ParialPoints,
    WrongAnswer,
    RuntimeError,
    Timelimitexceeded,
    MemoryLimitExceeded,
    CompileError,
    PresentationError,
    OutputLimitExceeded,
    JudgeNotAvailable,
  }

  public static class JudgeStatusHelper
  {
    private static Dictionary<JudgeStatus, string> toStringTable = new Dictionary<JudgeStatus,string>();
    private static Dictionary<JudgeStatus, string> toAbbreviationTable = new Dictionary<JudgeStatus, string>();
    private static Dictionary<string, JudgeStatus> fromStringTable = new Dictionary<string,JudgeStatus>();

    static JudgeStatusHelper()
    {
      foreach(var value in Enum.GetValues(typeof(JudgeStatus)))
      {
        var name = Enum.GetName(typeof(JudgeStatus), value);
        var sb = new StringBuilder();
        var abbrSb = new StringBuilder();
        foreach(var c in name)
        {
          if (char.IsUpper(c))
          {
            sb.Append(" ");
            abbrSb.Append(c);
          }
          sb.Append(c);
        }
        var str = sb.ToString().Substring(1); // sb[0] is " "
        toStringTable.Add((JudgeStatus)value, str);
        toAbbreviationTable.Add((JudgeStatus)value, abbrSb.ToString());
        fromStringTable.Add(str, (JudgeStatus)value);
      }
      // AOJ server will return "WA: Presentation Error" not "Presentation Error"
      var pe = toStringTable[JudgeStatus.PresentationError];
      fromStringTable["WA: " + pe] = JudgeStatus.PresentationError;

      // Abbreviation of Accepted is AC
      toAbbreviationTable[JudgeStatus.Accepted] = "AC";
    }

    public static string ToDisplayString(this JudgeStatus status)
    {
      if(toStringTable.ContainsKey(status))
        return toStringTable[status];
      throw new ArgumentException();
    }

    public static string ToAbbreviation(this JudgeStatus status)
    {
      if (toStringTable.ContainsKey(status))
        return toAbbreviationTable[status];
      throw new ArgumentException();
    }

    public static JudgeStatus FromString(string str)
    {
      if (fromStringTable.ContainsKey(str))
        return fromStringTable[str];
      throw new ArgumentException(str + " is invalid status");
    }
  }
}
