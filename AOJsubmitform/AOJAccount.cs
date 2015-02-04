//AOJのアカウント情報を取り扱う
namespace AOJsubmitform {
	public class AojAccount
	{
		private readonly string _userName;
		private readonly string _userPass;

		//ユーザー名を返す関数
		public string GetUserName()
		{
			return _userName;
		}

		//ユーザーパスワードを返す関数
		public string GetUserPass()
		{
			return _userPass;
		}

		//アカウント情報の登録
		public AojAccount(string name, string pass)
		{
			_userName = name;
			_userPass = pass;
		}

	}
}
