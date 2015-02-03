namespace AOJsubmitform {
	partial class MainForm {
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.ProblemNumberBox = new System.Windows.Forms.TextBox();
			this.SourceCodeBox = new System.Windows.Forms.RichTextBox();
			this.LanguageBox = new System.Windows.Forms.ComboBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ProblemNumberBox
			// 
			this.ProblemNumberBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ProblemNumberBox.Location = new System.Drawing.Point(12, 12);
			this.ProblemNumberBox.Name = "ProblemNumberBox";
			this.ProblemNumberBox.Size = new System.Drawing.Size(100, 19);
			this.ProblemNumberBox.TabIndex = 0;
			this.ProblemNumberBox.TextChanged += new System.EventHandler(this.ProblemNumberBoxChanged);
			// 
			// SourceCodeBox
			// 
			this.SourceCodeBox.AcceptsTab = true;
			this.SourceCodeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.SourceCodeBox.Location = new System.Drawing.Point(13, 38);
			this.SourceCodeBox.Name = "SourceCodeBox";
			this.SourceCodeBox.Size = new System.Drawing.Size(599, 391);
			this.SourceCodeBox.TabIndex = 1;
			this.SourceCodeBox.Text = "";
			this.SourceCodeBox.TextChanged += new System.EventHandler(this.SourceCodeChanged);
			// 
			// LanguageBox
			// 
			this.LanguageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.LanguageBox.FormattingEnabled = true;
			this.LanguageBox.Items.AddRange(new object[] {
			"C++",
			"C#",
			"C",
			"JAVA",
			"C++11",
			"D",
			"Ruby",
			"Python",
			"Python3",
			"PHP",
			"JavaScript"});
			this.LanguageBox.Location = new System.Drawing.Point(118, 12);
			this.LanguageBox.Name = "LanguageBox";
			this.LanguageBox.Size = new System.Drawing.Size(121, 20);
			this.LanguageBox.TabIndex = 2;
			this.LanguageBox.TabStop = false;
			this.LanguageBox.Text = "C++";
			this.LanguageBox.SelectedIndexChanged += new System.EventHandler(this.LanguageboxChanged);
			// 
			// SubmitButton
			// 
			this.SubmitButton.Location = new System.Drawing.Point(246, 9);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 23);
			this.SubmitButton.TabIndex = 3;
			this.SubmitButton.Text = "Submit!";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButtonClick);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(537, 9);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Config";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.ConfigButtonClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 441);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.SubmitButton);
			this.Controls.Add(this.LanguageBox);
			this.Controls.Add(this.SourceCodeBox);
			this.Controls.Add(this.ProblemNumberBox);
			this.Name = "MainForm";
			this.Text = "Submit Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ProblemNumberBox;
		private System.Windows.Forms.RichTextBox SourceCodeBox;
		private System.Windows.Forms.ComboBox LanguageBox;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Button button2;
	}
}

