using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;

namespace QuickStudyEnglish.View.Containers
{
    public class EndOfWordListView : ContainerBase
    {
        ///// <summary>
        ///// モード別リストの展開時大きさ固定値
        ///// </summary>
        private Point _ModeListExpandSize = new Point(309, 272);
        ///// <summary>
        ///// モード別リストの閉鎖時大きさ固定値
        ///// </summary>
        private Point _ModeListClosureSize = new Point(272, 30);

        /// <summary>
        /// 現在表示されている形容詞語尾アイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedAdjectiveItemIdList = new List<int>();

        /// <summary>
        /// 現在表示されている形容詞語尾後付けアイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedAdjectiveAddItemIdList = new List<int>();

        /// <summary>
        /// 現在表示されている動詞語尾アイテムのIDリスト
        /// </summary>
        private List<int> nowSelectedVerbItemIdList = new List<int>();

        public void SetEndOfWordList(bool init = false) {
            if (init) {
                parent.EndOfWordListView.SelectedIndexChanged += EndOfWordList_SelectedIndexChanged;
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemIndex = 0;
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemId = nowSelectedAdjectiveItemIdList[ContainerFactory.AdjectiveList.SelectedEndOfWordItemIndex];

                //英文を出力
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
            } else {
                switch (ContainerFactory.ConvertModePanel.NowPanelMode) {
                    case TargetMode.Adjective:
                        SetAdjectiveEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                        break;
                    case TargetMode.Verb:
                        SetVerbEndOfWordList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                        break;
                    case TargetMode.Being:
                        SetBeingEndOfWordList();
                        break;
                }
            }
        }
        public void SetAdjectiveEndOfWordList(JapaneseReadType type) {
            //リストのSelectedIndexChangedイベントハンドラは、オペレーターによる操作時のみ実行させる。
            parent.EndOfWordListView.SelectedIndexChanged -= EndOfWordList_SelectedIndexChanged;
            //リストのクリア時に選択状態も解除されるため。
            int selectIndex = (parent.EndOfWordListView.SelectedItems.Count <= 0) ? 0 : parent.EndOfWordListView.SelectedIndices[0];
            parent.EndOfWordListView.Items.Clear();
            nowSelectedAdjectiveItemIdList.Clear();

            //リストに入れるマスタデータの取得
            List<List<string>> master = new List<List<string>>();
            if (ContainerFactory.SubjectList._PersonalCategory == PersonalPronounCategory.ThirdSingle) {
                master = _SetAdjectiveEndWordMaster(type);
            } else {
                master = _SetAdjectiveEndWordMaster(type, true);
            }


            //語尾リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.EndOfWordListView.Items.Add(addItem);
                nowSelectedAdjectiveItemIdList.Add(items.index + 1);
            }

            if (parent.EndOfWordListView.Items.Count > selectIndex && parent.EndOfWordListView.Items.Count > 0) {
                parent.EndOfWordListView.Items[selectIndex].Selected = true;
            }
            parent.EndOfWordListView.Click += EndOfWordList_Click;
            parent.EndOfWordListView.SelectedIndexChanged += EndOfWordList_SelectedIndexChanged;

            SetOpenView();
        }
        public void _SetAdjectiveEndOfWordList(JapaneseReadType type) {
            //リストのSelectedIndexChangedイベントハンドラは、オペレーターによる操作時のみ実行させる。
            parent.EndOfWordListView.SelectedIndexChanged -= EndOfWordList_SelectedIndexChanged;
            //リストのクリア時に選択状態も解除されるため。
            int selectIndex = (parent.EndOfWordListView.SelectedItems.Count <= 0) ? 0 : parent.EndOfWordListView.SelectedIndices[0];
            parent.EndOfWordListView.Items.Clear();
            nowSelectedAdjectiveItemIdList.Clear();

            //リストに入れるマスタデータの取得
            List<List<string>> master = new List<List<string>>();
            if (ContainerFactory.SubjectList._PersonalCategory == PersonalPronounCategory.ThirdSingle) {
                master = _SetAdjectiveEndWordMaster(type);
            } else {
                master = _SetAdjectiveEndWordMaster(type, true);
            }

            //語尾リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.EndOfWordListView.Items.Add(addItem);
                nowSelectedAdjectiveItemIdList.Add(items.index + 1);
            }

            if (parent.EndOfWordListView.Items.Count > selectIndex - 1 && parent.EndOfWordListView.Items.Count > 0) {
                parent.EndOfWordListView.Items[selectIndex].Selected = true;
            }
            parent.EndOfWordListView.Click += EndOfWordList_Click;
            parent.EndOfWordListView.SelectedIndexChanged += EndOfWordList_SelectedIndexChanged;

            SetOpenView();
        }

        private List<List<string>> _SetAdjectiveEndWordMaster(JapaneseReadType type, bool numberOfPeople = false) {
            List<List<string>> masterString = null;
            List<int> endWordMasterId = ((ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP) ?
                                            MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeIdGroupByGroupIdAndStepLevel(
                                                ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId, 
                                                ContainerFactory.StepLevelComboBox._StepLevel,
                                                ContainerFactory.AdjectiveList.SelectedItem.Category)
                                            : MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeIdGroupByGroupId(
                                                ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId,
                                                ContainerFactory.AdjectiveList.SelectedItem.Category)
                                            ).ToList();
                    masterString = MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>()
                                            .GetRangeData(endWordMasterId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            //人数語尾（3人称以外）
            if (numberOfPeople) {
                masterString = _ConvertNumberOfPeople(masterString, endWordMasterId);
            } else {
                //後付け語尾
                for (int addWordCount = 0; addWordCount < masterString.Count; addWordCount++) {
                    List<string> addWord = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(endWordMasterId[addWordCount], 0, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    masterString[addWordCount][0] += (" " + addWord[0]).Replace("  ", " ");
                    masterString[addWordCount][1] = addWord[1] + masterString[addWordCount][1];
                }
            }
            
            return masterString;
        }
        /// <summary>
        /// 人数語尾マスタ置換処理
        /// </summary>
        /// <returns>置換語マスタ</returns>
        private List<List<string>> _ConvertNumberOfPeople(List<List<string>> master, List<int>masterId) {
            List<string> numberOfPeopleMaster = MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishWordList(
                        ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP 
                        ? MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishIdGroupByGroupIdAndPersonCategoryAndStepLevel(
                            ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId, 
                            ContainerFactory.SubjectList._PersonalCategory, 
                            ContainerFactory.StepLevelComboBox._StepLevel,
                            ContainerFactory.AdjectiveList.SelectedItem.Category)
                        : MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishIdGroupByGroupIdAndPersonCategory(
                            ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId, 
                            ContainerFactory.SubjectList._PersonalCategory,
                            ContainerFactory.AdjectiveList.SelectedItem.Category));

            for (int counter = 0; counter < master.Count; counter++) {
                if (numberOfPeopleMaster.Count > counter) {
                    master[counter][0] = numberOfPeopleMaster[counter];
                    List<string> addWord = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(masterId[counter], 0, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    master[counter][0] += (" " + addWord[0]).Replace("  ", " ");
                    master[counter][1] = addWord[1] + master[counter][1];

                }
            }
            return master;
        }
        public void SetVerbEndOfWordList(JapaneseReadType type) {
            //リストのSelectedIndexChangedイベントハンドラは、オペレーターによる操作時のみ実行させる。
            parent.EndOfWordListView.Click -= EndOfWordList_Click;
            parent.EndOfWordListView.SelectedIndexChanged -= EndOfWordList_SelectedIndexChanged;
            //リストのクリア時に選択状態も解除されるため。
            int selectIndex = (parent.EndOfWordListView.SelectedItems.Count <= 0) ? 0 : parent.EndOfWordListView.SelectedIndices[0];
            parent.EndOfWordListView.Items.Clear();
            nowSelectedVerbItemIdList.Clear();

            //リストに入れるマスタデータの取得
            List<List<string>> master = _SetVerbEndWordMaster(type);

            //語尾リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                ListViewItem addItem = new ListViewItem(items.value[0]);
                addItem.SubItems.Add(items.value[1]);
                parent.EndOfWordListView.Items.Add(addItem);
                nowSelectedVerbItemIdList.Add(items.index + 1);
            }

            if (parent.EndOfWordListView.Items.Count > selectIndex && parent.EndOfWordListView.Items.Count > 0) {
                parent.EndOfWordListView.Items[selectIndex].Selected = true;
            }
            parent.EndOfWordListView.Click += EndOfWordList_Click;
            parent.EndOfWordListView.SelectedIndexChanged += EndOfWordList_SelectedIndexChanged;

            SetOpenView();
        }
        public List<List<string>> _SetVerbEndWordMaster(JapaneseReadType type) {
            List<List<string>> masterString = null;
            List<VerbEndOfWord> endWordMaster = ((ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP) ?
                                            MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupIdAndStepLevel(ContainerFactory.VerbList.SelectedVerbGroupId, ContainerFactory.StepLevelComboBox._StepLevel)
                                            : MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupId(ContainerFactory.VerbList.SelectedVerbGroupId)
                                            ).ToList();
            List<int> endWordMasterId = endWordMaster.Select(indexer => indexer.Id).ToList();
                    masterString = MasterFactory.GetMasterData<VerbEndOfWordMaster>()
                                            .GetRangeData(endWordMasterId, type);

            ////後付け語尾
            for (int addWordCount = 0; addWordCount < masterString.Count; addWordCount++) {
                List<string> addWord = MasterFactory.GetMasterData<VerbAddEndOfWordMaster>().GetGroupByGroupIdData(endWordMasterId[addWordCount], ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                masterString[addWordCount][0] += (" " + addWord[0]).Replace("  ", " ");
                masterString[addWordCount][1] = addWord[1] + masterString[addWordCount][1];
            }
            SetOpenView();

            return masterString;
        }

        public void SetBeingEndOfWordList() {
            //英文を出力
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
            SetOpenView();
        }
        public void EndOfWordList_Click(object sender, EventArgs e) {
            switch (ContainerFactory.ConvertModePanel.NowPanelMode) {
                case TargetMode.Adjective:
                    if (ContainerFactory.AdjectiveList.SelectedEndOfWordItemIndex != parent.EndOfWordListView.SelectedIndices[0]) return;
                    AdjectiveEndOfWordList_SelectedIndexChanged();
                    break;

                case TargetMode.Verb:
                    if (ContainerFactory.VerbList.SelectedEndOfWordItemIndex != parent.EndOfWordListView.SelectedIndices[0]) return;
                    VerbEndOfWordList_SelectedIndexChanged();
                    break;

            }
        }

        public void EndOfWordList_SelectedIndexChanged(object sender, EventArgs e) {
            switch (ContainerFactory.ConvertModePanel.NowPanelMode) {
                case TargetMode.Adjective:
                    AdjectiveEndOfWordList_SelectedIndexChanged();
                    break;

                case TargetMode.Verb:
                    VerbEndOfWordList_SelectedIndexChanged();
                    break;

            }
        }


        public void AdjectiveEndOfWordList_SelectedIndexChanged() {            
            if (parent.EndOfWordListView.SelectedIndices.Count > 0) {
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemIndex = parent.EndOfWordListView.SelectedIndices[0];
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemId = nowSelectedAdjectiveItemIdList[parent.EndOfWordListView.SelectedIndices[0]];
            } else {
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemIndex = 0;
                ContainerFactory.AdjectiveList.SelectedEndOfWordItemId = 0;
            }

            //英文を出力
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        public void VerbEndOfWordList_SelectedIndexChanged() {
            ContainerFactory.VerbList.SelectedEndOfWordItemIndex = 0;
            ContainerFactory.VerbList.SelectedEndOfWordItemId = nowSelectedVerbItemIdList[0];

            if (parent.EndOfWordListView.SelectedIndices.Count > 0) {
                ContainerFactory.VerbList.SelectedEndOfWordItemIndex = parent.EndOfWordListView.SelectedIndices[0];
                ContainerFactory.VerbList.SelectedEndOfWordItemId = nowSelectedVerbItemIdList[parent.EndOfWordListView.SelectedIndices[0]];
            }

            //英文を出力
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        /// <summary>
        /// ビューの動きを制御
        /// </summary>
        private void SetOpenView() {
            // ビューのサイズを変更
            //parent.EndOfWordListView.Size = new Size((ContainerFactory.ConvertModePanel.NowPanelMode == TargetMode.Adjective || ContainerFactory.ConvertModePanel.NowPanelMode == TargetMode.Verb) ? _ModeListExpandSize : _ModeListClosureSize);
            //形容詞/動詞選択時、語尾リスト表示。その他は非表示。
            if(ContainerFactory.ConvertModePanel.NowPanelMode == TargetMode.Adjective || ContainerFactory.ConvertModePanel.NowPanelMode == TargetMode.Verb) {
                parent.EndOfWordListView.Enabled = true;
            } else {
                parent.EndOfWordListView.Items.Clear();
                parent.EndOfWordListView.Enabled = false;
            }

        }
    }
}
