using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AOJsubmitform {
	public partial class ConfigForm : Form {
		public ConfigForm() {
			InitializeComponent();
			if (MainForm.UserName != "") {
				UserNameBox.Text = MainForm.UserName;
			}
			if (MainForm.UserPassWord != "") {
				PassWordBox.Text = MainForm.UserPassWord;
			}
			if (MainForm.WriteDirectory != "")
			{
				DirectoryNameBox.Text = MainForm.WriteDirectory;
			}
		}

		private void UserNameLabelClick(object sender, EventArgs e) {

		}

		private void PassWordLabelClick(object sender, EventArgs e) {

		}

		private void UserNameBoxChanged(object sender, EventArgs e) {
			MainForm.UserName = UserNameBox.Text;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory);
		}

		private void PassWordBoxChanged(object sender, EventArgs e) {
			MainForm.UserPassWord = PassWordBox.Text;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory);
		}

		

		private void DirectoryNameBoxChanged(object sender, EventArgs e)
		{
			MainForm.WriteDirectory = DirectoryNameBox.Text;
			if (MainForm.UserName != "" && MainForm.UserPassWord != "") {
				ConfigWriter configWriter = new ConfigWriter();
				configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory);
			}
		}

		private void OkButtonClick(object sender, EventArgs e) {
			Close();
		}

		private void twitterConfigButton_Click(object sender, EventArgs e)
		{	
			TwitterAttestationForm twitterAttestationForm = new TwitterAttestationForm();
			twitterAttestationForm.Show();
		}
	}
}
