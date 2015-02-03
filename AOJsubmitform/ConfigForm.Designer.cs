namespace AOJsubmitform {
	partial class ConfigForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		public void InitializeComponent() {
			this.UserNameLabel = new System.Windows.Forms.Label();
			this.PassWordLabel = new System.Windows.Forms.Label();
			this.UserNameBox = new System.Windows.Forms.TextBox();
			this.PassWordBox = new System.Windows.Forms.TextBox();
			this.OKButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.DirectoryNameBox = new System.Windows.Forms.TextBox();
			this.twitterConfigButton = new System.Windows.Forms.Button();
			this.twitterConfigLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// UserNameLabel
			// 
			this.UserNameLabel.AutoSize = true;
			this.UserNameLabel.Location = new System.Drawing.Point(13, 13);
			this.UserNameLabel.Name = "UserNameLabel";
			this.UserNameLabel.Size = new System.Drawing.Size(57, 12);
			this.UserNameLabel.TabIndex = 0;
			this.UserNameLabel.Text = "ユーザー名";
			this.UserNameLabel.Click += new System.EventHandler(this.UserNameLabelClick);
			// 
			// PassWordLabel
			// 
			this.PassWordLabel.AutoSize = true;
			this.PassWordLabel.Location = new System.Drawing.Point(13, 37);
			this.PassWordLabel.Name = "PassWordLabel";
			this.PassWordLabel.Size = new System.Drawing.Size(52, 12);
			this.PassWordLabel.TabIndex = 1;
			this.PassWordLabel.Text = "パスワード";
			this.PassWordLabel.Click += new System.EventHandler(this.PassWordLabelClick);
			// 
			// UserNameBox
			// 
			this.UserNameBox.Location = new System.Drawing.Point(76, 10);
			this.UserNameBox.Name = "UserNameBox";
			this.UserNameBox.Size = new System.Drawing.Size(100, 19);
			this.UserNameBox.TabIndex = 2;
			this.UserNameBox.TextChanged += new System.EventHandler(this.UserNameBoxChanged);
			// 
			// PassWordBox
			// 
			this.PassWordBox.Location = new System.Drawing.Point(76, 34);
			this.PassWordBox.Name = "PassWordBox";
			this.PassWordBox.Size = new System.Drawing.Size(100, 19);
			this.PassWordBox.TabIndex = 3;
			this.PassWordBox.UseSystemPasswordChar = true;
			this.PassWordBox.TextChanged += new System.EventHandler(this.PassWordBoxChanged);
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(62, 121);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(72, 23);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OkButtonClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "保存フォルダ";
			// 
			// DirectoryNameBox
			// 
			this.DirectoryNameBox.Location = new System.Drawing.Point(76, 59);
			this.DirectoryNameBox.Name = "DirectoryNameBox";
			this.DirectoryNameBox.Size = new System.Drawing.Size(100, 19);
			this.DirectoryNameBox.TabIndex = 6;
			this.DirectoryNameBox.TextChanged += new System.EventHandler(this.DirectoryNameBoxChanged);
			// 
			// twitterConfigButton
			// 
			this.twitterConfigButton.Location = new System.Drawing.Point(101, 92);
			this.twitterConfigButton.Name = "twitterConfigButton";
			this.twitterConfigButton.Size = new System.Drawing.Size(75, 23);
			this.twitterConfigButton.TabIndex = 7;
			this.twitterConfigButton.Text = "認証";
			this.twitterConfigButton.UseVisualStyleBackColor = true;
			this.twitterConfigButton.Click += new System.EventHandler(this.twitterConfigButton_Click);
			// 
			// twitterConfigLabel
			// 
			this.twitterConfigLabel.AutoSize = true;
			this.twitterConfigLabel.Location = new System.Drawing.Point(6, 97);
			this.twitterConfigLabel.Name = "twitterConfigLabel";
			this.twitterConfigLabel.Size = new System.Drawing.Size(86, 12);
			this.twitterConfigLabel.TabIndex = 8;
			this.twitterConfigLabel.Text = "Twitter認証:なし";
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(202, 156);
			this.Controls.Add(this.twitterConfigLabel);
			this.Controls.Add(this.twitterConfigButton);
			this.Controls.Add(this.DirectoryNameBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.PassWordBox);
			this.Controls.Add(this.UserNameBox);
			this.Controls.Add(this.PassWordLabel);
			this.Controls.Add(this.UserNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximumSize = new System.Drawing.Size(210, 190);
			this.MinimumSize = new System.Drawing.Size(210, 190);
			this.Name = "ConfigForm";
			this.Text = "ConfigForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label UserNameLabel;
		private System.Windows.Forms.Label PassWordLabel;
		private System.Windows.Forms.TextBox UserNameBox;
		private System.Windows.Forms.TextBox PassWordBox;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox DirectoryNameBox;
		private System.Windows.Forms.Button twitterConfigButton;
		private System.Windows.Forms.Label twitterConfigLabel;
	}
}