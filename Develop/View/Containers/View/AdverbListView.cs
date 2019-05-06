using QuickStudyEnglish.Model.Enumeration;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using System.Linq;

namespace QuickStudyEnglish.View.Containers
{
    public class AdverbListView : ContainerBase
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

        public void SetAdverbList(JapaneseReadType type, AdverbPosition pos, bool init = false) {
            // 内部要素をクリア
            parent.AdverbListView.Items.Clear();

            nowSelectedItemIdList.Clear();

            List<List<string>> master = new List<List<string>>();

                    master = MasterFactory.GetMasterData<AdverbMaster>().GetRangeData(pos, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //副詞リストの初期化
            foreach(var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.AdverbListView.Items.Add(addItem);
                nowSelectedItemIdList.Add(items.index);
            }

            // 初期セットの場合は選択状態も設定
            if (init) {
                SelectedItemIndex = 0;
                parent.AdverbListView.Items[0].Selected = true;
                parent.AdverbListView.SelectedIndexChanged += SetSelectedItemIndex;
            }
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.AdverbListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.AdverbListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.AdverbListView.SelectedIndices[0]];
            }

            // 選択された英単語の英文と日本文を表示
            if(SelectedItemIndex > 0) {
                // セット
                switch(ContainerFactory.ConvertModePanel.NowPanelMode) {
                    case TargetMode.Adjective: ContainerFactory.EndOfWordListView.SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType); break;
                    case TargetMode.Verb: ContainerFactory.EndOfWordListView.SetVerbEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType); break;
                    case TargetMode.Being: ContainerFactory.EndOfWordListView.SetBeingEndOfWordList(); break;
                }

            }
        }

    }
}
