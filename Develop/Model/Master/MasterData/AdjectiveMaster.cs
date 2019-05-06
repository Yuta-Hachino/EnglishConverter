using QuickStudyEnglish.Model.Enumeration;
using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Adjective
    {
        public int Id;
        public SubjectCategory SubjectCategory;
        public PersonalPronounCategory PersonalCategory;
        public int GroupId;
        public int EnglishId;
        public int ComboId;
        public AdjectiveCategory Category;

        public Adjective(int id, SubjectCategory subCategory, PersonalPronounCategory perCategory, int group, int english, int comboId, AdjectiveCategory category) {
            this.Id = id;
            this.SubjectCategory = subCategory;
            this.PersonalCategory = perCategory;
            this.GroupId = group;
            this.EnglishId = english;
            this.ComboId = comboId;
            this.Category = category;
        }
    }

    public class AdjectiveMaster : BaseMaster
    {
        private List<Adjective> adjectiveMaster = new List<Adjective>();

        public int GetGroupId(int _id) {
            return adjectiveMaster[_id].GroupId;
        }
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
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(adjectiveMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(adjectiveMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(adjectiveMaster[_id].ComboId);
        }

        private List<List<string>> GetRangeKanjiData(IEnumerable<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach(int id in _idList) {
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
        public List<Adjective> GetMixCreOrObjAndCategorySubject(SubjectCategory creture, PersonalPronounCategory category) {
            IEnumerable<Adjective> itemList = adjectiveMaster.Where(n => n.SubjectCategory == creture && n.PersonalCategory == ((category == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second));
            return itemList.ToList();
        }
        public override void ResetData() {
            adjectiveMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                adjectiveMaster.Add(new Adjective(int.Parse(master[0]), (SubjectCategory)int.Parse(master[1]), (PersonalPronounCategory)int.Parse(master[2]), int.Parse(master[3]), int.Parse(master[4]), int.Parse(master[5]), (AdjectiveCategory)int.Parse(master[6])));
            }
        }
    }
}
