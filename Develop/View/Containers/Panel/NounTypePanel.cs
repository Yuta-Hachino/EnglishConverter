using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Drawing;

namespace QuickStudyEnglish.View.Containers
{
    public class NounTypePanel : ContainerBase
    {
        ///// <summary>
        ///// モード別リストの展開時大きさ固定値
        ///// </summary>
        private Point _ModeListExpandSize = new Point(194, 94);
        ///// <summary>
        ///// モード別リストの閉鎖時大きさ固定値
        ///// </summary>
        private Point _ModeListClosureSize = new Point(194, 30);
        ///// <summary>
        ///// モード別リストの間隔
        ///// </summary>
        private int _ModeListMargin = 4;

        /// <summary>
        /// 現在のパネルモード
        /// </summary>
        public NounType NowPanelMode
        {
            get;
            private set;
        }
        /// <summary>
        /// 現在の単数/複数形
        /// </summary>
        public QuantityCategory _nowQuantity;


        /// <summary>
        /// パネルを初期化
        /// </summary>
        public void SetInitPanel() {
            //名詞リスト表示
            NowPanelMode = NounType.Noun;
            ContainerFactory.NounList.SetNounList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            ContainerFactory.PersonPrepositionList.SetPersonPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            ContainerFactory.LocationPrepositionList.SetLocationPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            //冠詞リスト表示
            ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            //所有格リスト表示
            ContainerFactory.PossessiveList.SetPossessiveList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);
            //単数複数ボタンのイベント処理登録
            parent.quantitySingleBtn.Click += SetQuantitySingle_ButtonClick;
            parent.quantityMultiBtn.Click += SetQuantityMulti_ButtonClick;

            parent.NounListView.ColumnClick += SetNounView;
            parent.PersonPrepositionListView.ColumnClick += SetPersonPrepositionView;
            parent.LocationPrepositionListView.ColumnClick += SetLocationPrepositionListViewView;
        }
        /// <summary>
        /// 単数形設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetQuantitySingle_ButtonClick(object sender, EventArgs e) {
            switch (NowPanelMode) {
                case NounType.Noun: ContainerFactory.NounList.NowQuantity = QuantityCategory.single; break;
                case NounType.Location: ContainerFactory.LocationPrepositionList.NowQuantity = QuantityCategory.single; break;
                case NounType.Person: ContainerFactory.PersonPrepositionList.NowQuantity = QuantityCategory.single; break;
            }
        }
        /// <summary>
        /// 複数形設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetQuantityMulti_ButtonClick(object sender, EventArgs e) {
            switch (NowPanelMode) {
                case NounType.Noun: ContainerFactory.NounList.NowQuantity = QuantityCategory.multi; break;
                case NounType.Location: ContainerFactory.LocationPrepositionList.NowQuantity = QuantityCategory.multi; break;
                case NounType.Person: ContainerFactory.PersonPrepositionList.NowQuantity = QuantityCategory.multi; break;
            }
        }

        /// <summary>
        /// 名詞を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetNounView(object sender, EventArgs e) {
            if (NowPanelMode != NounType.Noun) {
                NowPanelMode = NounType.Noun;
                ContainerFactory.NounList.SetNounFocus();
                ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                SetOpenView();
            }
        }

        /// <summary>
        /// 動詞を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetPersonPrepositionView(object sender, EventArgs e) {
            if (NowPanelMode != NounType.Person) {
                NowPanelMode = NounType.Person;
                ContainerFactory.PersonPrepositionList.SetPersonPrepositionFocus();
                ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                SetOpenView();
            }
        }

        /// <summary>
        /// 存在を設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetLocationPrepositionListViewView(object sender, EventArgs e) {
            if (NowPanelMode != NounType.Location) {
                NowPanelMode = NounType.Location;
                ContainerFactory.LocationPrepositionList.SetLocationPrepositionFocus();
                ContainerFactory.ArticleList.SetArticleList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                SetOpenView();
            }
        }

        /// <summary>
        /// 現在のパネルモードで表示を更新
        /// </summary>
        public void UpdatePanelView() {
            // 現在のパネルモードから対象のリストを更新する
            switch (NowPanelMode) {
                case NounType.Noun:
                    ContainerFactory.NounList.SetNounList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
                case NounType.Person:
                    ContainerFactory.PersonPrepositionList.SetPersonPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
                case NounType.Location:
                    ContainerFactory.LocationPrepositionList.SetLocationPrepositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
                    break;
            }
        }

        /// <summary>
        /// ビューの動きを制御
        /// </summary>
        private void SetOpenView() {
            // 各ビューのサイズを変更
            parent.NounListView.Size = new Size(NowPanelMode == NounType.Noun ? _ModeListExpandSize : _ModeListClosureSize);
            parent.PersonPrepositionListView.Size = new Size(NowPanelMode == NounType.Person ? _ModeListExpandSize : _ModeListClosureSize);
            parent.LocationPrepositionListView.Size = new Size(NowPanelMode == NounType.Location ? _ModeListExpandSize : _ModeListClosureSize);

            // リストの位置を変更
            parent.PersonPrepositionListView.Location = new Point(parent.PersonPrepositionListView.Location.X, parent.NounListView.Location.Y + parent.NounListView.Height + _ModeListMargin);
            parent.LocationPrepositionListView.Location = new Point(parent.LocationPrepositionListView.Location.X, parent.NounListView.Location.Y + parent.NounListView.Height + _ModeListMargin + parent.PersonPrepositionListView.Height + _ModeListMargin);

            //単数複数ボタンの選択状態変更
            switch (NowPanelMode) {
                case NounType.Noun: SetSelectQuantity(ContainerFactory.NounList.NowQuantity); break;
                case NounType.Location: SetSelectQuantity(ContainerFactory.LocationPrepositionList.NowQuantity); break;
                case NounType.Person: SetSelectQuantity(ContainerFactory.PersonPrepositionList.NowQuantity); break;
            }
        }

        public void SetSelectQuantity(QuantityCategory quantity) {
            if (quantity == QuantityCategory.single) {
                parent.quantitySingleBtn.Checked = true;
                parent.quantityMultiBtn.Checked = false;
            } else {
                parent.quantitySingleBtn.Checked = false;
                parent.quantityMultiBtn.Checked = true;
            }

        }
    }
}
