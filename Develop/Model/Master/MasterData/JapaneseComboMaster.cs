using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class JapaneseCombo
    {
        public int Id;
        public int JapaneseWordMasterId;
        public int JapansesEndingWordMasterId;

        public JapaneseCombo(int _id, int _jwmId, int _jewmId) {
            this.Id = _id;
            this.JapaneseWordMasterId = _jwmId;
            this.JapansesEndingWordMasterId = _jewmId;
        }
    }

    public class JapaneseComboMaster : BaseMaster
    {
        private static List<JapaneseCombo> jcomboWordMaster = new List<JapaneseCombo>();

        public List<string> GetKanjiGroupData(int _id) {
            List<string> comboList = new List<string>();
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<JapaneseWordMaster>().GetKanjiData(combo.JapaneseWordMasterId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetKanjiData(combo.JapansesEndingWordMasterId));
            }
            return comboList;
        }

        public string GetKanjiData(int _id) {
            string kanji = "";
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                kanji = MasterFactory.GetMasterData<JapaneseWordMaster>().GetKanjiData(combo.JapaneseWordMasterId) + MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetKanjiData(combo.JapansesEndingWordMasterId);
            }
            return kanji;
        }

        public List<string> GetHiraganaGroupData(int _id) {
            List<string> comboList = new List<string>();
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<JapaneseWordMaster>().GetHiraganaData(combo.JapaneseWordMasterId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetHiraganaData(combo.JapansesEndingWordMasterId));
            }
            return comboList;
        }

        public string GetHiraganaData(int _id) {
            string hiragana = "";
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                hiragana = MasterFactory.GetMasterData<JapaneseWordMaster>().GetHiraganaData(combo.JapaneseWordMasterId) + MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetHiraganaData(combo.JapansesEndingWordMasterId);
            }
            return hiragana;
        }

        public List<string> GetFuriganaGroupData(int _id) {
            List<string> comboList = new List<string>();
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                comboList.Add(MasterFactory.GetMasterData<JapaneseWordMaster>().GetFuriganaData(combo.JapaneseWordMasterId));
                comboList.Add(MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetFuriganaData(combo.JapansesEndingWordMasterId));
            }
            return comboList;
        }

        public string GetFuriganaData(int _id) {
            string furigana = "";
            JapaneseCombo combo = jcomboWordMaster[_id];
            if (combo != null) {
                furigana =  MasterFactory.GetMasterData<JapaneseWordMaster>().GetFuriganaData(combo.JapaneseWordMasterId) + MasterFactory.GetMasterData<JapaneseEndingWordMaster>().GetFuriganaData(combo.JapansesEndingWordMasterId);
            }
            return furigana;
        }

        public List<List<string>> GetRangeKanjiGroupData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiGroupData(id));
            }
            return rangeData;
        }

        public List<string> GetRangeKanjiData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        public List<List<string>> GetRangeHiraganaGroupData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaGroupData(id));
            }
            return rangeData;
        }

        public List<string> GetRangeHiraganaData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        public List<List<string>> GetRangeFuriganaGroupData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaGroupData(id));
            }
            return rangeData;
        }

        public List<string> GetRangeFuriganaData(List<int> _idList) {
            List<string> rangeData = new List<string>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            jcomboWordMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                jcomboWordMaster.Add(new JapaneseCombo(int.Parse(master[0]), int.Parse(master[1]), int.Parse(master[2])));
            }
        }
    }
}
