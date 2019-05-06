using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class AdjectiveConnectionWord
    {
        public int Id;
        public int AdjectiveWordId;
        public int PrepositionId;

        public AdjectiveConnectionWord(int _id, int _adjectiveId, int _prepositionId) {
            this.Id = _id;
            this.AdjectiveWordId = _adjectiveId;
            this.PrepositionId = _prepositionId;
        }
    }

    public class AdjectiveConnectionMaster : BaseMaster
    {
        private List<AdjectiveConnectionWord> adjectiveConnectionMaster = new List<AdjectiveConnectionWord>();

        public int GetData(int _id) {
            return adjectiveConnectionMaster[_id].PrepositionId;
        }

        public List<int> GetRangeData(int _adjectiveWordId, int _numberOfPeopleId) {
            List<int> comboIdList = adjectiveConnectionMaster.Where(n => n.AdjectiveWordId == _adjectiveWordId).Select(n => n.PrepositionId).ToList();
            return GetRangeData(comboIdList);
        }

        public List<int> GetRangeData(List<int> _idList) {
            List<int> rangeData = new List<int>();
            foreach (int id in _idList) {
                rangeData.Add(GetData(id));
            }
            return rangeData;
        }


        public int GetGroupByGroupIdData(int _adjectiveWordId) {
            object masterdata = adjectiveConnectionMaster.FirstOrDefault(n => n.AdjectiveWordId == _adjectiveWordId);
            return (masterdata == null)? 0 : ((AdjectiveConnectionWord)(masterdata)).PrepositionId;
        }


        public override void ResetData() {
            adjectiveConnectionMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                adjectiveConnectionMaster.Add(new AdjectiveConnectionWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
