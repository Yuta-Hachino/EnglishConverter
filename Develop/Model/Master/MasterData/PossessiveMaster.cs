using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Possessive
    {
        ///<summary>所有格ID</summary>
        public int Id;
        ///<summary>所有格GroupID</summary>
        public int GroupId;
        ///<summary>英日組み合わせID</summary>
        public int ComboId;

        public Possessive(int _id, int _groupId, int _comboId) {
            this.Id = _id;
            this.GroupId = _groupId;
            this.ComboId = _comboId;
        }
    }

    public class PossessiveMaster : BaseMaster
    {
        private static List<Possessive> possessiveMaster = new List<Possessive>();
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
        private List<string> GetKanjiData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(possessiveMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(possessiveMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(possessiveMaster[_id].ComboId);
        }

        public List<Possessive> GetpossessiveItemList(int _groupId) {
            return possessiveMaster.Where(n => n.GroupId == _groupId).ToList();
        }

        private List<List<string>> GetRangeKanjiData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
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

        private List<List<string>> GetRangeFuriganaData(List<int> _idList) {
            List<List<string>> rangeData = new List<List<string>>();
            foreach (int id in _idList) {
                rangeData.Add(GetFuriganaData(id));
            }
            return rangeData;
        }

        public override void ResetData() {
            possessiveMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                possessiveMaster.Add(new Possessive(
                    int.Parse(master[0]),       //所有格ID
                    int.Parse(master[1]),
                    int.Parse(master[2])));       //英日組み合わせID
            }
        }
    }
}
