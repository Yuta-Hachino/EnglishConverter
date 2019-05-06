using QuickStudyEnglish.Model.File;
using System.Collections.Generic;

namespace QuickStudyEnglish.Model.Master
{
    public abstract class BaseMaster : ReadDelimiterFilesConfig
    {
        public abstract void SetData(List<string> _masterTextList);
        public abstract void ResetData();
    }
}
