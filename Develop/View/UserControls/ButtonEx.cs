using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickStudyEnglish.View.UserControls
{
    public class ButtonEx : Button
    {
        /// <summary> フォーカス制御 </summary>
        [Browsable(true)]
        [Description("フォーカス制御有効")]
        [Category("動作")]
        public bool Selectable
        {
            get => GetStyle(ControlStyles.Selectable);
            set => SetStyle(ControlStyles.Selectable, value);
        }
    }
}
