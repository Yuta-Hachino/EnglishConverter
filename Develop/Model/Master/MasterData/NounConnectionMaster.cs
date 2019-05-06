using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class NounConnectionWord
    {
        public int Id;
        public int NounWordId;
        public int PrepositionEnglishWordId;

        public NounConnectionWord(int _id, int _NounId, int _prepositionId) {
            this.Id = _id;
            this.NounWordId = _NounId;
            this.PrepositionEnglishWordId = _prepositionId;
        }
    }

    public class NounConnectionMaster : BaseMaster
    {
        private List<NounConnectionWord> nounConnectionMaster = new List<NounConnectionWord>();

        public string GetPrepositionData(int _id) {
            return MasterFactory.GetMasterData<EnglishWordMaster>().GetData(_id);
        }

        public List<string> GetRangeData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetPrepositionData(id));
            }
            return rangeData;
        }


        public string GetGroupByGroupIdData(int _NounWordId) {
            object masterdata = nounConnectionMaster.FirstOrDefault(n => n.NounWordId == _NounWordId);
            return GetPrepositionData((masterdata == null) ? 0 : ((NounConnectionWord)(masterdata)).PrepositionEnglishWordId);
        }


        public override void ResetData() {
            nounConnectionMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                nounConnectionMaster.Add(new NounConnectionWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
