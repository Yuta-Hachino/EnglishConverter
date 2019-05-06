using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickStudyEnglish.View.UserControls
{
    public class SlotGridView : DataGridView
    {
        public SlotGridView()
        {
            
        }

        /// <summary>
        /// フォームから参照するための変数
        /// </summary>
        public object Value { get; private set; } = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="multiSelect"></param>
        /// <param name="items"></param>
        public void Initialize(
            bool multiSelect = false,
            List<object>items = null
            )
        {


            ColumnCount = 1;                                        //1列
            Columns[0].Name = "ValueColumn";                        //列名
            RowCount = items.Count;                                 //行数はアイテム数に合わせる
            if (RowCount <= 0) return;                              //アイテム無の場合、初期化を中断
            MultiSelect = multiSelect;                              //複数選択は無し。
            for(int ct = 0; ct < RowCount; ct++)
            {
                Rows[ct].SetValues(items[ct]);                      //リストにアイテムを格納
            }
            RowHeadersVisible = false;                              //ヘッダー非表示
            ColumnHeadersVisible = false;
            Columns[0].Width = Width - 10;                          //列幅固定
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //表示位置中央

            GridColor = DefaultCellStyle.BackColor;
            MouseDown += SlotGridView_MouseDown;                    //主に選択セルのドラッグ移動の開始位置記憶
            MouseMove += SlotGridView_MouseMove;                    //ドラッグ移動の現在位置記憶
            MouseUp += SlotGridView_MouseUp;                        //セルの選択と、ドラッグ移動の終了位置記憶
            Scroll += SlotGridView_Scroll;                          //スロット回転用
            SelectionChanged += SlotGridView_SelectionChanged;      //選択セルの値決定用
        }

        private int _CenterCellIndex { get; set; } = -1;            //スロットの表示中央のセルのRowIndex

        private void SlotGridView_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void SlotGridView_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void SlotGridView_MouseMove(object sender, MouseEventArgs e)
        {
       
        }

        private void SlotGridView_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void SlotGridView_Scroll(object sender, ScrollEventArgs e)
        {
            int rowLimitHarf = DisplayedRowCount(true) / 2;
            //int limitLine = ((sender as SlotGridView).Height / 2);
            _CenterCellIndex = FirstDisplayedCell.RowIndex + rowLimitHarf;
            Rows[_CenterCellIndex].Selected = true;
            float defaultFontSize = 11.0f;
            for(int ct = FirstDisplayedCell.RowIndex; ct < _CenterCellIndex; ct++)
            {
                Rows[ct].Cells[0].Style.Font = new Font("Meiryo UI", defaultFontSize);
                Rows[ct].Height = (int)(defaultFontSize * 2);
                defaultFontSize += 2.0f;
            }

            for (int ct = _CenterCellIndex; ct < _CenterCellIndex + rowLimitHarf; ct++)
            {
                Rows[ct].Cells[0].Style.Font = new Font("Meiryo UI", defaultFontSize);
                Rows[ct].Height = (int)(defaultFontSize * 2);
                defaultFontSize -= 2.0f;
            }
        }
    }
}
