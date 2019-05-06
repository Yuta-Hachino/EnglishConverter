using QuickStudyEnglish.Model.Enumeration;
using QuickStudyEnglish.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.View.Containers
{
    public class JapaneseReadTypeComboBox : ContainerBase
    {
        ///// <summary>
        ///// 難易度
        ///// </summary>
        public JapaneseReadType _JapaneseReadType = JapaneseReadType.KANJI;

        public void SetJapaneseReadType() {
            parent.JapaneseReadTypeComboBox.SelectedIndex = 0;
            //_JapaneseReadType = Util.ParseEnum<JapaneseReadType>(parent.JapaneseReadTypeComboBox.Items[parent.JapaneseReadTypeComboBox.SelectedIndex].ToString());
            _JapaneseReadType = (JapaneseReadType)parent.JapaneseReadTypeComboBox.SelectedIndex;
            parent.JapaneseReadTypeComboBox.SelectedIndexChanged += _JapaneseReadTypeComboBox_SelectedIndexChanged;
        }

        private void _JapaneseReadTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            //_JapaneseReadType = Util.ParseEnum<JapaneseReadType>(parent.JapaneseReadTypeComboBox.Items[parent.JapaneseReadTypeComboBox.SelectedIndex].ToString());
            _JapaneseReadType = (JapaneseReadType)parent.JapaneseReadTypeComboBox.SelectedIndex;
            // 現在のパネルモードから対象のリストを更新する
            ContainerFactory.ConvertModePanel.UpdatePanelView();
        }
    }
}
