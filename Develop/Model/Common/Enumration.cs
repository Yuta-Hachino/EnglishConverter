namespace QuickStudyEnglish.Model.Enumeration
{
    /// <summary>
    /// 数量カテゴリ（０＝単数、１＝複数）
    /// </summary>
    public enum QuantityCategory
    {
        single = 0,
        multi = 1
    }
    /// <summary>
    /// カテゴリ（0＝カテゴリ無、1＝人、2＝動物、3＝場所）
    /// </summary>
    public enum NounCategory
    {
        None = 0,
        People,
        Animal,
        Location
    }

    /// <summary>
    /// 日本語の読み出しタイプ
    /// </summary>
    public enum JapaneseReadType
    {
        KANJI,
        HIRAGANA,
        FURIGANA
    }

    /// <summary>
    /// 時間指定
    /// </summary>
    public enum TimeType
    {
        None = 0,
        Current,
        Past,
        Progressing,
        PastParticiple,
        Future
    }

    /// <summary>
    /// 副詞の位置
    /// </summary>
    public enum AdverbPosition
    {
        None = 0,
        Front,
        Middle,
        Back,
        Special
    }

    /// <summary>
    /// ステップレベル
    /// </summary>
    public enum StepLevel
    {
        ALLSTEP,
        STEPⅠ,
        STEPⅡ,
        STEPⅢ,
        STEPⅣ,
        STEPⅤ,
        STEPⅥ
    }
    
    public enum NounType
    {
        None,
        Noun,
        Person,
        Location
    }

    /// <summary>
    /// 変換モード
    /// </summary>
    public enum TargetMode
    {
        None,
        Adjective,
        Verb,
        Being
    }

    /// <summary>
    /// 人称固定理由
    /// </summary>
    public enum ConstReason
    {
        /// <summary>指定無</summary>
        None,
        /// <summary>天気</summary>
        WEATHER
    }
    /// <summary>
    /// 接続詞
    /// </summary>
    public enum Conjunction
    {
        /// <summary>指定無</summary>
        None,
        AFTER,
        ALSO,
        ALTHOUGH,
        AND,
        AS,
        BECAUSE,
        BEFORE,
        BUT,
        CONSIDERING,
        DIRECTLY,
        EXCEPT,
        FOR,
        HOWEVER,
        IF,
        IMMEDIATELY,
        LEST,
        LIKE,
        NOR,
        NOW,
        NOTWITHSTANDING,
        ONCE,
        ONLY,
        OR,
        PLUS,
        PROVIDING,
        SAVE,
        SINCE,
        SO,
        THAN,
        THAT,
        THEN,
        THOUGH,
        TILL,
        UNLESS,
        UNTIL,
        WHEN,
        WHENEVER,
        WHERE,
        WHEREAS,
        WHEREVER,
        WHETHER,
        WHILE,
        WITHOUT,
        YET
    }
    /// <summary>
    /// 前置詞
    /// </summary>
    public enum PrePositions
    {
        /// <summary>指定無</summary>
        None,
        ABOUT,
        ABOARD,
        ABOVE,
        ACROSS,
        AFTER,
        AGAINST,
        ALONG,
        ALONGSIDE,
        AMID,
        AMONG,
        ANTI,
        AROUND,
        AS,
        AT,
        BAR,
        BEFORE,
        BEHIND,
        BELOW,
        BENEATH,
        BESIDE,
        BESIDES,
        BETWEEN,
        BEYOND,
        BUT,
        BY,
        CONSIDERING,
        DESPITE,
        DOWN,
        DURING,
        EXCEPT,
        FOR,
        FROM,
        IN,
        INSIDE,
        INTO,
        LESS,
        LIKE,
        MINUS,
        NEAR,
        NOTWITHSTANDING,
        OF,
        OFF,
        ON,
        ONTO,
        OPPOSITE,
        OUT,
        OUTSIDE,
        OVER,
        PACE,
        PAST,
        PENDING,
        PER,
        PLUS,
        RE,
        REGARDING,
        ROUND,
        SAVE,
        SAVING,
        SINCE,
        THAN,
        THROUGH,
        THROUGHOUT,
        TILL,
        TO,
        TOUCHING,
        TOWARD,
        UNDER,
        UNDERNEATH,
        UNLESS,
        UNLIKE,
        UNTIL,
        UP,
        VERSUS,
        VIA,
        VICE,
        WITH,
        WITHIN,
        WITHOUT
    }
    public enum PossessiveCategory
    {
        MY,
        HER
    }
    /// <summary>
    /// 形容詞種別
    /// </summary>
    public enum AdjectiveCategory
    {
        /// <summary>指定無</summary>
        None,
        /// <summary>Feel</summary>
        Feel,
        /// <summary>Be</summary>
        Be,
        /// <summary>Get</summary>
        Get,
        /// <summary>Go</summary>
        Go
    }
    /// <summary>
    /// 人モノ種別
    /// </summary>
    public enum SubjectCategory
    {
        /// <summary>指定無</summary>
        None,
        /// <summary>生物</summary>
        Creature,
        /// <summary>無生物</summary>
        InAnimateObject
    }

    /// <summary>
    /// 人称種別
    /// </summary>
    public enum PersonalPronounCategory
    {
        /// <summary>指定無</summary>
        None,
        /// <summary>1人称</summary>
        First,
        /// <summary>2人称</summary>
        Second,
        /// <summary>3人称単数</summary>
        ThirdSingle,
        /// <summary>3人称複数</summary>
        ThirdMulti
    }

    /// <summary>共通エラーコード</summary>
    public enum ResultCodes
    {
        /// <summary>成功</summary>
        Success,
        /// <summary>引数異常</summary>
        InvalidArgument,
        /// <summary>該当なし</summary>
        NotFound,
        /// <summary>サポートされない機能の呼び出し</summary>
        NotSupported,
        /// <summary>不要につき実行しなかった</summary>
        NotExecute,
        /// <summary>入力範囲外</summary>
        OutOfRange,
        /// <summary>登録済み</summary>
        AlreadyRegistered,
        /// <summary>ファイル読み込み失敗</summary>
        FailToReadFile,
        /// <summary>ファイル書き込み失敗</summary>
        FailToWriteFile,
        /// <summary>暗号化失敗</summary>
        FailToEncryption,
        /// <summary>復号化失敗</summary>
        FailToDecryption,
        /// <summary>事前条件の不備</summary>
        /// <remarks>
        /// プロパティの設定等、事前に行われているべき手順が実施されていないことをあらわします。
        /// </remarks>
        LackOfPreparation,
        /// <summary>OSによりキャッチされた例外</summary>
        /// <remarks>
        /// 詳細はログファイルを参照してください。
        /// 以下の例外をキャッチした可能性があります。
        /// <list type="bullet" >
        ///		<item>ApplicationException</item>
        ///		<item>ArgumentException</item>
        ///		<item>ArgumentNullException</item>
        ///		<item>ArgumentOutOfRangeException</item>
        ///		<item>DirectoryNotFoundException</item>
        ///		<item>DllNotFoundException</item>
        ///		<item>EntryPointNotFoundException</item>
        ///		<item>Exception</item>
        ///		<item>FileNotFoundException</item>
        ///		<item>FormatException</item>
        ///		<item>InvalidOperationException</item>
        ///		<item>IO.IOException</item>
        ///		<item>IOException</item>
        ///		<item>NotSupportedException</item>
        ///		<item>NullReferenceException</item>
        ///		<item>ObjectDisposedException</item>
        ///		<item>PathTooLongException</item>
        ///		<item>Security.SecurityException</item>
        ///		<item>Threading.AbandonedMutexException</item>
        ///		<item>UnauthorizedAccessException</item>
        ///		<item>Xml.XmlException</item>
        ///		<item>Xml.XPath.XPathException</item>
        ///		<item>XmlException</item>
        ///		<item>XPathException</item>
        /// </list>
        /// </remarks>
        ExceptionFromWindows,

        #region UI
        //未選択状態
        NotSelect,
        //処理済み
        AlreadySetting,
        //書き込み制限
        WriteProtected,
        #endregion
    }
}
