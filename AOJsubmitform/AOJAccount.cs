namespace AOJsubmitform
{
  public class AojAccount
  {
    private readonly string userName;
    private readonly string userPass;

    public string GetUserName()
    {
      return userName;
    }

    public string GetUserPass()
    {
      return userPass;
    }

    public AojAccount(string name, string pass)
    {
      userName = name;
      userPass = pass;
    }

  }
}
