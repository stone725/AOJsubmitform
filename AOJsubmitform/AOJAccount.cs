using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Collections;
using System.Windows.Forms;

namespace AOJsubmitform {
	public class AOJAccount
	{
		private readonly string userName;
		private readonly string userPass;

		public string getUserName()
		{
			return userName;
		}

		public string getUserPass()
		{
			return userPass;
		}

		public AOJAccount(string Name, string Pass)
		{
			userName = Name;
			userPass = Pass;
		}

	}
}
