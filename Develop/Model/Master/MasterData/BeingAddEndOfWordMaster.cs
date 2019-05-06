using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class BeingAddWord
    {
        public int Id;
        public int BeingWordId;
        public int ComboId;

        public BeingAddWord(int _id, int _endOfId, int _comboId) {
            this.Id = _id;
            this.BeingWordId = _endOfId;
            this.ComboId = _comboId;
        }
    }

    public class BeingAddWordMaster : BaseMaster
    {
        private List<BeingAddWord> beingAddWordMaster = new List<BeingAddWord>();
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
        public List<List<string>> GetRangeData(int _endOfWordId, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_endOfWordId);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_endOfWordId);
                default: return GetRangeFuriganaData(_endOfWordId);
            }
        }
        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(beingAddWordMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(beingAddWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(beingAddWordMaster[_id].ComboId);
        }

        private List<List<string>> GetRangeKanjiData(int _endOfWordId) {
            List<int> comboIdList = beingAddWordMaster.Where(n => n.BeingWordId == _endOfWordId).Select(n => n.Id).ToList();
            return GetRangeKanjiData(comboIdList);
        }

        private List<List<string>> GetRangeHiraganaData(int _endOfWordId) {
            List<int> comboIdList = beingAddWordMaster.Where(n => n.BeingWordId == _endOfWordId).Select(n => n.Id).ToList();
            return GetRangeHiraganaData(comboIdList);
        }

        private List<List<string>> GetRangeFuriganaData(int _endOfWordId) {
            List<int> comboIdList = beingAddWordMaster.Where(n => n.BeingWordId == _endOfWordId).Select(n => n.Id).ToList();
            return GetRangeFuriganaData(comboIdList);
        }

        private List<List<string>> GetRangeKanjiData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeHiraganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeFuriganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            beingAddWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                beingAddWordMaster.Add(new BeingAddWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
