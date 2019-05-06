using QuickStudyEnglish.Model.Enumeration;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using System.Linq;

namespace QuickStudyEnglish.View.Containers
{
    public class ArticleListView : ContainerBase
    {
        private QuantityCategory _nowQuantity;
        /// <summary>
        /// 数量カテゴリ（０＝単数、１＝複数）
        /// </summary>
        public QuantityCategory NowQuantity
        {
            get => _nowQuantity;
            set {
                _nowQuantity = value;
                //選択表示更新
                if (_nowQuantity == QuantityCategory.single) {
                    parent.quantitySingleBtn.Checked = true;
                    parent.quantityMultiBtn.Checked = false;
                } else {
                    parent.quantitySingleBtn.Checked = false;
                    parent.quantityMultiBtn.Checked = true;
                }
                //リスト表示更新
                SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                //変換結果更新
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
            }
        }


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
        public Article SelectedItem
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int NowSelectedGroupId { get; set; }
        /// <summary>
        /// 現在表示されているアイテムのリスト
        /// </summary>
        private List<Article> nowSelectedItemList = new List<Article>();

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedItemIdList = new List<int>();

        public void SetArticleList(JapaneseReadType type, bool init = false) {
            if (!init) {
                //表示する所有格をどの名詞リストから決定するか
                switch (ContainerFactory.NounTypePanel.NowPanelMode) {
                    case NounType.Noun: NowSelectedGroupId = ContainerFactory.NounList.SelectedItem.ArticleId; break;
                    case NounType.Location: NowSelectedGroupId = ContainerFactory.LocationPrepositionList.SelectedItem.ArticleId; break;
                    case NounType.Person: NowSelectedGroupId = ContainerFactory.PersonPrepositionList.SelectedItem.ArticleId; break;
                }
            }

            // 内部要素をクリア
            parent.ArticleListView.Items.Clear();
            nowSelectedItemList.Clear();
            nowSelectedItemList = null;
            nowSelectedItemList = MasterFactory.GetMasterData<ArticleMaster>().GetArticleItemList(NowSelectedGroupId);
            nowSelectedItemIdList.Clear();
            nowSelectedItemIdList = null;
            nowSelectedItemIdList = nowSelectedItemList.Select(n => n.Id).ToList();

            nowSelectedItemIdList.ForEach(n => {
                List<string> selectWord = MasterFactory.GetMasterData<ArticleMaster>().GetData(n, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                ListViewItem addItem = new ListViewItem(selectWord[0]);
                addItem.SubItems.Add(selectWord[1]);
                parent.ArticleListView.Items.Add(addItem);
            });

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                parent.ArticleListView.Items[0].Selected = true;
                parent.ArticleListView.SelectedIndexChanged += SetSelectedItemIndex;
                parent.ArticleListView.Click += ArticleListView_Click;
            }
        }

        private void ArticleListView_Click(object sender, EventArgs e) {
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        public void SetArticleFocus() {
            if (parent.ArticleListView.Items.Count <= 0) {
                SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if (parent.ArticleListView.Items.Count > 0) {
                parent.ArticleListView.Items[SelectedItemIndex].Focused = true;
                parent.ArticleListView.Items[SelectedItemIndex].Selected = true;
                // 語尾もセット
                ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.ArticleListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.ArticleListView.SelectedIndices[0];
                SelectedItem = nowSelectedItemList[parent.ArticleListView.SelectedIndices[0]];
                SelectedItemId = nowSelectedItemIdList[parent.ArticleListView.SelectedIndices[0]];
            }
            switch (ContainerFactory.NounTypePanel.NowPanelMode) {
                case NounType.Noun: ContainerFactory.NounList.SelectedArticleId = SelectedItemId; break;
                case NounType.Location: ContainerFactory.LocationPrepositionList.SelectedArticleId = SelectedItemId; break;
                case NounType.Person: ContainerFactory.PersonPrepositionList.SelectedArticleId = SelectedItemId; break;
            }
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }
    }
}
