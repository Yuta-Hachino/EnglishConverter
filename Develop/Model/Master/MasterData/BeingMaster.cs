using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class BeingWord
    {
        public int Id;
        public SubjectCategory SubjectCategory;
        public PersonalPronounCategory PersonalCategory;
        public StepLevel Level;
        public int ComboId;
        public int Priority;


        public BeingWord(int _id, SubjectCategory _subjectCategory, PersonalPronounCategory _personalCategory, StepLevel _level, int _comboId, int _priority) {
            this.Id = _id;
            this.SubjectCategory = _subjectCategory;
            this.PersonalCategory = _personalCategory;
            this.Level = _level;
            this.ComboId = _comboId;
            this.Priority = _priority;
        }
    }

    public class BeingWordMaster : BaseMaster
    {
        private List<BeingWord> beingEndOfWordMaster = new List<BeingWord>();
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
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(beingEndOfWordMaster[_id].ComboId); ;
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(beingEndOfWordMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(beingEndOfWordMaster[_id].ComboId);
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
        public IEnumerable<BeingWord> GetRangeIdPersonalPronounSubjectAndStepLevelMaster(StepLevel _level, PersonalPronounCategory _personal, SubjectCategory _subject) {
            return beingEndOfWordMaster.Where(n => n.Level == _level && n.PersonalCategory == _personal && n.SubjectCategory == _subject);
        }
        public IEnumerable<BeingWord> GetRangeIdPersonalPronounAndSubjectMaster(PersonalPronounCategory _personal, SubjectCategory _subject) {
            return beingEndOfWordMaster.Where(n => n.PersonalCategory == _personal && n.SubjectCategory == _subject);
        }
        public IEnumerable<int> GetRangeIdPersonalPronounSubjectAndStepLevel(StepLevel _level, PersonalPronounCategory _personal, SubjectCategory _subject) {
            return beingEndOfWordMaster.Where(n => n.Level == _level && n.PersonalCategory == _personal && n.SubjectCategory == _subject).Select(n => n.Id);
        }
        public IEnumerable<int> GetRangeIdPersonalPronounAndSubject(PersonalPronounCategory _personal, SubjectCategory _subject) {
            return beingEndOfWordMaster.Where(n => n.PersonalCategory == _personal && n.SubjectCategory == _subject).Select(n => n.Id);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupId(int _groupId) {
            return beingEndOfWordMaster.Select(n => n.ComboId);
        }

        public IEnumerable<int> GetRangeComboIdGroupByGroupIdAndStepLevel(int _groupId, StepLevel _level) {
            return beingEndOfWordMaster.Where(n => n.Level == _level).Select(n => n.ComboId);
        }

        public override void ResetData() {
            beingEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                beingEndOfWordMaster.Add(new BeingWord(
                    int.Parse(master[0]), 
                    (SubjectCategory)int.Parse(master[1]), 
                    (PersonalPronounCategory)int.Parse(master[2]),
                    (StepLevel)int.Parse(master[3]), 
                    int.Parse(master[4]),
                    int.Parse(master[5])));
            }
        }
    }
}
