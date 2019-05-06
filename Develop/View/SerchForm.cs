using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuickStudyEnglish.Model.Common;

namespace QuickStudyEnglish
{
    public partial class SerchForm : Form
    {
        public SerchForm() {
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e) {
        }

        private void buttonEx1_Click(object sender, EventArgs e) {
            ColorDialog cd = new ColorDialog();
            cd.Color = Application.OpenForms[0].BackColor;

            if(cd.ShowDialog() == DialogResult.OK) {
                // 選択されている色情報を別ファイルに保存
                Application.OpenForms[0].BackColor = cd.Color;
                string path = Application.StartupPath + @"\option.conf";
                string[] strArray = File.ReadAllLines(path, System.Text.Encoding.UTF8);
                string result = "";
                for(int i = 0;i < strArray.Length; i++) {
                    if (strArray[i].Contains("color=")) {
                        strArray[i] = "color=" + ColorTranslator.ToWin32(cd.Color);
                    }
                    result += strArray[i] + Environment.NewLine;
                }
                File.WriteAllText(path, result);
            }
        }
        private void Search_FormClosing(object sender, FormClosingEventArgs e) {
            this.Hide();
            e.Cancel = true;
        }
        private void buttonEx2_Click(object sender, EventArgs e) {
            this.Hide();
        }

        private void buttonEx3_Click(object sender, EventArgs e) {
            // 選択されている色情報を別ファイルに保存
            Application.OpenForms[0].BackColor = this.BackColor;
            string path = Application.StartupPath + @"\option.conf";
            string[] strArray = File.ReadAllLines(path, System.Text.Encoding.UTF8);
            string result = "";
            for (int i = 0; i < strArray.Length; i++) {
                if (strArray[i].Contains("color=")) {
                    strArray[i] = "color=" + ColorTranslator.ToWin32(this.BackColor);
                }
                result += strArray[i] + Environment.NewLine;
            }
            File.WriteAllText(path, result);
        }

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show("工事中です。近日中に機能追加します。");
        }
    }
}
