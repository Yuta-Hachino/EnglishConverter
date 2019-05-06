using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master
{
    public class EnglishWord
    {
        public int Id;
        public string Word;

        public EnglishWord(int _id, string _word) {
            this.Id = _id;
            this.Word = _word;
        }
    }

    public class EnglishWordMaster : BaseMaster
    {
        private static List<EnglishWord> englishWordMaster = new List<EnglishWord>();

        public string GetData(int _id) {
            return englishWordMaster[_id].Word;
        }

        public List<string> GetRangeData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach(int id in _idList) {
                rangeData.Add(GetData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            englishWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach(List<string> master in masterData) {
                englishWordMaster.Add(new EnglishWord(int.Parse(master[0]), master[1]));
            }
        }
    }
}
