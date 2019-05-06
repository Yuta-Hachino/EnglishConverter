using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class EndOfWord
    {
        public int Id;
        public int ComboId;

        public EndOfWord(int id, int comboId) {
            this.Id = id;
            this.ComboId = comboId;
        }
    }

    public class EndOfWordMaster : BaseMaster
    {
        private List<EndOfWord> endOfWordMaster = new List<EndOfWord>();

        public List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(endOfWordMaster.FirstOrDefault(data => data.Id == _id).ComboId);
        }

        public List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(endOfWordMaster.FirstOrDefault(data => data.Id == _id).ComboId);
        }

        public List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(endOfWordMaster.FirstOrDefault(data => data.Id == _id).ComboId);
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

        public override void ResetData() {
            endOfWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                endOfWordMaster.Add(new EndOfWord(int.Parse(master[0]), int.Parse(master[1])));
            }
        }
    }
}
