using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Drawing;

namespace QuickStudyEnglish.View.Containers
{
    public class ConvertModePanel : ContainerBase
    {
        ///// <summary>
        ///// モード別リストの展開時大きさ固定値
        ///// </summary>
        private Point _ModeListExpandSize = new Point(297, 230);
        ///// <summary>
        ///// モード別リストの閉鎖時大きさ固定値
        ///// </summary>
        private Point _ModeListClosureSize = new Point(297, 30);
        ///// <summary>
        ///// モード別リストの間隔
        ///// </summary>
        private int _ModeListMargin = 9;

        /// <summary>
        /// 現在のパネルモード
        /// </summary>
        public TargetMode NowPanelMode
        {
            get;
            private set;
        }

        /// <summary>
        /// パネルを初期化
        /// </summary>
        public void SetInitPanel() {
            NowPanelMode = TargetMode.Adjective;

            ContainerFactory.AdjectiveList.SetAdjectiveList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            ContainerFactory.VerbList.SetVerbList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            ContainerFactory.BeingList.SetBeingList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);

            parent.AdjectiveListView.ColumnClick += SetAdjectiveView;
            parent.VerbListView.ColumnClick += SetVerbView;
            parent.BeingListView.ColumnClick += SetBeingView;
        }

        /// <summary>
        /// 形容詞を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetAdjectiveView(object sender, EventArgs e) {
            if (NowPanelMode != TargetMode.Adjective) {
                NowPanelMode = TargetMode.Adjective;
                ContainerFactory.AdjectiveList.SetAdjectiveFocus();

                SetOpenView();
                // 現在のパネルモードから対象のリストを更新する
                ContainerFactory.ConvertModePanel.UpdatePanelView();
                // セット
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();

            }
        }

        /// <summary>
        /// 動詞を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetVerbView(object sender, EventArgs e) {
            if (NowPanelMode != TargetMode.Verb) {
                NowPanelMode = TargetMode.Verb;
                ContainerFactory.VerbList.SetVerbFocus();

                SetOpenView();
                // 現在のパネルモードから対象のリストを更新する
                ContainerFactory.ConvertModePanel.UpdatePanelView();
                // セット
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();

            }
        }

        /// <summary>
        /// 存在を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetBeingView(object sender, EventArgs e) {
            if (NowPanelMode != TargetMode.Being) {
                NowPanelMode = TargetMode.Being;
                ContainerFactory.BeingList.SetBeingFocus();

                SetOpenView();
                // 現在のパネルモードから対象のリストを更新する
                ContainerFactory.ConvertModePanel.UpdatePanelView();
                // セット
                ContainerFactory.ResultTextBoxPanel.UpdateResultTextBoxPanel();

            }
        }

        /// <summary>
        /// 現在のパネルモードで表示を更新
        /// </summary>
        public void UpdatePanelView() {
            // 現在のパネルモードから対象のリストを更新する
            switch (ContainerFactory.ConvertModePanel.NowPanelMode) {
                case TargetMode.Adjective:
                    ContainerFactory.AdjectiveList.SetAdjectiveList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
                case TargetMode.Verb:
                    ContainerFactory.VerbList.SetVerbList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
                case TargetMode.Being:
                    ContainerFactory.BeingList.SetBeingList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
            }
        }

        /// <summary>
        /// ビューの動きを制御
        /// </summary>
        private void SetOpenView() {
            // 各ビューのサイズを変更
            parent.AdjectiveListView.Size = new Size(NowPanelMode == TargetMode.Adjective ? _ModeListExpandSize : _ModeListClosureSize);
            parent.VerbListView.Size = new Size(NowPanelMode == TargetMode.Verb ? _ModeListExpandSize : _ModeListClosureSize);
            parent.BeingListView.Size = new Size(NowPanelMode == TargetMode.Being ? _ModeListExpandSize : _ModeListClosureSize);

            // リストの位置を変更
            parent.VerbListView.Location = new Point(parent.VerbListView.Location.X, parent.AdjectiveListView.Location.Y + parent.AdjectiveListView.Height + _ModeListMargin);
            parent.BeingListView.Location = new Point(parent.BeingListView.Location.X, parent.AdjectiveListView.Location.Y + parent.AdjectiveListView.Height + _ModeListMargin + parent.VerbListView.Height + _ModeListMargin);

        }
    }
}
