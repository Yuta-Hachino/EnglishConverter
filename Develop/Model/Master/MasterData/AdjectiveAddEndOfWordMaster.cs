using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class AdjectiveAddEndOfWord
    {
        public int Id;
        public int AdjectiveEndOfWordId;
        public int AdjectiveNumberOfPeopleEndOfWordId;
        public int ComboId;

        public AdjectiveAddEndOfWord(int _id, int _endOfId, int _pEndOfId, int _comboId) {
            this.Id = _id;
            this.AdjectiveEndOfWordId = _endOfId;
            this.AdjectiveNumberOfPeopleEndOfWordId = _pEndOfId;
            this.ComboId = _comboId;
        }
    }

    public class AdjectiveAddEndOfWordMaster : BaseMaster
    {
        private List<AdjectiveAddEndOfWord> adjectiveAddEndOfWordMaster = new List<AdjectiveAddEndOfWord>();
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
        public List<string> GetData(int _endOfWordId, int _numberOfPeopleId, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetKanjiData(_endOfWordId, _numberOfPeopleId);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetHiraganaData(_endOfWordId, _numberOfPeopleId);
                default: return GetFuriganaData(_endOfWordId, _numberOfPeopleId);
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
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(adjectiveAddEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(adjectiveAddEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(adjectiveAddEndOfWordMaster[_id].ComboId);
        }

        private List<List<string>> GetRangeKanjiData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = adjectiveAddEndOfWordMaster.Where(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId).Select(n => n.ComboId).ToList();
            return GetRangeKanjiData(comboIdList);
        }

        private List<List<string>> GetRangeHiraganaData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = adjectiveAddEndOfWordMaster.Where(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId).Select(n => n.ComboId).ToList();
            return GetRangeHiraganaData(comboIdList);
        }

        private List<List<string>> GetRangeFuriganaData(int _endOfWordId, int _numberOfPeopleId) {
            List<int> comboIdList = adjectiveAddEndOfWordMaster.Where(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId).Select(n => n.ComboId).ToList();
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

        private List<string> GetKanjiData(int _endOfWordId, int _numberOfPeopleId) {
            object masterdata = adjectiveAddEndOfWordMaster.FirstOrDefault(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId);
            return GetKanjiData((masterdata == null)? 0 : ((AdjectiveAddEndOfWord)(masterdata)).Id);
        }

        private List<string> GetHiraganaData(int _endOfWordId, int _numberOfPeopleId) {
            object masterdata = adjectiveAddEndOfWordMaster.FirstOrDefault(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId);
            return GetHiraganaData((masterdata == null) ? 0 : ((AdjectiveAddEndOfWord)(masterdata)).Id);
        }

        private List<string> GetFuriganaData(int _endOfWordId, int _numberOfPeopleId) {
            object masterdata = adjectiveAddEndOfWordMaster.FirstOrDefault(n => n.AdjectiveEndOfWordId == _endOfWordId && n.AdjectiveNumberOfPeopleEndOfWordId == _numberOfPeopleId);
            return GetFuriganaData((masterdata == null) ? 0 : ((AdjectiveAddEndOfWord)(masterdata)).Id);
        }

        public override void ResetData() {
            adjectiveAddEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                adjectiveAddEndOfWordMaster.Add(new AdjectiveAddEndOfWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2]), int.Parse(master[3])));
            }
        }
    }
}
