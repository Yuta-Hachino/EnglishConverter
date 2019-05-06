using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class VerbAddEndOfWord
    {
        public int Id;
        public int VerbEndOfWordId;
        public int ComboId;

        public VerbAddEndOfWord(int _id, int _endOfId, int _comboId) {
            this.Id = _id;
            this.VerbEndOfWordId = _endOfId;
            this.ComboId = _comboId;
        }
    }

    public class VerbAddEndOfWordMaster : BaseMaster
    {
        private List<VerbAddEndOfWord> verbAddEndOfWordMaster = new List<VerbAddEndOfWord>();
        public List<string> GetData(int _id, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetKanjiData(_id);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetHiraganaData(_id);
                default: return GetFuriganaData(_id);
            }
        }
        public List<string> GetGroupByGroupIdData(int _id, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetGroupByGroupIdKanjiData(_id);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetGroupByGroupIdHiraganaData(_id);
                default: return GetGroupByGroupIdFuriganaData(_id);
            }
        }
        public List<List<string>> GetRangeData(List<int> _idList, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_idList);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_idList);
                default: return GetRangeFuriganaData(_idList);
            }
        }
        public List<List<string>> GetRangeData(int _endOfWordId, int _numberOfPeopleId, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_endOfWordId, _numberOfPeopleId);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_endOfWordId, _numberOfPeopleId);
                default: return GetRangeFuriganaData(_endOfWordId, _numberOfPeopleId);
            }
        }

        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(verbAddEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(verbAddEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(verbAddEndOfWordMaster[_id].ComboId);
        }

        private List<List<string>> GetRangeKanjiData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = verbAddEndOfWordMaster.Where(n => n.VerbEndOfWordId == _endOfWordId).Select(n => n.ComboId).ToList();
            return GetRangeKanjiData(comboIdList);
        }

        private List<List<string>> GetRangeHiraganaData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = verbAddEndOfWordMaster.Where(n => n.VerbEndOfWordId == _endOfWordId).Select(n => n.ComboId).ToList();
            return GetRangeHiraganaData(comboIdList);
        }

        private List<List<string>> GetRangeFuriganaData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = verbAddEndOfWordMaster.Where(n => n.VerbEndOfWordId == _endOfWordId).Select(n => n.ComboId).ToList();
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

        private List<string> GetGroupByGroupIdKanjiData(int _endOfWordId) {
            object masterdata = verbAddEndOfWordMaster.FirstOrDefault(n => n.VerbEndOfWordId == _endOfWordId);
            return GetKanjiData((masterdata == null)? 0 : ((VerbAddEndOfWord)(masterdata)).Id);
        }

        private List<string> GetGroupByGroupIdHiraganaData(int _endOfWordId) {
            object masterdata = verbAddEndOfWordMaster.FirstOrDefault(n => n.VerbEndOfWordId == _endOfWordId);
            return GetHiraganaData((masterdata == null) ? 0 : ((VerbAddEndOfWord)(masterdata)).Id);
        }

        private List<string> GetGroupByGroupIdFuriganaData(int _endOfWordId) {
            object masterdata = verbAddEndOfWordMaster.FirstOrDefault(n => n.VerbEndOfWordId == _endOfWordId);
            return GetFuriganaData((masterdata == null) ? 0 : ((VerbAddEndOfWord)(masterdata)).Id);
        }

        public override void ResetData() {
            verbAddEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                verbAddEndOfWordMaster.Add(new VerbAddEndOfWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
