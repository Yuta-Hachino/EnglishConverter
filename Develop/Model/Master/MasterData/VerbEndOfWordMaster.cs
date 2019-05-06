using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class VerbEndOfWord
    {
        public int Id;
        public int GroupId;
        public int VerbId;
        public TimeType TimeType;
        public StepLevel Level;
        public int ComboId;
        public int Priority;

        public VerbEndOfWord(int _id, int _groupId, int _vId, TimeType _type, StepLevel _level, int _comboId, int _priority) {
            this.Id = _id;
            this.GroupId = _groupId;
            this.VerbId = _vId;
            this.TimeType = _type;
            this.Level = _level;
            this.ComboId = _comboId;
            this.Priority = _priority;
        }
    }

    public class VerbEndOfWordMaster : BaseMaster
    {
        private List<VerbEndOfWord> verbEndOfWordMaster = new List<VerbEndOfWord>();
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
        public List<List<string>> GetRangeData(List<VerbEndOfWord> _idList, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(_idList);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(_idList);
                default: return GetRangeFuriganaData(_idList);
            }
        }
        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(verbEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(verbEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(verbEndOfWordMaster[_id].ComboId);
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
        private List<List<string>> GetRangeKanjiData(List<VerbEndOfWord> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (VerbEndOfWord id in _idList) {
                rangeData.Add(GetKanjiData(id.Id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeHiraganaData(List<VerbEndOfWord> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (VerbEndOfWord id in _idList) {
                rangeData.Add(GetHiraganaData(id.Id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeFuriganaData(List<VerbEndOfWord> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (VerbEndOfWord id in _idList) {
                rangeData.Add(GetFuriganaData(id.Id));
            }
            return rangeData;
        }

        public IEnumerable<VerbEndOfWord> GetRangeGroupByGroupId(int _groupId) {
            return verbEndOfWordMaster.Where(n => n.VerbId == _groupId);
        }

        public IEnumerable<VerbEndOfWord> GetRangeGroupByGroupIdAndStepLevel(int _groupId, StepLevel _level) {
            return verbEndOfWordMaster.Where(n => n.VerbId == _groupId && n.Level == _level);
        }

        public override void ResetData() {
            verbEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                verbEndOfWordMaster.Add(new VerbEndOfWord(
                    int.Parse(master[0]), 
                    int.Parse(master[1]), 
                    int.Parse(master[2]), 
                    (TimeType)int.Parse(master[3]), 
                    (StepLevel)int.Parse(master[4]), 
                    int.Parse(master[5]), 
                    int.Parse(master[6])));
            }
        }
    }
}
