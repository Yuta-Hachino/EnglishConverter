using QuickStudyEnglish.Model.Enumeration;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickStudyEnglish.View.Containers
{
    public class PrePositionsListView : ContainerBase
    {
        /// <summary>
        /// 自身の選択されたアイテムインデックス
        /// </summary>
        public int SelectedItemIndex
        {
            get;
            private set;
        }

        public int SelectedItemId
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedItemIdList = new List<int>();

        public void SetPrePositionList(JapaneseReadType type, bool init = false) {
            // 内部要素をクリア
            parent.PrepositionListView.Items.Clear();

            nowSelectedItemIdList.Clear();

            List<List<string>> master = new List<List<string>>();

            switch (type) {
                case JapaneseReadType.HIRAGANA:
                    master = MasterFactory.GetMasterData<PrepositionMaster>().GetRangeHiraganaData(MasterFactory.GetMasterData<PrepositionMaster>().GetPrepositionIdList());
                    break;

                case JapaneseReadType.FURIGANA:
                    master = MasterFactory.GetMasterData<PrepositionMaster>().GetRangeFuriganaData(MasterFactory.GetMasterData<PrepositionMaster>().GetPrepositionIdList());
                    break;

                case JapaneseReadType.KANJI:
                default:
                    master = MasterFactory.GetMasterData<PrepositionMaster>().GetRangeKanjiData(MasterFactory.GetMasterData<PrepositionMaster>().GetPrepositionIdList());
                    break;
            }

            //主語リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.PrepositionListView.Items.Add(addItem);
                nowSelectedItemIdList.Add(items.index);
            }

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                parent.PrepositionListView.Items[0].Selected = true;
                parent.PrepositionListView.SelectedIndexChanged += SetSelectedItemIndex;
            }
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.PrepositionListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.PrepositionListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.PrepositionListView.SelectedIndices[0]];
            }
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }







        //private void _PrePositionsListView_Click(object sender, EventArgs e) {
        //    _UpdateControls();
        //}


        ////前置詞グループ
        //_PrePositionsListView.Items.Clear();
        //_PrePositionsListView.Items.Add("選択");
        //_MasterData.PrepositionItems.ForEach(prepositionitem => {
        //    if (prepositionitem.Preposition == _MasterData.AdjectiveItems.FirstOrDefault(x => x.Source == _AdjectiveListView.SelectedItems[0].Text).PreValue) {
        //        if (!_PrePositionsListView.Items.Contains(new ListViewItem(prepositionitem.Source))) {
        //            ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //            addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //            _PrePositionsListView.Items.Add(addItem);
        //        }
        //    }
        //});
        //foreach (ListViewItem prePositionItem in _PrePositionsListView.Items) {
        //    if (prePositionItem.Text == SelectPrepositionWord) _PrePositionsListView.Items[prePositionItem.Index].Selected = true;
        //}
        //foreach (StructurePrepositionItem item in _MasterData.PrepositionItems) {
        //    if (item.Source == SelectPrepositionWord) {
        //        _Preposition = item.Preposition;
        //    } else if (SelectPrepositionWord == "選択") {
        //        _Preposition = PrePositions.None;
        //    }
        //}



        ////前置詞グループ
        //_PrePositionsListView.Items.Clear();
        //_PrePositionsListView.Items.Add("選択");
        //_MasterData.PrepositionItems.ForEach(prepositionitem => {
        //    if (!_PrePositionsListView.Items.Contains(new ListViewItem(prepositionitem.Source))) {
        //        ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //        addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //        _PrePositionsListView.Items.Add(addItem);
        //    }
        //});

        //foreach (ListViewItem prePositionItem in _PrePositionsListView.Items) {
        //    if (prePositionItem.Text == SelectPrepositionWord) _PrePositionsListView.Items[prePositionItem.Index].Selected = true;
        //}
        //foreach (StructurePrepositionItem item in _MasterData.PrepositionItems) {
        //    if (item.Source == SelectPrepositionWord) {
        //        _Preposition = item.Preposition;
        //    } else if (SelectPrepositionWord == "選択") {
        //        _Preposition = PrePositions.None;
        //    }
        //}




        ////前置詞グループ
        //_PrePositionsListView.Items.Clear();
        //    _PrePositionsListView.Items.Add("選択");

    }
}
