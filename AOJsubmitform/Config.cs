using System.IO;
using System.Text;

namespace AOJsubmitform
{
  public class Config
  {
    public string username_ { get; set; }
    public string password_ { get; set; }
    public string savedirectory_ { get; set; }
    public bool enableasciifilter_ { get; set; }
    public bool saveproblemname_ { get; set; }
    public bool tweetall_ { get; set; }
    public bool savefile_ { get; set; }

    public string twittertoken_ { get; set; }
    public string twittertokensecret_ { get; set; }

    public Config()
    {
      username_ = password_ = savedirectory_ = twittertoken_ = twittertokensecret_ = "";
    }

    public void SaveConfig()
    {
      File.WriteAllBytes("UserName.bin",           Encoding.Unicode.GetBytes(username_));
      File.WriteAllBytes("UserPassWord.bin",       Encoding.Unicode.GetBytes(password_));
      File.WriteAllBytes("AsciiFilter.bin",        Encoding.Unicode.GetBytes(enableasciifilter_ ? "enabled" : ""));
      File.WriteAllBytes("WriteDirectory.bin",     Encoding.Unicode.GetBytes(savedirectory_));
      File.WriteAllBytes("SaveFile.bin",           Encoding.Unicode.GetBytes(savefile_ ? "save" : ""));
      File.WriteAllBytes("SaveProblemName.bin",    Encoding.Unicode.GetBytes(saveproblemname_ ? "save" : ""));
      File.WriteAllBytes("TweetAll.bin",           Encoding.Unicode.GetBytes(tweetall_ ? "tweet" : ""));
      File.WriteAllBytes("TwitterToken.bin",       Encoding.Unicode.GetBytes(twittertoken_));
      File.WriteAllBytes("TwitterTokenSecret.bin", Encoding.Unicode.GetBytes(twittertokensecret_));
    }

	  private string LoadConfigFromNewFile(string path)
	  {
		  if (!File.Exists(path))
		  {
			  return "";
		  }
		  return Encoding.Unicode.GetString(File.ReadAllBytes(path));
	  }

	  private void LoadConfigFromOldFile()
	  {
		  if (File.Exists("Config.txt"))
		  {
			  using (StreamReader configFileReader = new StreamReader("Config.txt"))
			  {
				  username_ = configFileReader.ReadLine();
				  password_ = configFileReader.ReadLine();
				  savedirectory_ = configFileReader.ReadLine();
				  saveproblemname_ = configFileReader.ReadLine() == "save";
			  }
		  }

		  if (File.Exists("TwitterConfig.txt"))
		  {
			  using (StreamReader twitterConfigFileReader = new StreamReader("TwitterConfig.txt"))
			  {
				  twittertoken_ = twitterConfigFileReader.ReadLine();
				  twittertokensecret_ = twitterConfigFileReader.ReadLine();
				  twitterConfigFileReader.Close();
				  File.Delete("TwitterConfig.txt");
			  }
		  }

		  File.Delete("Config.txt");
			File.Delete("TwitterConfig.txt");
		}

    public void LoadConfig()
    {
			//新しい方式の設定記録ファイル（無意味なバイナリ式）から設定を呼び出す
			username_ = LoadConfigFromNewFile("UserName.bin");
	    password_ = LoadConfigFromNewFile("UserPassWord.bin");
	    enableasciifilter_ = LoadConfigFromNewFile("AsciiFilter.bin") == "enabled";
	    savedirectory_ = LoadConfigFromNewFile("WriteDirectory.bin");
	    savefile_ = !File.Exists("SaveFile.bin") || LoadConfigFromNewFile("SaveFile.bin") == "save";
	    saveproblemname_ = LoadConfigFromNewFile("SaveProblemName.bin") == "save";
	    tweetall_ = LoadConfigFromNewFile("TweetAll.bin") == "tweet";

	    twittertoken_ = LoadConfigFromNewFile("TwitterToken.bin");
	    twittertokensecret_ = LoadConfigFromNewFile("TwitterTokenSecret.bin");

			//旧形式の設定記録ファイルから設定を呼び出す
			LoadConfigFromOldFile();

			

	    //設定の記録(旧形式のファイルのデータはこの時新形式のファイルに変えられる)
	    SaveConfig();
		}
  }
}
