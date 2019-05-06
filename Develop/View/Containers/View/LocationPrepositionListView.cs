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
    public class LocationPrepositionListView : ContainerBase
    {
        /// <summary>
        /// 数量カテゴリ（０＝単数、１＝複数）
        /// </summary>
        public QuantityCategory NowQuantity
        {
            get => ContainerFactory.NounTypePanel._nowQuantity;
            set {
                ContainerFactory.NounTypePanel._nowQuantity = value;
                //選択表示更新
                if (ContainerFactory.NounTypePanel._nowQuantity == QuantityCategory.single) {
                    parent.quantitySingleBtn.Checked = true;
                    parent.quantityMultiBtn.Checked = false;
                } else {
                    parent.quantitySingleBtn.Checked = false;
                    parent.quantityMultiBtn.Checked = true;
                }
                //冠詞リスト更新
                ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                //所有格リスト更新
                ContainerFactory.PossessiveList.SetPossessiveList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                //リスト表示更新
                SetLocationPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                //変換結果更新
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
            }
        }

        /// <summary>
        /// 選択中の冠詞ID
        /// </summary>
        public int SelectedArticleId { get; set; }
        /// <summary>
        /// 選択中の所有格ID
        /// </summary>
        public int SelectedPossessiveId { get; set; }

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
        public Noun SelectedItem
        {
            get;
            private set;
        }
        /// <summary>
        /// 現在表示されているアイテムのリスト
        /// </summary>
        private List<Noun> nowSelectedItemList = new List<Noun>();

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedItemIdList = new List<int>();

        public void SetLocationPrepositionList(JapaneseReadType type, bool init = false) {
            // 内部要素をクリア
            parent.LocationPrepositionListView.Items.Clear();
            nowSelectedItemList.Clear();                    
            nowSelectedItemList = null;                     
            nowSelectedItemList = MasterFactory.GetMasterData<NounMaster>().GetNounItemListCategoryAndVisible(NounCategory.Location, true, ContainerFactory.SubjectList._Subject, NowQuantity);    //SET!|дﾟ)ﾊｯ！
            nowSelectedItemList.Insert(0, MasterFactory.GetMasterData<NounMaster>().GetNounList()[0]);     
            nowSelectedItemIdList.Clear();                  
            nowSelectedItemIdList = null;                    
            nowSelectedItemIdList = nowSelectedItemList.Select(n => n.Id).ToList();   

            nowSelectedItemIdList.ForEach(n => {
                List<string> selectWord = MasterFactory.GetMasterData<NounMaster>().GetData(n, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                ListViewItem addItem = new ListViewItem(selectWord[0]);
                addItem.SubItems.Add(selectWord[1]);
                parent.LocationPrepositionListView.Items.Add(addItem);
            });

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                SelectedItem = nowSelectedItemList[0];
                parent.LocationPrepositionListView.Items[0].Selected = true;
                parent.LocationPrepositionListView.SelectedIndexChanged += SetSelectedItemIndex;
                parent.LocationPrepositionListView.Click += LocationPrepositionListView_Click;
            }
        }

        private void LocationPrepositionListView_Click(object sender, EventArgs e)
        {
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        public void SetLocationPrepositionFocus() {
            if (parent.LocationPrepositionListView.Items.Count <= 0) {
                SetLocationPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if (parent.LocationPrepositionListView.Items.Count > 0) {
                parent.LocationPrepositionListView.Items[SelectedItemIndex].Focused = true;
                parent.LocationPrepositionListView.Items[SelectedItemIndex].Selected = true;

                // 語尾もセット
                ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];
            SelectedItemId = nowSelectedItemIdList[0];
            SelectedArticleId = SelectedItem.ArticleId;
            SelectedPossessiveId = SelectedItem.PossessiveId;
            if (parent.LocationPrepositionListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.LocationPrepositionListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.LocationPrepositionListView.SelectedIndices[0]];
                SelectedItem = nowSelectedItemList[parent.LocationPrepositionListView.SelectedIndices[0]];
                SelectedArticleId = SelectedItem.ArticleId;
                SelectedPossessiveId = SelectedItem.PossessiveId;
            }
            ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }
        //private void _LocationPrepositionListView_Click(object sender, EventArgs e) {
        //    _UpdateControls();
        //}






        ////場所グループ
        //  _LocationPrepositionListView.Items.Clear();
        //  _LocationPrepositionListView.Items.Add("選択");
        //  _MasterData.PrepositionItems.ForEach(prepositionitem => {
        //      if (!_LocationPrepositionListView.Items.Contains(new ListViewItem(prepositionitem.Source)) && prepositionitem.Preposition != PrePositions.WITH) {
        //          ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //          addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //          _LocationPrepositionListView.Items.Add(addItem);
        //      }
        //  });
        //  foreach (ListViewItem prePositionItem in _LocationPrepositionListView.Items) {
        //      if (prePositionItem.Text == SelectPrepositionWord) _LocationPrepositionListView.Items[prePositionItem.Index].Selected = true;
        //  }





        ////場所グループ
        //    _LocationPrepositionListView.Items.Clear();
        //    _LocationPrepositionListView.Items.Add("選択");
        //    _MasterData.PrepositionItems.ForEach(prepositionitem => {
        //        if (!_LocationPrepositionListView.Items.Contains(new ListViewItem(prepositionitem.Source)) && prepositionitem.Preposition != PrePositions.WITH) {
        //            ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //            addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //            _LocationPrepositionListView.Items.Add(addItem);
        //        }
        //    });
        //    foreach (ListViewItem prePositionItem in _LocationPrepositionListView.Items) {
        //        if (prePositionItem.Text == SelectLocationPrepositionWord) _LocationPrepositionListView.Items[prePositionItem.Index].Selected = true;
        //    }

    }
}
