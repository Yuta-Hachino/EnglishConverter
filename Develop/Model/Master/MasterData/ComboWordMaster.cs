using System.Collections.Generic;
using System.Linq;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class ComboWord
    {
        public int Id;
        public int EnglishId;
        public int JapaneseId;

        public ComboWord(int _id, int _eId, int _jId) {
            this.Id = _id;
            this.EnglishId = _eId;
            this.JapaneseId = _jId;
        }
    }

    public class ComboWordMaster : BaseMaster
    {
        private static List<ComboWord> comboWordMaster = new List<ComboWord>();

        public List<string> GetKanjiData(int _id) {
            List<string> comboList = new List<string>();
            ComboWord combo = comboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<EnglishWordMaster>().GetData(combo.EnglishId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseComboMaster>().GetKanjiData(combo.JapaneseId));
            }
            return comboList;
        }

        public List<string> GetHiraganaData(int _id) {
            List<string> comboList = new List<string>();
            ComboWord combo = comboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<EnglishWordMaster>().GetData(combo.EnglishId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseComboMaster>().GetHiraganaData(combo.JapaneseId));
            }
            return comboList;
        }

        public List<string> GetFuriganaData(int _id) {
            List<string> comboList = new List<string>();
            ComboWord combo = comboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<EnglishWordMaster>().GetData(combo.EnglishId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseComboMaster>().GetFuriganaData(combo.JapaneseId));
            }
            return comboList;
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
            comboWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                comboWordMaster.Add(new ComboWord(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
