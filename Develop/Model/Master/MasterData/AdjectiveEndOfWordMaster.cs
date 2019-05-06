using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class AdjectiveEndOfWord
    {
        public int Id;
        public int GroupId;
        public TimeType TimeType;
        public StepLevel Level;
        public int ComboId;
        public AdjectiveCategory Category;

        public AdjectiveEndOfWord(int _id, int _group, TimeType _time, StepLevel _level, int _comboId, AdjectiveCategory category) {
            this.Id = _id;
            this.GroupId = _group;
            this.TimeType = _time;
            this.Level = _level;
            this.ComboId = _comboId;
            this.Category = category;
        }
    }

    public class AdjectiveEndOfWordMaster : BaseMaster
    {
        private List<AdjectiveEndOfWord> adjectiveEndOfWordMaster = new List<AdjectiveEndOfWord>();
        public List<string> GetData(int _id, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetKanjiData(_id);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetHiraganaData(_id);
                default: return GetFuriganaData(_id);
            }
        }
        public List<List<string>> GetRangeData(IEnumerable<int> _idList, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_idList);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_idList);
                default: return GetRangeFuriganaData(_idList);
            }
        }
        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(adjectiveEndOfWordMaster[_id].ComboId); ;
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(adjectiveEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(adjectiveEndOfWordMaster[_id].ComboId);
        }

        private List<List<string>> GetRangeKanjiData(IEnumerable<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeHiraganaData(IEnumerable<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeFuriganaData(IEnumerable<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public IEnumerable<int> GetRangeIdGroupByGroupId(int _groupId, AdjectiveCategory _category) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.Category == _category).Select(n => n.Id);
        }
        public IEnumerable<AdjectiveEndOfWord> GetRangeItemGroupByGroupId(int _groupId, AdjectiveCategory _category) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.Category == _category);
        }

        public IEnumerable<int> GetRangeIdGroupByGroupIdAndTimeType(int _groupId, TimeType _time) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.TimeType == _time).Select(n => n.Id);
        }

        public IEnumerable<int> GetRangeIdGroupByGroupIdAndStepLevel(int _groupId, StepLevel _level, AdjectiveCategory _category) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.Level == _level && n.Category == _category).Select(n => n.Id);
        }
        public IEnumerable<AdjectiveEndOfWord> GetRangeItemGroupByGroupIdAndStepLevel(int _groupId, StepLevel _level, AdjectiveCategory _category) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.Level == _level && n.Category == _category);
        }

        public IEnumerable<int> GetRangeIdGroupByGroupIdAndTimeTypeAndStepLevel(int _groupId, TimeType _time, StepLevel _level) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.TimeType == _time && n.Level == _level).Select(n => n.Id);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupId(int _groupId) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId).Select(n => n.ComboId);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupIdAndTimeType(int _groupId, TimeType _time) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.TimeType == _time).Select(n => n.ComboId);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupIdAndStepLevel(int _groupId, StepLevel _level) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.Level == _level).Select(n => n.ComboId);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupIdAndTimeTypeAndStepLevel(int _groupId, TimeType _time, StepLevel _level) {
            return adjectiveEndOfWordMaster.Where(n => n.GroupId == _groupId && n.TimeType == _time && n.Level == _level).Select(n => n.ComboId);
        }

        public override void ResetData() {
            adjectiveEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                adjectiveEndOfWordMaster.Add(new AdjectiveEndOfWord(
                    int.Parse(master[0]), 
                    int.Parse(master[1]), 
                    (TimeType)int.Parse(master[2]), 
                    (StepLevel)int.Parse(master[3]), 
                    int.Parse(master[4]),
                    (AdjectiveCategory)int.Parse(master[5])));
            }
        }
    }
}
