using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Master.MasterData
{
    public class Noun
    {
        ///<summary>名詞ID</summary>
        public int Id;
        ///<summary>英日組み合わせID（名詞）</summary>
        public int ComboId;
        ///<summary>冠詞GroupID</summary>
        public int ArticleId;
        ///<summary>所有格GroupIDID</summary>
        public int PossessiveId;
        ///<summary>名詞ID</summary>
        public int MultiId;
        ///<summary>非表示（0＝表示、1＝非表示）</summary>
        public bool Visible;
        ///<summary>カテゴリ（0＝カテゴリ無、1＝人、2＝動物、3＝場所）</summary>
        public NounCategory nounCategory;
        ///<summary>人モノ判定ID（指定無=0生物=1無生物=2）</summary>
        public SubjectCategory CreatureOrObjectId;


        public Noun(int _id, int _comboId, int _articleId, int _possessiveId, int _multiId, int _visible, int _category, int _creatureOrObjectId) {
            this.Id = _id;
            this.ComboId = _comboId;
            this.ArticleId = _articleId;
            this.PossessiveId = _possessiveId;
            this.MultiId = _multiId;
            this.Visible = _visible == 0 ? true : false;
            this.nounCategory = (NounCategory)_category;
            this.CreatureOrObjectId = (SubjectCategory)_creatureOrObjectId;
        }
    }

    public class NounMaster : BaseMaster
    {
        private static List<Noun> nounMaster = new List<Noun>();

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
            return MasterFactory.GetMasterData<ComboWordMaster>().GetKanjiData(nounMaster[_id].ComboId);
        }

        private List<string> GetHiraganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetHiraganaData(nounMaster[_id].ComboId);
        }

        private List<string> GetFuriganaData(int _id) {
            return MasterFactory.GetMasterData<ComboWordMaster>().GetFuriganaData(nounMaster[_id].ComboId);
        }

        public List<int> GetNounIdList() {
            return nounMaster.Select(n => n.Id).ToList();
        }
        public List<Noun> GetNounList() {
            return nounMaster;
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

        public List<Noun> GetNounItemListCategory(NounCategory _nounCategory, SubjectCategory _creatureOrObject, QuantityCategory _quantityCategory) {
            return nounMaster.Where(n => n.nounCategory == _nounCategory
            && ((_quantityCategory == QuantityCategory.single)? n.MultiId == 0 : n.MultiId != 0) 
            && (n.CreatureOrObjectId == _creatureOrObject || n.CreatureOrObjectId == 0)
            ).ToList();
        }

        public List<Noun> GetNounItemListVisible(bool _visible, SubjectCategory _creatureOrObject, QuantityCategory _quantityCategory) {
            return nounMaster.Where(n =>
            ((_quantityCategory == QuantityCategory.single) ? n.MultiId == 0 : n.MultiId != 0)
            && (n.CreatureOrObjectId == _creatureOrObject || n.CreatureOrObjectId == 0)
            &&n.Visible == _visible).ToList();
        }

        public List<Noun> GetNounItemListCategoryAndVisible(NounCategory _nounCategory, bool _visible, SubjectCategory _creatureOrObject, QuantityCategory _quantityCategory) {
            return nounMaster.Where(n => n.nounCategory == _nounCategory
            &&((_quantityCategory == QuantityCategory.single) ? n.MultiId == 0 : n.MultiId != 0)
            && (n.CreatureOrObjectId == _creatureOrObject || n.CreatureOrObjectId == 0)
            && n.Visible == _visible).ToList();
        }

        public List<Noun> GetNounItemListPersonAnimalAndVisible(bool _visible, SubjectCategory _creatureOrObject, QuantityCategory _quantityCategory) {
            return nounMaster.Where(n => (n.nounCategory == NounCategory.People || n.nounCategory == NounCategory.Animal) 
            && ((_quantityCategory == QuantityCategory.single) ? n.MultiId == 0 : n.MultiId != 0)
            && (n.CreatureOrObjectId == _creatureOrObject || n.CreatureOrObjectId == 0)
            && n.Visible == _visible).ToList();
        }

        public override void ResetData() {
            nounMaster.Clear();
        }

        public override void SetData(List<string> _masterTextList) {
            List<List<string>> masterData = Common.Util.ReadDelimiterLine(_masterTextList, DELIMITER);
            foreach (List<string> master in masterData) {
                nounMaster.Add(new Noun(
                    int.Parse(master[0]),       //名詞ID
                    int.Parse(master[1]),       //英日組み合わせID（名詞）
                    int.Parse(master[2]),       //冠詞GroupIDID
                    int.Parse(master[3]),       //所有格GroupID
                    int.Parse(master[4]),       //複数形名詞ID
                    int.Parse(master[5]),       //非表示（0＝表示、1＝非表示）
                    int.Parse(master[6]),       //カテゴリ（0＝カテゴリ無、1＝人、2＝動物、3＝場所）
                    int.Parse(master[7])));     //人モノ判定ID（指定無=0生物=1無生物=2）
            }
        }
    }
}
