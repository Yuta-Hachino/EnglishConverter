using System;
using System.Windows.Forms;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using QuickStudyEnglish.Model.Enumeration;
using System.Linq;

namespace QuickStudyEnglish.View.Containers
{
    public class VerbListView : ContainerBase
    {
        /// <summary>
        /// 自身の選択されたアイテムインデックス
        /// </summary>
        public int SelectedItemIndex
        {
            get;
            private set;
        }

        public Verb SelectedItem
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
        public int SelectedVerbGroupId
        {
            get {
                return MasterFactory.GetMasterData<VerbMaster>().GetGroupId(SelectedItem.Id);
            }

        }

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        public List<int> nowSelectedItemIdList = new List<int>();
        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<Verb> nowSelectedItemList = new List<Verb>();

        //人称指定が効いていない。初期表示で形容詞語尾でなく動詞語尾が表示される。
        public void SetVerbList(JapaneseReadType type, bool init = false) {

            parent.VerbListView.SelectedIndexChanged -= SetSelectedItemIndex;
            int selectIndex = (parent.VerbListView.SelectedItems.Count <= 0) ? 0 : parent.VerbListView.SelectedIndices[0];


            // 内部要素をクリア
            parent.VerbListView.Items.Clear();
            nowSelectedItemList.Clear();
            nowSelectedItemIdList.Clear();

            // 主語の種別に合致する形容詞リストを作成
            //主語リストの初期化
            List<List<string>> master = new List<List<string>>();
            nowSelectedItemList = MasterFactory.GetMasterData<VerbMaster>().GetMixCreOrObjAndCategorySubject(ContainerFactory.SubjectList._Subject, ContainerFactory.SubjectList._PersonalCategory);
            nowSelectedItemIdList = nowSelectedItemList.Select(indexer => indexer.Id).ToList();
            master = MasterFactory.GetMasterData<VerbMaster>().GetRangeData(nowSelectedItemIdList, type);

            //リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.VerbListView.Items.Add(addItem);
            }

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                SelectedItem = nowSelectedItemList[SelectedItemIndex];
                parent.VerbListView.Items[0].Selected = true;
                parent.VerbListView.Click += VerbListView_Click;
                parent.VerbListView.SelectedIndexChanged += SetSelectedItemIndex;
            } else {
                if (parent.VerbListView.Items.Count >= selectIndex - 1) {
                    parent.VerbListView.Items[selectIndex].Selected = true;
                }
                parent.VerbListView.SelectedIndexChanged += SetSelectedItemIndex;
                // 語尾もセット
                SetSelectedItemIndex(null, null);
            }
        }

        public void SetVerbFocus() {
            if (parent.VerbListView.Items.Count <= 0) {
                SetVerbList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if(parent.VerbListView.Items.Count > 0) {
                parent.VerbListView.Items[SelectedItemIndex].Focused = true;
                parent.VerbListView.Items[SelectedItemIndex].Selected = true;

                // 語尾もセット
                ContainerFactory.EndOfWordListView.SetVerbEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }
        }
        public void VerbListView_Click(object sender, EventArgs e) {
            if (SelectedItemIndex != parent.VerbListView.SelectedIndices[0]) return;
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];
            if (parent.VerbListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.VerbListView.SelectedIndices[0];
                SelectedItem = nowSelectedItemList[parent.VerbListView.SelectedIndices[0]];
            }

            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetVerbEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //変換結果出力
            ContainerFactory.EndOfWordListView.VerbEndOfWordList_SelectedIndexChanged();

        }
        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItem = nowSelectedItemList[0];
            if (parent.VerbListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.VerbListView.SelectedIndices[0];
                SelectedItem = nowSelectedItemList[parent.VerbListView.SelectedIndices[0]];
            }

            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetVerbEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //変換結果出力
            ContainerFactory.EndOfWordListView.VerbEndOfWordList_SelectedIndexChanged();

        }
    }
}
