using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class BeingConnectionWord
    {
        public int Id;
        public int BeingWordId;
        public int PrepositionId;

        public BeingConnectionWord(int _id, int _beingId, int _prepositionId) {
            this.Id = _id;
            this.BeingWordId = _beingId;
            this.PrepositionId = _prepositionId;
        }
    }

    public class BeingConnectionMaster : BaseMaster
    {
        private List<BeingConnectionWord> beingConnectionMaster = new List<BeingConnectionWord>();

        public int GetPrepositionId(int _id) {
            return beingConnectionMaster[_id].PrepositionId;
        }

        public List<int> GetRangeData(int _beingWordId, int _numberOfPeopleId) {
            List<int> comboIdList = beingConnectionMaster.Where(n => n.BeingWordId == _beingWordId).Select(n => n.PrepositionId).ToList();
            return GetRangeKanjiData(comboIdList);
        }

        public List<int> GetRangeKanjiData(List<int> _idList) {
            List<int> rangeData = new List<int>();
            foreach (int id in _idList) {
                rangeData.Add(GetPrepositionId(id));
            }
            return rangeData;
        }


        public int GetGroupByGroupIdData(int _beingWordId) {
            object masterdata = beingConnectionMaster.FirstOrDefault(n => n.BeingWordId == _beingWordId);
            return GetPrepositionId((masterdata == null) ? 0 : ((BeingConnectionWord)(masterdata)).PrepositionId);
        }


        public override void ResetData() {
            beingConnectionMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                beingConnectionMaster.Add(new BeingConnectionWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
