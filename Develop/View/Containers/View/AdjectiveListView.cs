using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master.MasterData;
using QuickStudyEnglish.Model.Master;

namespace QuickStudyEnglish.View.Containers
{
    public class AdjectiveListView : ContainerBase
    {
        /// <summary>
        /// 自身の選択されたアイテムインデックス
        /// </summary>
        public int SelectedItemIndex
        {
            get;
            private set;
        }

        public Adjective SelectedItem
        {
            get;
            private set;
        }

        /// <summary>
        /// 語尾で選択されたアイテムインデックス
        /// </summary>
        public int SelectedEndOfWordItemIndex
        {
            get;
            set;
        }

        public int SelectedEndOfWordItemId
        {
            get;
            set;
        }

        /// <summary>
        /// 選択されているアイテムのグループID
        /// </summary>
        public int SelectedAdjectiveGroupId
        {
            get
            {
                return MasterFactory.GetMasterData<AdjectiveMaster>().GetGroupId(SelectedItem.Id);
            }
        }

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedItemIdList = new List<int>();

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<Adjective> nowSelectedItemList = new List<Adjective>();


        public void SetAdjectiveList(JapaneseReadType type, bool init = false) {
            parent.AdjectiveListView.SelectedIndexChanged -= SetSelectedItemIndex;
            int selectIndex = (parent.AdjectiveListView.SelectedItems.Count <= 0) ? 0 : parent.AdjectiveListView.SelectedIndices[0];

            // 内部要素をクリア
            parent.AdjectiveListView.Items.Clear();
            nowSelectedItemIdList.Clear();
            nowSelectedItemList.Clear();
            // 主語の種別に合致する形容詞リストを作成
            //主語リストの初期化
            List<Adjective> master = new List<Adjective>();
            List<List<string>> masterWord = new List<List<string>>();
            master = MasterFactory.GetMasterData<AdjectiveMaster>().GetMixCreOrObjAndCategorySubject(ContainerFactory.SubjectList._Subject, ContainerFactory.SubjectList._PersonalCategory);
            masterWord = MasterFactory.GetMasterData<AdjectiveMaster>().GetRangeData(master.Select(indexer => indexer.Id), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //リストの初期化
            foreach (var items in masterWord.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.AdjectiveListView.Items.Add(addItem);
                nowSelectedItemIdList.Add(items.index + 1);
            }
            nowSelectedItemList = master;

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                SelectedItem = nowSelectedItemList[SelectedItemIndex];
                parent.AdjectiveListView.Items[0].Selected = true;
                parent.AdjectiveListView.Click += AdjectiveListView_Click;
            }
            else {
                if (parent.AdjectiveListView.Items.Count >= selectIndex - 1) {
                    parent.AdjectiveListView.Items[selectIndex].Selected = true;
                }
            }
            parent.AdjectiveListView.SelectedIndexChanged += SetSelectedItemIndex;


            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
        }

        public void SetAdjectiveFocus() {
            if(parent.AdjectiveListView.Items.Count <= 0) {
                SetAdjectiveList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if(parent.AdjectiveListView.Items.Count > 0) {
                parent.AdjectiveListView.Items[SelectedItemIndex].Focused = true;
                parent.AdjectiveListView.Items[SelectedItemIndex].Selected = true;

                // 語尾もセット
                ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }
        }

        public void AdjectiveListView_Click(object sender, EventArgs e) {
            if (SelectedItemIndex != parent.AdjectiveListView.SelectedIndices[0]) return;
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];

            if (parent.AdjectiveListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.AdjectiveListView.SelectedIndices[0];
                SelectedItem = nowSelectedItemList[parent.AdjectiveListView.SelectedIndices[0]];
            }


            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //変換結果出力
            ContainerFactory.EndOfWordListView.AdjectiveEndOfWordList_SelectedIndexChanged();
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];

            if (parent.AdjectiveListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.AdjectiveListView.SelectedIndices[0];
                SelectedItem = nowSelectedItemList[parent.AdjectiveListView.SelectedIndices[0]];
            }
            

            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //変換結果出力
            ContainerFactory.EndOfWordListView.AdjectiveEndOfWordList_SelectedIndexChanged();
        }
    }
}
