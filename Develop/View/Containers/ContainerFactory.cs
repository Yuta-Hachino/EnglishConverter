namespace QuickStudyEnglish.View.Containers
{
    public class ContainerBase
    {
        public static MainForm parent;
    }

    public class ContainerFactory
    {
        // 難易度リスト
        private static StepLevelComboBox _stepLevelComboBox = new StepLevelComboBox();
        public static StepLevelComboBox StepLevelComboBox
        {
            get { return _stepLevelComboBox; }
        }
        // 仮名リスト
        private static JapaneseReadTypeComboBox _japaneseReadTypeComboBox = new JapaneseReadTypeComboBox();
        public static JapaneseReadTypeComboBox JapaneseReadTypeComboBox
        {
            get { return _japaneseReadTypeComboBox; }
        }

        // 主語リスト
        private static SubjectListView _subjectList = new SubjectListView();
        public static SubjectListView SubjectList {
            get { return _subjectList; }
        }

        // 形容詞、動詞、存在を表示するパネル
        private static ConvertModePanel _convertModePanel = new ConvertModePanel();
        public static ConvertModePanel ConvertModePanel{
            get { return _convertModePanel; }
        }

        // 形容詞リスト
        private static AdjectiveListView _adjectiveList = new AdjectiveListView();
        public static AdjectiveListView AdjectiveList {
            get { return _adjectiveList; }
        }

        // 動詞リスト
        private static VerbListView _verbList = new VerbListView();
        public static VerbListView VerbList
        {
            get { return _verbList; }
        }

        // 存在リスト
        private static BeingListView _beingList = new BeingListView();
        public static BeingListView BeingList
        {
            get { return _beingList; }
        }

        // 語尾リスト
        private static EndOfWordListView _endOfWordListView = new EndOfWordListView();
        public static EndOfWordListView EndOfWordListView
        {
            get { return _endOfWordListView; }
        }

        // 副詞リスト
        private static AdverbListView _adverbList = new AdverbListView();
        public static AdverbListView AdverbList {
            get { return _adverbList; }
        }

        // 名詞リスト
        private static NounListView _nounList = new NounListView();
        public static NounListView NounList
        {
            get { return _nounList; }
        }

        // 前置詞リスト
        private static PrePositionsListView _prePositionList = new PrePositionsListView();
        public static PrePositionsListView PrePositionList
        {
            get { return _prePositionList; }
        }
        // 名詞リスト
        private static PersonPrepositionListView _personPrepositionList = new PersonPrepositionListView();
        public static PersonPrepositionListView PersonPrepositionList
        {
            get { return _personPrepositionList; }
        }
        
        // 名詞リスト
        private static LocationPrepositionListView _locationPrepositionList = new LocationPrepositionListView();
        public static LocationPrepositionListView LocationPrepositionList
        {
            get { return _locationPrepositionList; }
        }
        // 名詞(一般、人、場所)を表示するパネル
        private static NounTypePanel _nounTypePanel = new NounTypePanel();
        public static NounTypePanel NounTypePanel
        {
            get { return _nounTypePanel; }
        }
        //変換結果表示テキストボックス
        public static ResultTextBoxPanel _resultTextBoxPanel = new ResultTextBoxPanel();
        public static ResultTextBoxPanel ResultTextBoxPanel
        {
            get { return _resultTextBoxPanel; }
        }
        private static ArticleListView _articleList = new ArticleListView();
        public static ArticleListView ArticleList
        {
            get { return _articleList; }
        }
        private static PossessiveListView _possessiveList = new PossessiveListView();
        public static PossessiveListView PossessiveList
        {
            get { return _possessiveList; }
        }
    }
}
