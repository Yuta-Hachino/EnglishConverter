using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;

namespace QuickStudyEnglish.View.Containers
{
    public class BeingListView : ContainerBase
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

        public int SelectedBeingItemId
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在表示されているアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedItemIdList = new List<int>();
        public void SetBeingList(JapaneseReadType type, bool init = false) {
            // 内部要素をクリア
            parent.BeingListView.Items.Clear();
            nowSelectedItemIdList.Clear();
            List<List<string>> masterString = null;
            List<BeingWord> tempWord = (ContainerFactory.StepLevelComboBox._StepLevel == StepLevel.ALLSTEP) ?
                MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounAndSubjectMaster(
                    ContainerFactory.SubjectList._PersonalCategory
                    , ContainerFactory.SubjectList._Subject).ToList()
                : MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounSubjectAndStepLevelMaster(
                    ContainerFactory.StepLevelComboBox._StepLevel
                    , ContainerFactory.SubjectList._PersonalCategory
                    , ContainerFactory.SubjectList._Subject).ToList();
            masterString = MasterFactory.GetMasterData<BeingWordMaster>()
                                    .GetRangeData(tempWord.Select(n => n.Id), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            //後付け語尾
            for (int addWordCount = 0; addWordCount < masterString.Count; addWordCount++) {
                masterString[addWordCount][0] += (" " + ((MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord[addWordCount].Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType).Count <= 0)
                    ? string.Empty
                    : MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord[addWordCount].Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0][0]))
                    .Replace("  ", " ");
                masterString[addWordCount][1] = ((MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord[addWordCount].Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType).Count <= 0)
                    ? string.Empty
                    : MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord[addWordCount].Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0][1]) + masterString[addWordCount][1];
            }

            //主語リストの初期化
            foreach (var items in masterString.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.BeingListView.Items.Add(addItem);
                nowSelectedItemIdList.Add(items.index + 1);
            }

            if (init) {
                SelectedItemIndex = 0;
            }
                parent.BeingListView.Click += BeingListView_Click;
            parent.BeingListView.SelectedIndexChanged += SetSelectedItemIndex;
        }

        public void SetBeingFocus() {
            if (parent.BeingListView.Items.Count <= 0) {
                SetBeingList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            }

            if(parent.BeingListView.Items.Count > 0) {
                parent.BeingListView.Items[SelectedItemIndex].Focused = true;
                parent.BeingListView.Items[SelectedItemIndex].Selected = true;
            }
        }

        public void BeingListView_Click(object sender, EventArgs e) {
            if (SelectedItemIndex != parent.BeingListView.SelectedIndices[0]) return;
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.BeingListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.BeingListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.BeingListView.SelectedIndices[0]];
            }

            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetBeingEndOfWordList();
        }

        public void SetSelectedItemIndex(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.BeingListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.BeingListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.BeingListView.SelectedIndices[0]];
            }
            
            // 語尾もセット
            ContainerFactory.EndOfWordListView.SetBeingEndOfWordList();
        }
    }
}
