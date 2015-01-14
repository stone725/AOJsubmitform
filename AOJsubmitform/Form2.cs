using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AOJsubmitform {
	public partial class Form2 : Form {
		public Form2() {
			InitializeComponent();
			if (Form1._userName != "") {
				textBox1.Text = Form1._userName;
			}
			if (Form1._userPassWord != "") {
				textBox2.Text = Form1._userPassWord;
			}
		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void label2_Click(object sender, EventArgs e) {

		}

		private void textBox1_TextChanged(object sender, EventArgs e) {
			Form1._userName = textBox1.Text;
			if (Form1._userPassWord != "") {
				var writeFile = new StreamWriter(@"Config.txt", false, Encoding.Default);
				writeFile.WriteLine(Form1._userName);
				writeFile.WriteLine(Form1._userPassWord);
				writeFile.Close();
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e) {
			Form1._userPassWord = textBox2.Text;
			if (Form1._userName != "") {
				var writeFile = new StreamWriter(@"Config.txt", false, Encoding.Default);
				writeFile.WriteLine(Form1._userName);
				writeFile.WriteLine(Form1._userPassWord);
				writeFile.Close();
			}
		}

	}
}
