using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class JapaneseEndingWord
    {
        public int Id;
        public string WordKanji;
        public string WordHiragana;
        public string WordFurigana;
        
        public JapaneseEndingWord(int _id, string _kanji, string _hiragana, string _furigana) {
            this.Id = _id;
            this.WordKanji = _kanji;
            this.WordHiragana = _hiragana;
            this.WordFurigana = _furigana;
        }
    }

    public class JapaneseEndingWordMaster : BaseMaster
    {
        private List<JapaneseEndingWord> japaneseEndingWordMaster = new List<JapaneseEndingWord>();

        public string GetKanjiData(int _id) {
            return japaneseEndingWordMaster[_id].WordKanji;
        }

        public string GetHiraganaData(int _id) {
            return japaneseEndingWordMaster[_id].WordHiragana;
        }

        public string GetFuriganaData(int _id) {
            return japaneseEndingWordMaster[_id].WordFurigana;
        }

        public List<string> GetRangeKanjiData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        public List<string> GetRangeHiraganaData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        public List<string> GetRangeFuriganaData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            japaneseEndingWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                japaneseEndingWordMaster.Add(new JapaneseEndingWord(int.Parse(master[0]), master[1], master[2], master[3]));
            }
        }
    }
}
