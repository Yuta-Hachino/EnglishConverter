using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuickStudyEnglish.Model.Common;
using System.Collections.Generic;
using QuickStudyEnglish.View.UserControls;

namespace QuickStudyEnglish
{
    public partial class SelectDialog : Form
    {
        int retObject = -1;
        public SelectDialog() {
            InitializeComponent();
        }
        public SelectDialog(List<KeyValuePair<int, string>> buttons) {
            InitializeComponent();
                StartPosition = FormStartPosition.CenterParent;
            if(buttons == null) {
                this.Close();
            } else {
                buttons.ForEach(x => {
                    ButtonEx button = new ButtonEx();
                    button.Size = new Size(100, 50);
                    button.Name = x.Key.ToString();
                    button.Text = x.Value;
                    button.Click += button_Click;
                    flowLayoutPanel.Controls.Add(button);
                    if (flowLayoutPanel.Controls.Count % 4 == 0) flowLayoutPanel.SetFlowBreak(button, true);
                });
            }
        }

        private void button_Click(object sender, EventArgs e) {
            retObject = int.Parse((sender as ButtonEx).Name);
            this.Close();
        }
        private void SelectDialog_Load(object sender, EventArgs e) {
            if (DesignMode) return;

        }

        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e) {
            //this.Close();
            //e.Cancel = true;
        }
        public static int ShowSubForm(IWin32Window owner, List<KeyValuePair<int, string>> buttonNameList) {
            SelectDialog f = new SelectDialog(buttonNameList);
            f.ShowDialog(owner);
            f.Dispose();
            return f.retObject;
        }
    }
}
