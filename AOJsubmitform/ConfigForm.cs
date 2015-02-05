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
			if (MainForm.UserPassWord != "") {
				StreamWriter configFileWriter = new StreamWriter(@"Config.txt", false, Encoding.Default);
				configFileWriter.WriteLine(MainForm.UserName);
				configFileWriter.WriteLine(MainForm.UserPassWord);
				configFileWriter.WriteLine(MainForm.WriteDirectory);
				configFileWriter.Close();
			}
		}

		private void PassWordBoxChanged(object sender, EventArgs e) {
			MainForm.UserPassWord = PassWordBox.Text;
			if (MainForm.UserName != "") {
				StreamWriter configFileWriter = new StreamWriter(@"Config.txt", false, Encoding.Default);
				configFileWriter.WriteLine(MainForm.UserName);
				configFileWriter.WriteLine(MainForm.UserPassWord);
				configFileWriter.WriteLine(MainForm.WriteDirectory);
				configFileWriter.Close();
			}
		}

		

		private void DirectoryNameBoxChanged(object sender, EventArgs e)
		{
			MainForm.WriteDirectory = DirectoryNameBox.Text;
			if (MainForm.UserName != "" && MainForm.UserPassWord != "") {
				StreamWriter configFileWriter = new StreamWriter(@"Config.txt", false, Encoding.Default);
				configFileWriter.WriteLine(MainForm.UserName);
				configFileWriter.WriteLine(MainForm.UserPassWord);
				configFileWriter.WriteLine(MainForm.WriteDirectory);
				configFileWriter.Close();
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
