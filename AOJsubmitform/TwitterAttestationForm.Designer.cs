namespace AOJsubmitform {
	partial class TwitterAttestationForm {
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
		private void InitializeComponent() {
			this.TwitterAttestationBrowser = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// TwitterAttestationBrowser
			// 
			this.TwitterAttestationBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TwitterAttestationBrowser.Location = new System.Drawing.Point(3, 3);
			this.TwitterAttestationBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.TwitterAttestationBrowser.Name = "TwitterAttestationBrowser";
			this.TwitterAttestationBrowser.Size = new System.Drawing.Size(471, 325);
			this.TwitterAttestationBrowser.TabIndex = 0;
			this.TwitterAttestationBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.DocumentCompleted);
			// 
			// TwitterAttestationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(472, 326);
			this.Controls.Add(this.TwitterAttestationBrowser);
			this.Location = new System.Drawing.Point(480, 360);
			this.MaximumSize = new System.Drawing.Size(480, 360);
			this.Name = "TwitterAttestationForm";
			this.Text = "Twitter認証";
			this.Load += new System.EventHandler(this.FormLoad);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser TwitterAttestationBrowser;
	}
}