using QuickStudyEnglish.Model.File;
using QuickStudyEnglish.Model.Master.MasterData;

namespace QuickStudyEnglish.Model.Master
{
    internal class MasterFilesName
    {
        public const string ENGLISH_WORD_MASTER = "EnglishWordMaster";
        public const string WEATHER_MASTER = "WeatherMaster";
        public const string JAPANESE_WORD_MASTER = "JapaneseWordMaster";
        public const string JAPANESE_ENDING_WORD_MASTER = "JapaneseEndingWordMaster";
        public const string JAPANESE_COMBO_WORD_MASTER = "JapaneseComboWordMaster";
        public const string COMBO_WORD_MASTER = "ComboWordMaster";
        public const string SUBJCT_MASTER = "SubjectMaster";
        public const string ADJECTIVE_CONNECTION_MASTER = "AdjectiveConnectionMaster";
        public const string ADJECTIVE_END_OF_WORD = "AdjectiveEndOfWordMaster";
        public const string ADJECTIVE_NUMBER_OF_PEOPLE_END_OF_WORD = "AdjectiveNumberOfPeopleEndOfWordMaster";
        public const string ADJECTIVE_ADD_END_OF_WORD = "AdjectiveAddEndOfWordMaster";
        public const string ADJECTIVE_MASTER = "AdjectiveMaster";
        public const string VERB_CONNECTION_MASTER = "VerbConnectionMaster";
        public const string VERB_END_OF_WORD_MASTER = "VerbEndOfWordMaster";
        public const string VERB_ADD_END_OF_WORD = "VerbAddEndOfWordMaster";
        public const string VERB_MASTER = "VerbMaster";
        public const string BEING_CONNECTION_MASTER = "BeingConnectionMaster";
        public const string BEING_ADD_MASTER = "BeingAddMaster";
        public const string BEING_MASTER = "BeingMaster";
        public const string ADVERB_MASTER = "AdverbMaster";
        public const string PREPOSITION_MASTER = "PrepositionMaster";
        public const string NOUN_MASTER = "NounMaster";
        public const string NOUN_CONNECTION_MASTER = "NounConnectionMaster";
        public const string ARTICLE_MASTER = "ArticleMaster";
        public const string POSSESSIVE_MASTER = "PossessiveMaster";
    }

    public class MasterFactory
    {
        private static BaseMaster englishWordMaster = null;
        private static BaseMaster weatherMaster = null;
        private static BaseMaster japaneseWordMaster = null;
        private static BaseMaster japaneseEndingWordMaster = null;
        private static BaseMaster japaneseComboWordMaster = null;
        private static BaseMaster comboWordMaster = null;
        private static BaseMaster subjectMaster = null;
        private static BaseMaster adjectiveConnectionMaster = null;
        private static BaseMaster adjectiveEndOfWordMaster = null;
        private static BaseMaster adjectiveNumberOfPeopleEndOfWordMaster = null;
        private static BaseMaster adjectiveAddEndOfWordMaster = null;
        private static BaseMaster adjectiveMaster = null;
        private static BaseMaster verbConnectionMaster = null;
        private static BaseMaster verbEndOfWordMaster = null;
        private static BaseMaster verbAddEndOfWordMaster = null;
        private static BaseMaster verbMaster = null;
        private static BaseMaster beingConnectionMaster = null;
        private static BaseMaster beingAddMaster = null;
        private static BaseMaster beingMaster = null;
        private static BaseMaster adverbMaster = null;
        private static BaseMaster prepositionMaster = null;
        private static BaseMaster nounMaster = null;
        private static BaseMaster nounConnectionMaster = null;
        private static BaseMaster articleMaster = null;
        private static BaseMaster possessiveMaster = null;

        /// <summary>
        /// マスターデータを取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <returns>マスタデータ</returns>
        public static T GetMasterData<T>() where T : BaseMaster {
            BaseMaster md = null;

            // 英単語マスタ
            if(typeof(T) == typeof(EnglishWordMaster)) {
                if (englishWordMaster == null) {
                    englishWordMaster = new EnglishWordMaster();
                    englishWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ENGLISH_WORD_MASTER));
                }
                md = englishWordMaster;
            }

            // 天気マスタ
            if(typeof(T) == typeof(WeatherMaster)) {
                if (weatherMaster == null) {
                    weatherMaster = new WeatherMaster();
                    weatherMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.WEATHER_MASTER));
                }
                md = weatherMaster;
            }

            // 日本語マスタ
            if (typeof(T) == typeof(JapaneseWordMaster)) {
                if (japaneseWordMaster == null) {
                    japaneseWordMaster = new JapaneseWordMaster();
                    japaneseWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.JAPANESE_WORD_MASTER));
                }
                md = japaneseWordMaster;
            }

            // 日本語接尾語マスタ
            if(typeof(T) == typeof(JapaneseEndingWordMaster)) {
                if(japaneseEndingWordMaster == null) {
                    japaneseEndingWordMaster = new JapaneseEndingWordMaster();
                    japaneseEndingWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.JAPANESE_ENDING_WORD_MASTER));
                }
                md = japaneseEndingWordMaster;
            }

            // 日本語組み合わせマスタ
            if(typeof(T) == typeof(JapaneseComboMaster)) {
                if(japaneseComboWordMaster == null) {
                    japaneseComboWordMaster = new JapaneseComboMaster();
                    japaneseComboWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.JAPANESE_COMBO_WORD_MASTER));
                }
                md = japaneseComboWordMaster;
            }

            // 英日組み合わせマスタ
            if (typeof(T) == typeof(ComboWordMaster)) {
                if (comboWordMaster == null) {
                    comboWordMaster = new ComboWordMaster();
                    comboWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.COMBO_WORD_MASTER));
                }
                md = comboWordMaster;
            }

            // 主語マスタ
            if (typeof(T) == typeof(SubjectMaster)) {
                if (subjectMaster == null) {
                    subjectMaster = new SubjectMaster();
                    subjectMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.SUBJCT_MASTER));
                }
                md = subjectMaster;
            }

            // 形容詞語尾マスタ
            if(typeof(T) == typeof(AdjectiveEndOfWordMaster)) {
                if(adjectiveEndOfWordMaster == null) {
                    adjectiveEndOfWordMaster = new AdjectiveEndOfWordMaster();
                    adjectiveEndOfWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADJECTIVE_END_OF_WORD));
                }
                md = adjectiveEndOfWordMaster;
            }

            // 形容詞人数語尾マスタ
            if(typeof(T) == typeof(AdjectiveNumberOfPeopleEndOfWordMaster)) {
                if(adjectiveNumberOfPeopleEndOfWordMaster == null) {
                    adjectiveNumberOfPeopleEndOfWordMaster = new AdjectiveNumberOfPeopleEndOfWordMaster();
                    adjectiveNumberOfPeopleEndOfWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADJECTIVE_NUMBER_OF_PEOPLE_END_OF_WORD));
                }
                md = adjectiveNumberOfPeopleEndOfWordMaster;
            }

            // 形容詞接続管理マスタ
            if (typeof(T) == typeof(AdjectiveConnectionMaster)) {
                if (adjectiveConnectionMaster == null) {
                    adjectiveConnectionMaster = new AdjectiveConnectionMaster();
                    adjectiveConnectionMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADJECTIVE_CONNECTION_MASTER));
                }
                md = adjectiveConnectionMaster;
            }

            // 形容詞語尾後付けマスタ
            if (typeof(T) == typeof(AdjectiveAddEndOfWordMaster)) {
                if(adjectiveAddEndOfWordMaster == null) {
                    adjectiveAddEndOfWordMaster = new AdjectiveAddEndOfWordMaster();
                    adjectiveAddEndOfWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADJECTIVE_ADD_END_OF_WORD));
                }
                md = adjectiveAddEndOfWordMaster;
            }

            // 形容詞マスタ
            if (typeof(T) == typeof(AdjectiveMaster)) {
                if (adjectiveMaster == null) {
                    adjectiveMaster = new AdjectiveMaster();
                    adjectiveMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADJECTIVE_MASTER));
                }
                md = adjectiveMaster;
            }

            // 動詞語尾マスタ
            if (typeof(T) == typeof(VerbEndOfWordMaster)) {
                if (verbEndOfWordMaster == null) {
                    verbEndOfWordMaster = new VerbEndOfWordMaster();
                    verbEndOfWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.VERB_END_OF_WORD_MASTER));
                }
                md = verbEndOfWordMaster;
            }
            //動詞接続管理マスタ
            if (typeof(T) == typeof(VerbConnectionMaster)) {
                if (verbConnectionMaster == null) {
                    verbConnectionMaster = new VerbConnectionMaster();
                    verbConnectionMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.VERB_CONNECTION_MASTER));
                }
                md = verbConnectionMaster;
            }
            //動詞語尾後付けマスタ
            if (typeof(T) == typeof(VerbAddEndOfWordMaster)) {
                if (verbAddEndOfWordMaster == null) {
                    verbAddEndOfWordMaster = new VerbAddEndOfWordMaster();
                    verbAddEndOfWordMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.VERB_ADD_END_OF_WORD));
                }
                md = verbAddEndOfWordMaster;
            }

            // 動詞マスタ
            if (typeof(T) == typeof(VerbMaster)) {
                if (verbMaster == null) {
                    verbMaster = new VerbMaster();
                    verbMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.VERB_MASTER));
                }
                md = verbMaster;
            }

            // 存在接続管理マスタ
            if (typeof(T) == typeof(BeingConnectionMaster)) {
                if (beingConnectionMaster == null) {
                    beingConnectionMaster = new BeingConnectionMaster();
                    beingConnectionMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.BEING_CONNECTION_MASTER));
                }
                md = beingConnectionMaster;
            }

            // 存在後付けマスタ
            if (typeof(T) == typeof(BeingAddWordMaster)) {
                if (beingAddMaster == null) {
                    beingAddMaster = new BeingAddWordMaster();
                    beingAddMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.BEING_ADD_MASTER));
                }
                md = beingAddMaster;
            }

            // 存在マスタ
            if (typeof(T) == typeof(BeingWordMaster)) {
                if (beingMaster == null) {
                    beingMaster = new BeingWordMaster();
                    beingMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.BEING_MASTER));
                }
                md = beingMaster;
            }

            // 副詞マスタ
            if (typeof(T) == typeof(AdverbMaster)) {
                if(adverbMaster == null) {
                    adverbMaster = new AdverbMaster();
                    adverbMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ADVERB_MASTER));
                }
                md = adverbMaster;
            }

            // 前置詞マスタ
            if(typeof(T) == typeof(PrepositionMaster)) {
                if (prepositionMaster == null) {
                    prepositionMaster = new PrepositionMaster();
                    prepositionMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.PREPOSITION_MASTER));
                }
                md = prepositionMaster;
            }

            // 名詞マスタ
            if(typeof(T) == typeof(NounMaster)) {
                if (nounMaster == null) {
                    nounMaster = new NounMaster();
                    nounMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.NOUN_MASTER));
                }
                md = nounMaster;
            }

            // 名刺接続管理マスタ
            if (typeof(T) == typeof(NounConnectionMaster)) {
                if (nounConnectionMaster == null) {
                    nounConnectionMaster = new NounConnectionMaster();
                    nounConnectionMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.NOUN_CONNECTION_MASTER));
                }
                md = nounConnectionMaster;
            }

            // 冠詞マスタ
            if (typeof(T) == typeof(ArticleMaster)) {
                if (articleMaster == null) {
                    articleMaster = new ArticleMaster();
                    articleMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.ARTICLE_MASTER));
                }
                md = articleMaster;
            }
            
            // 所有格マスタ
            if (typeof(T) == typeof(PossessiveMaster)) {
                if (possessiveMaster == null) {
                    possessiveMaster = new PossessiveMaster();
                    possessiveMaster.SetData(ReadDelimiterFiles.ReadMasterData(MasterFilesName.POSSESSIVE_MASTER));
                }
                md = possessiveMaster;
            }

            return (T)md;
        }
    }
}
