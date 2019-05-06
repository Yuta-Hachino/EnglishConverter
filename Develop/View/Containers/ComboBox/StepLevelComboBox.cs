using QuickStudyEnglish.Model.Enumeration;
using QuickStudyEnglish.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.View.Containers
{
    public class StepLevelComboBox : ContainerBase
    {
        ///// <summary>
        ///// 難易度
        ///// </summary>
        public StepLevel _StepLevel = StepLevel.STEPⅠ;

        public void SetStepLevel() {
            parent.StepLevelComboBox.SelectedIndex = 0;
            _StepLevel = Util.ParseEnum<StepLevel>(parent.StepLevelComboBox.Items[parent.StepLevelComboBox.SelectedIndex].ToString());
            parent.StepLevelComboBox.SelectedIndexChanged += _StepLevelComboBox_SelectedIndexChanged;
        }

        private void _StepLevelComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            _StepLevel = Util.ParseEnum<StepLevel>(parent.StepLevelComboBox.Items[parent.StepLevelComboBox.SelectedIndex].ToString());

            // 現在のパネルモードから対象のリストを更新する
            ContainerFactory.ConvertModePanel.UpdatePanelView();
        }
    }
}
