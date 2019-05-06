using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Preposition
    {
        public int Id;
        public int ComboId;
        public int PrepositionId;

        public Preposition(int _id, int _comboId) {
            this.Id = _id;
            this.ComboId = _comboId;
        }
    }

    public class PrepositionMaster : BaseMaster
    {
        private static List<Preposition> prepositionMaster = new List<Preposition>();
        public List<string> GetPreposition(int _id, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetPrepositionKanji(_id);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetPrepositionHiragana(_id);
                default: return GetPrepositionFurigana(_id);
            }
        }
        public List<string> GetData(int _id, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetKanjiData(_id);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetHiraganaData(_id);
                default: return GetFuriganaData(_id);
            }
        }

        public List<List<string>> GetRangeData(List<int> _idList, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_idList);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_idList);
                default: return GetRangeFuriganaData(_idList);
            }
        }

        private List<string> GetPrepositionKanji(int _id) {
            return MasterFactory.GetMasterData<PrepositionMaster>().GetKanjiData(prepositionMaster[_id].PrepositionId);
        }

        private List<string> GetPrepositionHiragana(int _id) {
            return MasterFactory.GetMasterData<PrepositionMaster>().GetHiraganaData(prepositionMaster[_id].PrepositionId);
        }

        private List<string> GetPrepositionFurigana(int _id) {
            return MasterFactory.GetMasterData<PrepositionMaster>().GetFuriganaData(prepositionMaster[_id].PrepositionId);
        }

        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(prepositionMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(prepositionMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(prepositionMaster[_id].ComboId);
        }

        public List<int> GetPrepositionIdList() {
            return prepositionMaster.Select(n => n.Id).ToList();
        }

        public List<List<string>> GetRangeKanjiData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        public List<List<string>> GetRangeHiraganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        public List<List<string>> GetRangeFuriganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            prepositionMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                prepositionMaster.Add(new Preposition(int.Parse(master[0]), int.Parse(master[1])));
            }
        }
    }
}
