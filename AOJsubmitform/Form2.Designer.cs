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
			this.OKButton.Location = new System.Drawing.Point(59, 59);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(72, 23);
			this.OKButton.TabIndex = 4;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButtonClick);
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(182, 87);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.PassWordBox);
			this.Controls.Add(this.UserNameBox);
			this.Controls.Add(this.PassWordLabel);
			this.Controls.Add(this.UserNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximumSize = new System.Drawing.Size(190, 121);
			this.MinimumSize = new System.Drawing.Size(190, 121);
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
	}
}