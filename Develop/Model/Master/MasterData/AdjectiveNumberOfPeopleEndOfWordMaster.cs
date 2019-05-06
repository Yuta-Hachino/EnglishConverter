using System.Collections.Generic;
using System.Linq;
using QuickStudyEnglish.Model.Enumeration;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class AdjectiveNumberOfPeopleEndOfWord
    {
        public int Id;
        public int GroupId;
        public PersonalPronounCategory PersonCategory;
        public StepLevel Level;
        public int EnglishId;
        public int Priority;
        public AdjectiveCategory Category;

        public AdjectiveNumberOfPeopleEndOfWord(int _id, int _group, PersonalPronounCategory _category, StepLevel _level, int _english, int _priority, AdjectiveCategory _adjectiveCategory) {
            this.Id = _id;
            this.GroupId = _group;
            this.PersonCategory = _category;
            this.Level = _level;
            this.EnglishId = _english;
            this.Priority = _priority;
            this.Category = _adjectiveCategory;
        }
    }

    public class AdjectiveNumberOfPeopleEndOfWordMaster : BaseMaster
    {
        private List<AdjectiveNumberOfPeopleEndOfWord> adjectiveNumberOfPeopleEndOfWordMaster = new List<AdjectiveNumberOfPeopleEndOfWord>();
        public string GetEnglishWord(int _englishId) {
            return MasterFactory.GetMasterData<EnglishWordMaster>().GetData(_englishId);
        }
        public List<int> GetRangeEnglishIdGroupByGroupId(int _groupId) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => n.GroupId == _groupId).Select(n => n.EnglishId).ToList();
        }

        public List<string> GetRangeEnglishWordGroupByGroupId(int _groupId) {
            return GetRangeEnglishWordList(GetRangeEnglishIdGroupByGroupId(_groupId));
        }

        public List<int> GetRangeEnglishIdGroupByGroupIdAndPersonCategory(int _groupId, PersonalPronounCategory _personalCategory, AdjectiveCategory _category) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => 
            n.GroupId == _groupId 
            && n.PersonCategory == ((_personalCategory == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second) 
            && n.Category == _category)
            .Select(n => n.EnglishId).ToList();
        }
        public List<AdjectiveNumberOfPeopleEndOfWord> GetRangeEnglishItemGroupByGroupIdAndPersonCategory(int _groupId, PersonalPronounCategory _personalCategory, AdjectiveCategory _category) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => 
            n.GroupId == _groupId 
            && n.PersonCategory == ((_personalCategory == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second) 
            && n.Category == _category).ToList();
        }

        public List<int> GetRangeEnglishIdGroupByGroupIdAndPersonCategoryAndStepLevel(int _groupId, PersonalPronounCategory _personalCategory, StepLevel _level, AdjectiveCategory _category) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => n.GroupId == _groupId && n.PersonCategory == ((_personalCategory == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second) && n.Level == _level).Select(n => n.EnglishId).ToList();
        }
        public List<AdjectiveNumberOfPeopleEndOfWord> GetRangeEnglishItemGroupByGroupIdAndPersonCategoryAndStepLevel(int _groupId, PersonalPronounCategory _personalCategory, StepLevel _level, AdjectiveCategory _category) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => 
            n.GroupId == _groupId 
            && n.PersonCategory == ((_personalCategory == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second) 
            && n.Level == _level 
            && n.Category == _category).ToList();
        }

        public List<int> GetRangeEnglishIdGroupByGroupIdAndTimeTypeAndStepLevel(int _groupId, PersonalPronounCategory _personalCategory, StepLevel _level, AdjectiveCategory _category) {
            return adjectiveNumberOfPeopleEndOfWordMaster.Where(n => 
            n.GroupId == _groupId 
            && n.PersonCategory == ((_personalCategory == PersonalPronounCategory.First) ? PersonalPronounCategory.First : PersonalPronounCategory.Second) 
            && n.Level == _level 
            && n.Category == _category)
                .Select(n => n.EnglishId).ToList();
        }

        public List<string> GetRangeEnglishWordList(List<int> _englishIdList) {
            return MasterFactory.GetMasterData<EnglishWordMaster>().GetRangeData(_englishIdList);
        }

        public override void ResetData() {
            adjectiveNumberOfPeopleEndOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                //プライオリティはマスターに未実装の為初期値『0』のまま。
                adjectiveNumberOfPeopleEndOfWordMaster.Add(new AdjectiveNumberOfPeopleEndOfWord(
                    int.Parse(master[0]), 
                    int.Parse(master[1]), 
                    (PersonalPronounCategory)int.Parse(master[2]), 
                    (StepLevel)int.Parse(master[3]), 
                    int.Parse(master[4]), 
                    0,
                    (AdjectiveCategory)int.Parse(master[5])));
            }
        }
    }
}
