using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.View.Containers
{
    class OutPutTextButton
    {



        //private void _OutPutTextBtn_Click(object sender, EventArgs e) {
        //    List<string> outputTextLines = new List<string>();
        //    if (_OutputConvertedTextBox.Lines.Length == 0
        //        || _OutputSourceTextBox.Lines.Length == 0) {
        //        _ErrorOnControls(sender, "出力するテキストデータがありません。「確定」ボタンを押して文章を追加してください。");
        //        return;
        //    }
        //    //保存するテキスト
        //    int convertedLen = _OutputConvertedTextBox.Lines.Length;
        //    int sourceLen = _OutputSourceTextBox.Lines.Length;
        //    for (int ct = 0; ct < convertedLen || ct < sourceLen; ct++) {
        //        string outputTempText = ((ct < convertedLen) ? _OutputConvertedTextBox.Lines[ct] : "")
        //            + "\t" + "\t" + "\t"
        //            + ((ct < sourceLen) ? _OutputSourceTextBox.Lines[ct] : "");
        //        outputTextLines.Add(outputTempText);
        //    }

        //    using (SaveFileDialog saveDir = new SaveFileDialog()) {
        //        //はじめのファイル名を指定する
        //        //はじめに「ファイル名」で表示される文字列を指定する
        //        saveDir.FileName = "新しいファイル.txt";
        //        //はじめに表示されるフォルダを指定する
        //        saveDir.InitialDirectory = @"C:\";
        //        //[ファイルの種類]に表示される選択肢を指定する
        //        //指定しない（空の文字列）の時は、現在のディレクトリが表示される
        //        saveDir.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
        //        //[ファイルの種類]ではじめに選択されるものを指定する
        //        //2番目の「すべてのファイル」が選択されているようにする
        //        saveDir.FilterIndex = 1;
        //        //タイトルを設定する
        //        saveDir.Title = "保存先のファイルを選択してください";
        //        //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        //        saveDir.RestoreDirectory = true;
        //        //既に存在するファイル名を指定したとき警告する
        //        //デフォルトでTrueなので指定する必要はない
        //        saveDir.OverwritePrompt = true;
        //        //存在しないパスが指定されたとき警告を表示する
        //        //デフォルトでTrueなので指定する必要はない
        //        saveDir.CheckPathExists = true;
        //        if (saveDir.ShowDialog() == DialogResult.OK) {
        //            using (StreamWriter sw = new StreamWriter(saveDir.FileName)) {
        //                foreach (string line in outputTextLines) {
        //                    sw.WriteLine(line);
        //                }
        //            }
        //        }
        //    }

        //}
    }
}
