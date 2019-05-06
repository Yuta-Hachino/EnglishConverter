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
    public class PersonPrepositionListView : ContainerBase
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
                SetPersonPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
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

        public void SetPersonPrepositionList(JapaneseReadType type, bool init = false) {
            // 内部要素をクリア
            parent.PersonPrepositionListView.Items.Clear(); 
            nowSelectedItemList.Clear();                    
            nowSelectedItemList = null;                      
            nowSelectedItemList = MasterFactory.GetMasterData<NounMaster>().GetNounItemListPersonAnimalAndVisible(true, SubjectCategory.None, NowQuantity);    
            nowSelectedItemList.Insert(0, MasterFactory.GetMasterData<NounMaster>().GetNounList()[0]);    
            nowSelectedItemIdList.Clear();                  
            nowSelectedItemIdList = null;                   
            nowSelectedItemIdList = nowSelectedItemList.Select(n => n.Id).ToList();    

            nowSelectedItemIdList.ForEach(n => {
                List<string> selectWord = MasterFactory.GetMasterData<NounMaster>().GetData(n, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                ListViewItem addItem = new ListViewItem(selectWord[0]);
                addItem.SubItems.Add(selectWord[1]);
                parent.PersonPrepositionListView.Items.Add(addItem);
            });

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                SelectedItem = nowSelectedItemList[0];
                parent.PersonPrepositionListView.Items[0].Selected = true;
                parent.PersonPrepositionListView.Click += PersonPrepositionListView_Click;
                parent.PersonPrepositionListView.SelectedIndexChanged += SetSelectedItemIndex;
            }
        }


        public void SetPersonPrepositionFocus() {
            if (parent.PersonPrepositionListView.Items.Count <= 0) {
                SetPersonPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if (parent.PersonPrepositionListView.Items.Count > 0) {
                parent.PersonPrepositionListView.Items[SelectedItemIndex].Focused = true;
                parent.PersonPrepositionListView.Items[SelectedItemIndex].Selected = true;

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
            if (parent.PersonPrepositionListView.SelectedIndices.Count > 0) {
                SelectedItem = nowSelectedItemList[parent.PersonPrepositionListView.SelectedIndices[0]];
                SelectedItemIndex = parent.PersonPrepositionListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.PersonPrepositionListView.SelectedIndices[0]];
                SelectedArticleId = SelectedItem.ArticleId;
                SelectedPossessiveId = SelectedItem.PossessiveId;
            }
            ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }
        private void PersonPrepositionListView_Click(object sender, EventArgs e)
        {
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        //private void _PersonPrepositionListView_Click(object sender, EventArgs e) {
        //    _UpdateControls();
        //}





        ////人グループ
        //   _PersonPrepositionListView.Items.Clear();
        //   _PersonPrepositionListView.Items.Add("選択");
        //   _MasterData.PrepositionItems.ForEach(prepositionitem => {
        //       if (!_PersonPrepositionListView.Items.Contains(new ListViewItem(prepositionitem.Source)) && prepositionitem.Preposition == PrePositions.WITH) {
        //           ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //           addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //           _PersonPrepositionListView.Items.Add(addItem);
        //       }
        //   });
        //   foreach (ListViewItem prePositionItem in _PersonPrepositionListView.Items) {
        //       if (prePositionItem.Text == SelectPersonPrepositionWord) _PersonPrepositionListView.Items[prePositionItem.Index].Selected = true;
        //   }





        ////人グループ
        //    _PersonPrepositionListView.Items.Clear();
        //    _PersonPrepositionListView.Items.Add("選択");
        //    _MasterData.PrepositionItems.ForEach(prepositionitem => {
        //        if (!_PersonPrepositionListView.Items.Contains(new ListViewItem(prepositionitem.Source)) && prepositionitem.Preposition == PrePositions.WITH) {
        //            ListViewItem addItem = new ListViewItem(prepositionitem.Source);
        //            addItem.SubItems.Add(prepositionitem.Preposition.ToString().ToLower() + " " + prepositionitem.Value);
        //            _PersonPrepositionListView.Items.Add(addItem);
        //        }
        //    });
        //    foreach (ListViewItem prePositionItem in _PersonPrepositionListView.Items) {
        //        if (prePositionItem.Text == SelectPersonPrepositionWord) _PersonPrepositionListView.Items[prePositionItem.Index].Selected = true;
        //    }

    }
}
