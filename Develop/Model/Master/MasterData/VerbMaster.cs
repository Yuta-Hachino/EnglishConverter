using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Verb
    {
        public int Id;
        public int GroupId;
        public SubjectCategory SubjectCategory;
        public PersonalPronounCategory PersonalCategory;
        public int ComboId;
        public int PastVerbId;
        public int PastParticipleId;
        public int ProgressingId;
        public int Priority;

        public Verb(int id, int endId, int subCategory, int perCategory, int comboId, int pastVerb, int pastParticiple, int progressing, int priority) {
            this.Id = id;
            this.GroupId = endId;
            this.SubjectCategory = (SubjectCategory)subCategory;
            this.PersonalCategory = (PersonalPronounCategory)perCategory;
            this.ComboId = comboId;
            this.PastVerbId = pastVerb;
            this.PastParticipleId = pastParticiple;
            this.ProgressingId = progressing;
            this.Priority = priority;
        }
    }

    public class VerbMaster : BaseMaster
    {
        public List<Verb> verbMaster = new List<Verb>();
        public int GetGroupId(int _id) {
            return verbMaster[_id].Id;
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
        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(verbMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(verbMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(verbMaster[_id].ComboId);
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
        public List<Verb> GetMixCreOrObjAndCategorySubject(SubjectCategory creture, PersonalPronounCategory category) {
            return verbMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == category).ToList();
        }
        public List<int> GetMixCreOrObjAndCategorySubjectId(SubjectCategory creture, PersonalPronounCategory category) {
            return verbMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == category).Select(indexer => indexer.Id).ToList();
        }
        public override void ResetData() {
            verbMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                verbMaster.Add(new Verb(int.Parse(master[0]), 
                    int.Parse(master[1]), 
                    int.Parse(master[2]), 
                    int.Parse(master[3]), 
                    int.Parse(master[4]), 
                    int.Parse(master[5]), 
                    int.Parse(master[6]), 
                    int.Parse(master[7]), 
                    int.Parse(master[8])));
            }
        }
    }
}
