using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Subject
    {
        public int Id;
        public SubjectCategory SubjectCategory;
        public PersonalPronounCategory PersonalCategory;
        public int ComboId;
        public string ModificationWord;

        public Subject(int id, int subCategory, int category, int comboId, string word) {
            this.Id = id;
            this.SubjectCategory = (SubjectCategory)subCategory;
            this.PersonalCategory = (PersonalPronounCategory)category;
            this.ComboId = comboId;
            this.ModificationWord = word;
        }
    }

    public class SubjectMaster : BaseMaster
    {
        private static List<Subject> subjectMaster = new List<Subject>();
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
        public List<int> GetCategoryData(int _id) {
            List<int> categoryData = new List<int>();
            categoryData.Add((int)(subjectMaster[_id].SubjectCategory));
            categoryData.Add((int)(subjectMaster[_id].PersonalCategory));
            return categoryData;
        }

        public List<string> GetKanjiData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(subjectMaster[_id].ComboId);
            comboData.Add(subjectMaster[_id].ModificationWord);
            return comboData;
        }

        public List<string> GetHiraganaData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(subjectMaster[_id].ComboId);
            comboData.Add(subjectMaster[_id].ModificationWord);
            return comboData;
        }

        public List<string> GetFuriganaData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(subjectMaster[_id].ComboId);
            comboData.Add(subjectMaster[_id].ModificationWord);
            return comboData;
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

        public List<List<string>> GetCreatureOrObjectSubjctKanji(SubjectCategory creature) {
            List<int> idList = null;
            switch (creature) {
                    case SubjectCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeKanjiData(idList);

            }
            idList = subjectMaster.Where(n => n.SubjectCategory == creature).Select(indexer => indexer.Id).ToList();
            return GetRangeKanjiData(idList);
        }

        public List<List<string>> GetCreatureOrObjectSubjctHiragana(SubjectCategory creature) {
            List<int> idList = null;
            switch (creature) {
                case SubjectCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeHiraganaData(idList);

            }
            idList = subjectMaster.Where(n => n.SubjectCategory == creature).Select(indexer => indexer.Id).ToList();
            return GetRangeHiraganaData(idList);
        }

        public List<List<string>> GetCreatureOrObjectSubjctFurigana(SubjectCategory creature) {
            List<int> idList = null;
            switch (creature) {
                case SubjectCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeFuriganaData(idList);

            }
            idList = subjectMaster.Where(n => n.SubjectCategory == creature).Select(indexer => indexer.Id).ToList();
            return GetRangeFuriganaData(idList);
        }

        public List<List<string>> GetCategorySubjectKanji(PersonalPronounCategory personalCategory) {
            List<int> idList = null;
            switch (personalCategory) {
                case PersonalPronounCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeKanjiData(idList);

            }
            idList = subjectMaster.Where(n => n.PersonalCategory == personalCategory).Select(indexer => indexer.Id).ToList();
            return GetRangeKanjiData(idList);
        }

        public List<List<string>> GetCategorySubjectHiragana(PersonalPronounCategory personalCategory) {
            List<int> idList = null;
            switch (personalCategory) {
                case PersonalPronounCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeHiraganaData(idList);

            }
            idList = subjectMaster.Where(n => n.PersonalCategory == personalCategory).Select(indexer => indexer.Id).ToList();
            return GetRangeHiraganaData(idList);
        }

        public List<List<string>> GetCategorySubjectFurigana(PersonalPronounCategory personalCategory) {
            List<int> idList = null;
            switch (personalCategory) {
                case PersonalPronounCategory.None:
                    idList = subjectMaster.Select(indexer => indexer.Id).ToList();
                    return GetRangeFuriganaData(idList);

            }
            idList = subjectMaster.Where(n => n.PersonalCategory == personalCategory).Select(indexer => indexer.Id).ToList();
            return GetRangeFuriganaData(idList);
        }

        public List<List<string>> GetMixCreOrObjAndCategorySubjectKanji(SubjectCategory creture, PersonalPronounCategory category) {
            List<int> idList = subjectMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == category).Select(indexer => indexer.Id).ToList();
            return GetRangeKanjiData(idList);
        }

        public List<List<string>> GetMixCreOrObjAndCategorySubjectHiragana(SubjectCategory creture, PersonalPronounCategory category) {
            List<int> idList = subjectMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == category).Select(indexer => indexer.Id).ToList();
            return GetRangeHiraganaData(idList);
        }

        public List<List<string>> GetMixCreOrObjAndCategorySubjectFurigana(SubjectCategory creture, PersonalPronounCategory category) {
            List<int> idList = subjectMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == category).Select(indexer => indexer.Id).ToList();
            return GetRangeFuriganaData(idList);
        }

        public override void ResetData() {
            subjectMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                subjectMaster.Add(new Subject(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2]), int.Parse(master[3]), master[4]));
            }
        }
    }
}
