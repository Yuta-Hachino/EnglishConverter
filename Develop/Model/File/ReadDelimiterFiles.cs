using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuickStudyEnglish.Model.File
{
    public class ReadDelimiterFiles
    {
        public static List<string> ReadMasterData(string fileName) {
            List<string> lineData = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + ReadDelimiterFilesConfig.ROOT_DIR + fileName + ReadDelimiterFilesConfig.FILE_EXTENSION)) {
                while(sr.Peek() >= 0) {
                    lineData.Add(sr.ReadLine());
                }
            }
            return lineData;
        }
    }
}
