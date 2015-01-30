using System;
using System.Windows.Forms;

namespace AOJsubmitform {
	static class Program
	{
		public static string FileName = "";
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			if (args.Length != 0)
			{
				FileName = args[0];
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SubmitForm());
		}
	}
}
