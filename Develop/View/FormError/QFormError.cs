using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuickStudyEnglish.View.FormError
{
    public class QFormError
    {
        public enum ERROR_CLEAN_UP_TYPE
        {
            NONE,
            SHATDOWN,
            CONTINUE
        }

        /// <summary>
        /// エラーメッセージ表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public static void _ErrorOnControls(object sender, string message) {
            Color tempColor = Color.White;
            if (sender != null) {
                tempColor = (sender as Control).BackColor;
                (sender as Control).BackColor = Color.Yellow;
            }

            MessageBox.Show(message);
            if (sender != null) (sender as Control).BackColor = tempColor;
        }

        public static void OpenErrorMessage(string message, ERROR_CLEAN_UP_TYPE type) {
            MessageBox.Show(message);
            switch (type) {
                case ERROR_CLEAN_UP_TYPE.SHATDOWN:
                    ShatdownForm();
                    break;
            }
        }

        private static void ShatdownForm() {
            Application.ExitThread();
        }
    }
}
