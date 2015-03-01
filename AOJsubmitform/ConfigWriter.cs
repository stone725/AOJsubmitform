using System.IO;
using System.Text;

namespace AOJsubmitform {
	class ConfigWriter {
		public void ConfigWrite(string username, string userpassword, string writedirectory, bool saveproblemname, bool tweetall)
		{
			File.WriteAllBytes("UserName.bin", Encoding.Unicode.GetBytes(username));
			File.WriteAllBytes("UserPassWord.bin", Encoding.Unicode.GetBytes(userpassword));
			File.WriteAllBytes("WriteDirectory.bin", Encoding.Unicode.GetBytes(writedirectory));
			File.WriteAllBytes("SaveProblemName.bin", Encoding.Unicode.GetBytes(saveproblemname ? "save" : ""));
			File.WriteAllBytes("TweetAll.bin", Encoding.Unicode.GetBytes(tweetall ? "tweet" : ""));
		}

		public void TwitterConfigWrite(string twitterToken, string twitterTokenSecret)
		{
			File.WriteAllBytes("TwitterToken.bin", Encoding.Unicode.GetBytes(twitterToken));
			File.WriteAllBytes("TwitterTokenSecret.bin", Encoding.Unicode.GetBytes(twitterTokenSecret)); 
		}
	}
}
