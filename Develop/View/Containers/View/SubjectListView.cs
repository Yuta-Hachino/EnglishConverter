using QuickStudyEnglish.Model.Enumeration;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuickStudyEnglish.View.Containers
{
    public class SubjectListView : ContainerBase
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
        /// 選択されているてにをは
        /// </summary>
        public string SelectedTENIOHA
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在選択されている主語種別
        /// </summary>
        public SubjectCategory _Subject
        {
            get; private set;
        }

        /// <summary>
        /// 現在選択されている主語の人称種別
        /// </summary>
        public PersonalPronounCategory _PersonalCategory
        {
            get; private set;
        }

        public ConstReason _ConstReason
        {
            get; private set;
        }
        private List<int> nowSelectedItemIdList = new List<int>();

        public void SetSubjectList(JapaneseReadType type, bool init = false) {
            // 内部要素をクリア
            parent.SubjectListView.Items.Clear();
            nowSelectedItemIdList.Clear();

            //主語リストの初期化
            List<List<string>> master = new List<List<string>>();

            switch (type) {
                case JapaneseReadType.HIRAGANA:
                    master = MasterFactory.GetMasterData<SubjectMaster>().GetCreatureOrObjectSubjctHiragana(SubjectCategory.None);
                    break;

                case JapaneseReadType.FURIGANA:
                    master = MasterFactory.GetMasterData<SubjectMaster>().GetCreatureOrObjectSubjctFurigana(SubjectCategory.None);
                    break;

                case JapaneseReadType.KANJI:
                default:
                    master = MasterFactory.GetMasterData<SubjectMaster>().GetCreatureOrObjectSubjctKanji(SubjectCategory.None);
                    break;
            }

            //主語リストの初期化
            foreach (var items in master.Select((value, index) => new { index, value })) {
                if(items.index > 0) {
                    ListViewItem addItem = new ListViewItem(items.value[0]);
                    addItem.SubItems.Add(items.value[1]);
                    parent.SubjectListView.Items.Add(addItem);
                    nowSelectedItemIdList.Add(items.index);
                }
            }

            // 初期セットの場合は選択状態も設定
            if (init) {
                parent.SubjectListView.Items[0].Selected = true;
                SelectedTENIOHA = "は";
                _Subject = SubjectCategory.Creature;
                _PersonalCategory = PersonalPronounCategory.First;
                _ConstReason = ConstReason.None;
                SelectedItemIndex = 0;
                SelectedItemId = nowSelectedItemIdList[SelectedItemIndex];
                parent.SubjectListView.SelectedIndexChanged += _SubjectListView_SelectedIndexChanged;
            }
        }

        public void _SubjectListView_Click(string tenioha) {
            if (parent.SubjectListView.SelectedIndices[0] != SelectedItemIndex) return;
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.SubjectListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.SubjectListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.SubjectListView.SelectedIndices[0]];
            }

            SelectedTENIOHA = tenioha;
            _Subject = (SubjectCategory)(MasterFactory.GetMasterData<SubjectMaster>().GetCategoryData(SelectedItemId)[0]);
            _PersonalCategory = (PersonalPronounCategory)(MasterFactory.GetMasterData<SubjectMaster>().GetCategoryData(SelectedItemId)[1]);
            _ConstReason = ConstReason.None;

            // 現在のパネルモードから対象のリストを更新する
            ContainerFactory.ConvertModePanel.UpdatePanelView();
            // セット
            ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
        }

        private void _SubjectListView_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedItemIndex = 0;
            SelectedItemId = nowSelectedItemIdList[0];
            if (parent.SubjectListView.SelectedIndices.Count > 0) {
                SelectedItemIndex = parent.SubjectListView.SelectedIndices[0];
                SelectedItemId = nowSelectedItemIdList[parent.SubjectListView.SelectedIndices[0]];
                _Subject = (SubjectCategory)(MasterFactory.GetMasterData<SubjectMaster>().GetCategoryData(SelectedItemId)[0]);
                _PersonalCategory = (PersonalPronounCategory)(MasterFactory.GetMasterData<SubjectMaster>().GetCategoryData(SelectedItemId)[1]);
                _ConstReason = ConstReason.None;
                // 現在のパネルモードから対象のリストを更新する
                ContainerFactory.ConvertModePanel.UpdatePanelView();
                // セット
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();
            }
        }
    }
}
