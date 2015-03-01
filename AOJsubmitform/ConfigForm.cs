using System;
using System.Windows.Forms;

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
			saveCheckBox.Checked = MainForm.SaveProblemName;
			TweetAllCheckBox.Checked = MainForm.TweetAll;
		}

		private void UserNameLabelClick(object sender, EventArgs e) {

		}

		private void PassWordLabelClick(object sender, EventArgs e) {

		}

		private void UserNameBoxChanged(object sender, EventArgs e) {
			MainForm.UserName = UserNameBox.Text;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory, MainForm.SaveProblemName, MainForm.TweetAll);
		}

		private void PassWordBoxChanged(object sender, EventArgs e) {
			MainForm.UserPassWord = PassWordBox.Text;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory, MainForm.SaveProblemName, MainForm.TweetAll);
		}

		

		private void DirectoryNameBoxChanged(object sender, EventArgs e)
		{
			MainForm.WriteDirectory = DirectoryNameBox.Text;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory, MainForm.SaveProblemName, MainForm.TweetAll);
		}

		private void OkButtonClick(object sender, EventArgs e) {
			Close();
		}

		private void twitterConfigButton_Click(object sender, EventArgs e)
		{	
			TwitterAttestationForm twitterAttestationForm = new TwitterAttestationForm();
			twitterAttestationForm.Show();
		}

		private void SaveProblemNameChanged(object sender, EventArgs e)
		{
			MainForm.SaveProblemName = saveCheckBox.Checked;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory, MainForm.SaveProblemName, MainForm.TweetAll);
		}

		private void TweetAllCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			MainForm.TweetAll = TweetAllCheckBox.Checked;
			ConfigWriter configWriter = new ConfigWriter();
			configWriter.ConfigWrite(MainForm.UserName, MainForm.UserPassWord, MainForm.WriteDirectory, MainForm.SaveProblemName, MainForm.TweetAll);
		}
	}
}
