using System;
using System.Windows.Forms;

namespace AOJsubmitform
{
  public partial class ConfigForm : Form
  {
    private Config config;

    public ConfigForm(Config config)
    {
      InitializeComponent();
      this.config = config;
      Initialize();
    }

    private void Initialize()
    {
      UserNameBox.Text = config.username_;
      PassWordBox.Text = config.password_;
      DirectoryNameBox.Text = config.savedirectory_;
      asciiFilterCheckBox.Checked = config.saveproblemname_;
      TweetAllCheckBox.Checked = config.tweetall_;
      SaveFileCheckBox.Checked = config.savefile_;
    }

    private void Save()
    {
      config.username_ = UserNameBox.Text;
      config.password_ = PassWordBox.Text;
      config.enableasciifilter_ = asciiFilterCheckBox.Checked;
      config.savedirectory_ = DirectoryNameBox.Text;
      config.savefile_ = SaveFileCheckBox.Checked;
      config.saveproblemname_ = asciiFilterCheckBox.Checked;
      config.tweetall_ = TweetAllCheckBox.Checked;
      config.SaveConfig();
    }

    private void twitterConfigButton_Click(object sender, EventArgs e)
    {
      TwitterAttestationForm twitterAttestationForm = new TwitterAttestationForm(config);
      twitterAttestationForm.Show();
    }

    private void OkButtonClick(object sender, EventArgs e)
    {
      Save();
      Close();
    }
  }
}
