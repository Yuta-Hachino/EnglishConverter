using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Adverb
    {
        public int Id;
        public AdverbPosition AdverbPosition;
        public TimeType TimeType;
        public string SpecialPositionStr;
        public int ComboId;
        public int Priority;

        public Adverb(int _id, AdverbPosition _pos, TimeType _timetype, string _ss, int _comboId, int _priority) {
            this.Id = _id;
            this.AdverbPosition = _pos;
            this.TimeType = _timetype;
            this.SpecialPositionStr = _ss;
            this.ComboId = _comboId;
            this.Priority = _priority;
        }
    }

    public class AdverbMaster : BaseMaster
    {
        private static List<Adverb> adverbMaster = new List<Adverb>();
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
        public List<List<string>> GetRangeData(AdverbPosition pos, JapaneseReadType japaneseReadType) {
            switch (japaneseReadType) {
                case Enumeration.JapaneseReadType.KANJI: return GetRangeKanjiData(pos);
                case Enumeration.JapaneseReadType.HIRAGANA: return GetRangeHiraganaData(pos);
                default: return GetRangeFuriganaData(pos);
            }
        }
        private List<string> GetKanjiData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(adverbMaster[_id].ComboId);
            return comboData;
        }

        private List<string> GetHiraganaData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(adverbMaster[_id].ComboId);
            return comboData;
        }

        private List<string> GetFuriganaData(int _id) {
            List<string> comboData = MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(adverbMaster[_id].ComboId);
            return comboData;
        }

        private List<List<string>> GetRangeKanjiData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeKanjiData(AdverbPosition pos) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in GetRangeComboIdFromAdverbPos(pos)) {
                rangeData.Add(GetKanjiData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeHiraganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeHiraganaData(AdverbPosition pos) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in GetRangeComboIdFromAdverbPos(pos)) {
                rangeData.Add(GetHiraganaData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeFuriganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        private List<List<string>> GetRangeFuriganaData(AdverbPosition pos) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in GetRangeComboIdFromAdverbPos(pos)) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public IEnumerable<int> GetRangeComboIdFromAdverbPos(AdverbPosition pos) {
            switch (pos) {
                case AdverbPosition.None:
                    return adverbMaster.Select(indexer => indexer.Id);

            }
            return adverbMaster.Where(n => n.AdverbPosition == pos).Select(indexer => indexer.Id);
        }

        public AdverbPosition GetAdverbPosFromId(int _id) {
            return adverbMaster[_id].AdverbPosition;
        }


        public override void ResetData() {
            adverbMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                adverbMaster.Add(new Adverb(int.Parse(master[0]), (AdverbPosition)int.Parse(master[1]), (TimeType)int.Parse(master[2]), master[3], int.Parse(master[4]), int.Parse(master[5])));
            }
        }
    }
}
