using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AOJsubmitform {
	public partial class ConfigForm : Form {
		public ConfigForm() {
			InitializeComponent();
			if (SubmitForm.UserName != "") {
				UserNameBox.Text = SubmitForm.UserName;
			}
			if (SubmitForm.UserPassWord != "") {
				PassWordBox.Text = SubmitForm.UserPassWord;
			}
		}

		private void UserNameLabelClick(object sender, EventArgs e) {

		}

		private void PassWordLabelClick(object sender, EventArgs e) {

		}

		private void UserNameBoxChanged(object sender, EventArgs e) {
			SubmitForm.UserName = UserNameBox.Text;
			if (SubmitForm.UserPassWord != "") {
				StreamWriter configFileWriter = new StreamWriter(@"Config.txt", false, Encoding.Default);
				configFileWriter.WriteLine(SubmitForm.UserName);
				configFileWriter.WriteLine(SubmitForm.UserPassWord);
				configFileWriter.Close();
			}
		}

		private void PassWordBoxChanged(object sender, EventArgs e) {
			SubmitForm.UserPassWord = PassWordBox.Text;
			if (SubmitForm.UserName != "") {
				StreamWriter configFileWriter = new StreamWriter(@"Config.txt", false, Encoding.Default);
				configFileWriter.WriteLine(SubmitForm.UserName);
				configFileWriter.WriteLine(SubmitForm.UserPassWord);
				configFileWriter.Close();
			}
		}

		private void OkButtonClick(object sender, EventArgs e) {
			Close();
		}

	}
}
