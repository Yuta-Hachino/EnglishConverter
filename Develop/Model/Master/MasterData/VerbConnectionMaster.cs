using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class VerbConnectionWord
    {
        public int Id;
        public int VerbWordId;
        public int PrepositionId;

        public VerbConnectionWord(int _id, int _verbId, int _prepositionId) {
            this.Id = _id;
            this.VerbWordId = _verbId;
            this.PrepositionId = _prepositionId;
        }
    }

    public class VerbConnectionMaster : BaseMaster
    {
        private List<VerbConnectionWord> verbConnectionMaster = new List<VerbConnectionWord>();

        public int GetData(int _id) {
            return verbConnectionMaster[_id].PrepositionId;
        }

        public List<int> GetRangeData(int _verbWordId, int _numberOfPeopleId) {
            List<int> comboIdList = verbConnectionMaster.Where(n => n.VerbWordId == _verbWordId).Select(n => n.PrepositionId).ToList();
            return GetRangeData(comboIdList);
        }

        public List<int> GetRangeData(List<int> _idList) {
            List<int> rangeData = new List<int>();
            foreach (int id in _idList) {
                rangeData.Add(GetData(id));
            }
            return rangeData;
        }


        public int GetGroupByGroupIdData(int _verbWordId) {
            object masterdata = verbConnectionMaster.FirstOrDefault(n => n.VerbWordId == _verbWordId);
            return GetData((masterdata == null) ? 0 : ((VerbConnectionWord)(masterdata)).PrepositionId);
        }


        public override void ResetData() {
            verbConnectionMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                verbConnectionMaster.Add(new VerbConnectionWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
