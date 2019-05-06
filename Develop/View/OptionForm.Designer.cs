namespace QuickStudyEnglish
{
    partial class OptionForm
    {
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
            this.buttonEx1 = new QuickStudyEnglish.View.UserControls.ButtonEx();
            this.buttonEx2 = new QuickStudyEnglish.View.UserControls.ButtonEx();
            this.buttonEx3 = new QuickStudyEnglish.View.UserControls.ButtonEx();
            this.SuspendLayout();
            // 
            // buttonEx1
            // 
            this.buttonEx1.Location = new System.Drawing.Point(12, 12);
            this.buttonEx1.Name = "buttonEx1";
            this.buttonEx1.Selectable = true;
            this.buttonEx1.Size = new System.Drawing.Size(156, 30);
            this.buttonEx1.TabIndex = 0;
            this.buttonEx1.Text = "背景色変更";
            this.buttonEx1.UseVisualStyleBackColor = true;
            this.buttonEx1.Click += new System.EventHandler(this.buttonEx1_Click);
            // 
            // buttonEx2
            // 
            this.buttonEx2.Location = new System.Drawing.Point(102, 138);
            this.buttonEx2.Name = "buttonEx2";
            this.buttonEx2.Selectable = true;
            this.buttonEx2.Size = new System.Drawing.Size(66, 40);
            this.buttonEx2.TabIndex = 1;
            this.buttonEx2.Text = "適用";
            this.buttonEx2.UseVisualStyleBackColor = true;
            this.buttonEx2.Click += new System.EventHandler(this.buttonEx2_Click);
            // 
            // buttonEx3
            // 
            this.buttonEx3.Location = new System.Drawing.Point(12, 138);
            this.buttonEx3.Name = "buttonEx3";
            this.buttonEx3.Selectable = true;
            this.buttonEx3.Size = new System.Drawing.Size(65, 40);
            this.buttonEx3.TabIndex = 2;
            this.buttonEx3.Text = "初期化";
            this.buttonEx3.UseVisualStyleBackColor = true;
            this.buttonEx3.Click += new System.EventHandler(this.buttonEx3_Click);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 193);
            this.Controls.Add(this.buttonEx3);
            this.Controls.Add(this.buttonEx2);
            this.Controls.Add(this.buttonEx1);
            this.Name = "OptionForm";
            this.Text = "Option";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionForm_FormClosing);
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private View.UserControls.ButtonEx buttonEx1;
        private View.UserControls.ButtonEx buttonEx2;
        private View.UserControls.ButtonEx buttonEx3;
    }
}