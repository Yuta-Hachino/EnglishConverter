using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace QuickStudyEnglish.Model.Common
{
    class ExceptionFilter
    {
        public ExceptionFilter() {

        }
        /// <summary>
        /// XML処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string XmlFilter(Exception ex,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "") {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            bool unexpected = true;
            if ((ex is ArgumentNullException) ||
                (ex is XmlException) ||
                (ex is ArgumentException) ||
                (ex is PathTooLongException) ||
                (ex is DirectoryNotFoundException) ||
                (ex is FileNotFoundException) ||
                (ex is IOException) ||
                (ex is UnauthorizedAccessException) ||
                (ex is NotSupportedException) ||
                (ex is System.Security.SecurityException)) {
            }   //	想定内の例外。
            MessageBox.Show(msg);
            return msg;
        }
        /// <summary>
        /// System.IOのファイル操作時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string FileIOFilter(Exception ex,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "") {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            if ((ex is DirectoryNotFoundException) ||
                (ex is FileNotFoundException) ||
                (ex is IOException) ||
                (ex is UnauthorizedAccessException) ||
                (ex is NotSupportedException) ||
                (ex is System.Security.SecurityException)) {
            }   //	想定内の例外。
            MessageBox.Show(msg);

            return msg;
        }
        /// <summary>
        /// リストや配列の処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string CollectionsFilter(Exception ex,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "") {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            if ((ex is IndexOutOfRangeException) ||
                (ex is KeyNotFoundException)) {

            }   //	想定内の例外。
            MessageBox.Show(msg);
            return msg;
        }
        /// <summary>
        /// スレッド処理時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string ThreadExceptionFilter(Exception ex,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "") {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            if ((ex is AggregateException)) {
                //	想定内の例外。
                throw (ex as AggregateException).Flatten();
            }
            MessageBox.Show(msg);
            return msg;
        }

        /// <summary>
        /// System.Diagnostics.Processクラス使用時の例外処理
        /// </summary>
        /// <param name="ex">例外</param>
        /// <param name="functionName">実行関数名</param>
        /// <returns>ログに出力される例外内容の写し</returns>
        public static string ProcessFilter(Exception ex,
                                            [CallerFilePath]string path = "",
                                            [CallerMemberName]string functionName = "") {
            string msg = $"{functionName},{ex.Message},{path},{ex.StackTrace}";
            if ((ex is Win32Exception) ||
                (ex is ObjectDisposedException) ||
                (ex is FileNotFoundException)) {
                
            }   //	想定内の例外。
            MessageBox.Show(msg);
            return msg;
        }

    }
}
