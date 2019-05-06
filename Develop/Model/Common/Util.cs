using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Common
{
    public class Util
    {
        /// <summary>
        /// 区切り文字リストを整形
        /// </summary>
        /// <param name="_delStringList">区切り文字入り文字列リスト</param>
        /// <param name="delimiter">区切り文字</param>
        /// <returns>整形結果のリスト</returns>
        public static List<List<string>> ReadDelimiterLine(List<string> _delStringList, char delimiter) {
            List<List<string>> shapingList = new List<List<string>>();
            foreach (string line in _delStringList) {
                List<string> list = line.Split(new char[] { delimiter }).ToList();
                if(list.Count > 1) {
                    shapingList.Add(list);
                }
            }
            return shapingList;
        }

        /// <summary>
        /// 2つの指定した文字列の間にある文字を削除します。2つの指定文字列を含むんで削除するかを指定できます。
        /// </summary>
        /// <param name="firstword">最初の文字列</param>
        /// <param name="endword">後ろの文字列</param>
        /// <param name="sourceword">削除対象が含まれた文字列</param>
        /// <param name="removePairwords">2つの指定文字列を含むんで削除する</param>
        /// <returns></returns>
        public static string RemoveBetweenPairStrings(string firstword, string endword, string sourceword, bool removePairwords = true) {
            string returnWord = sourceword;
            int firstIndex = -1;
            int endIndex = -1;
            while((returnWord.Contains(firstword) && returnWord.Contains(endword))) {
                firstIndex = returnWord.IndexOf(firstword);
                endIndex = returnWord.IndexOf(endword);
                returnWord = returnWord.Remove((removePairwords)? firstIndex : firstIndex + 1, endIndex - firstIndex + ((removePairwords) ? 1 : 2));
                firstIndex = -1;
                endIndex = -1;
            }
            return returnWord;
        }


        /// <summary>
        /// 文字列をEnumに変換する
        /// </summary>
        /// <typeparam name="T">enumタイプ</typeparam>
        /// <param name="enumStr">enum文字列</param>
        /// <returns>enum</returns>
        public static T ParseEnum<T>(string enumStr) where T : struct
        {
            return (T)Enum.Parse(typeof(T), enumStr, true);
        }

        public static bool IsHiragana(string word)
        {
            return HiraganaToAlphabet(word).Contains("*") ? false : true;
        }
        /// <summary>
        /// ひらがな→ローマ字変換
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static string HiraganaToAlphabet(string s1)
        {
            string s2 = "";
            for (int i = 0; i < s1.Length; i++)
            {
                // 小さい文字が含まれる場合
                if (i + 1 < s1.Length)
                {
                    // 「っ」が含まれる場合
                    if (s1.Substring(i, 1).CompareTo("っ") == 0)
                    {
                        s2 += HiraganaToAlphabetParse(s1.Substring(i + 1, 1)).Substring(0, 1);
                        continue;
                    }


                    // それ以外の小さい文字
                    string s3 = HiraganaToAlphabetParse(s1.Substring(i, 2));
                    if (s3.CompareTo("*") != 0)
                    {
                        s2 += s3;
                        i++;
                        continue;
                    }
                }
                s2 += HiraganaToAlphabetParse(s1.Substring(i, 1));
            }
            return s2;
        }


        private static string HiraganaToAlphabetParse(string s1)
        {
            switch (s1)
            {
                case "あ": return "a";
                case "い": return "i";
                case "う": return "u";
                case "え": return "e";
                case "お": return "o";
                case "か": return "ka";
                case "き": return "ki";
                case "く": return "ku";
                case "け": return "ke";
                case "こ": return "ko";
                case "さ": return "sa";
                case "し": return "shi";
                case "す": return "su";
                case "せ": return "se";
                case "そ": return "so";
                case "た": return "ta";
                case "ち": return "chi";
                case "つ": return "tsu";
                case "て": return "te";
                case "と": return "to";
                case "な": return "na";
                case "に": return "ni";
                case "ぬ": return "nu";
                case "ね": return "ne";
                case "の": return "no";
                case "は": return "ha";
                case "ひ": return "hi";
                case "ふ": return "hu";
                case "へ": return "he";
                case "ほ": return "ho";
                case "ま": return "ma";
                case "み": return "mi";
                case "む": return "mu";
                case "め": return "me";
                case "も": return "mo";
                case "や": return "ya";
                case "ゆ": return "yu";
                case "よ": return "yo";
                case "ら": return "ra";
                case "り": return "ri";
                case "る": return "ru";
                case "れ": return "re";
                case "ろ": return "ro";
                case "わ": return "wa";
                case "を": return "wo";
                case "ん": return "n";
                case "が": return "ga";
                case "ぎ": return "gi";
                case "ぐ": return "gu";
                case "げ": return "ge";
                case "ご": return "go";
                case "ざ": return "za";
                case "じ": return "ji";
                case "ず": return "zu";
                case "ぜ": return "ze";
                case "ぞ": return "zo";
                case "だ": return "da";
                case "ぢ": return "ji";
                case "づ": return "du";
                case "で": return "de";
                case "ど": return "do";
                case "ば": return "ba";
                case "び": return "bi";
                case "ぶ": return "bu";
                case "べ": return "be";
                case "ぼ": return "bo";
                case "ぱ": return "pa";
                case "ぴ": return "pi";
                case "ぷ": return "pu";
                case "ぺ": return "pe";
                case "ぽ": return "po";
                case "きゃ": return "kya";
                case "きぃ": return "kyi";
                case "きゅ": return "kyu";
                case "きぇ": return "kye";
                case "きょ": return "kyo";
                case "しゃ": return "sha";
                case "しぃ": return "syi";
                case "しゅ": return "shu";
                case "しぇ": return "she";
                case "しょ": return "sho";
                case "ちゃ": return "cha";
                case "ちぃ": return "cyi";
                case "ちゅ": return "chu";
                case "ちぇ": return "che";
                case "ちょ": return "cho";
                case "にゃ": return "nya";
                case "にぃ": return "nyi";
                case "にゅ": return "nyu";
                case "にぇ": return "nye";
                case "にょ": return "nyo";
                case "ひゃ": return "hya";
                case "ひぃ": return "hyi";
                case "ひゅ": return "hyu";
                case "ひぇ": return "hye";
                case "ひょ": return "hyo";
                case "みゃ": return "mya";
                case "みぃ": return "myi";
                case "みゅ": return "myu";
                case "みぇ": return "mye";
                case "みょ": return "myo";
                case "りゃ": return "rya";
                case "りぃ": return "ryi";
                case "りゅ": return "ryu";
                case "りぇ": return "rye";
                case "りょ": return "ryo";
                case "ぎゃ": return "gya";
                case "ぎぃ": return "gyi";
                case "ぎゅ": return "gyu";
                case "ぎぇ": return "gye";
                case "ぎょ": return "gyo";
                case "じゃ": return "ja";
                case "じぃ": return "ji";
                case "じゅ": return "ju";
                case "じぇ": return "je";
                case "じょ": return "jo";
                case "ぢゃ": return "dya";
                case "ぢぃ": return "dyi";
                case "ぢゅ": return "dyu";
                case "ぢぇ": return "dye";
                case "ぢょ": return "dyo";
                case "びゃ": return "bya";
                case "びぃ": return "byi";
                case "びゅ": return "byu";
                case "びぇ": return "bye";
                case "びょ": return "byo";
                case "ぴゃ": return "pya";
                case "ぴぃ": return "pyi";
                case "ぴゅ": return "pyu";
                case "ぴぇ": return "pye";
                case "ぴょ": return "pyo";
                case "ぐぁ": return "gwa";
                case "ぐぃ": return "gwi";
                case "ぐぅ": return "gwu";
                case "ぐぇ": return "gwe";
                case "ぐぉ": return "gwo";
                case "つぁ": return "tsa";
                case "つぃ": return "tsi";
                case "つぇ": return "tse";
                case "つぉ": return "tso";
                case "ふぁ": return "fa";
                case "ふぃ": return "fi";
                case "ふぇ": return "fe";
                case "ふぉ": return "fo";
                case "うぁ": return "wha";
                case "うぃ": return "whi";
                case "うぅ": return "whu";
                case "うぇ": return "whe";
                case "うぉ": return "who";
                case "ヴぁ": return "va";
                case "ヴぃ": return "vi";
                case "ヴ": return "vu";
                case "ヴぇ": return "ve";
                case "ヴぉ": return "vo";
                case "でゃ": return "dha";
                case "でぃ": return "dhi";
                case "でゅ": return "dhu";
                case "でぇ": return "dhe";
                case "でょ": return "dho";
                case "てゃ": return "tha";
                case "てぃ": return "thi";
                case "てゅ": return "thu";
                case "てぇ": return "the";
                case "てょ": return "tho";
                default: return "*";
            }
        }
        public static bool IsKatakana(string word) {
            return HiraganaToAlphabet(word).Contains("*") ? false : true;
        }
        /// <summary>
        /// カタカナ→ローマ字変換
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public static string KatakanaToAlphabet(string s1) {
            string s2 = "";
            for (int i = 0; i < s1.Length; i++) {
                // 小さい文字が含まれる場合
                if (i + 1 < s1.Length) {
                    // 「っ」が含まれる場合
                    if (s1.Substring(i, 1).CompareTo("ッ") == 0) {
                        s2 += KatakanaToAlphabetParse(s1.Substring(i + 1, 1)).Substring(0, 1);
                        continue;
                    }


                    // それ以外の小さい文字
                    string s3 = KatakanaToAlphabetParse(s1.Substring(i, 2));
                    if (s3.CompareTo("*") != 0) {
                        s2 += s3;
                        i++;
                        continue;
                    }
                }
                s2 += KatakanaToAlphabetParse(s1.Substring(i, 1));
            }
            return s2;
        }


        private static string KatakanaToAlphabetParse(string s1) {
            switch (s1) {
                case "ア": return "a";
                case "イ": return "i";
                case "ウ": return "u";
                case "エ": return "e";
                case "オ": return "o";
                case "カ": return "ka";
                case "キ": return "ki";
                case "ク": return "ku";
                case "ケ": return "ke";
                case "コ": return "ko";
                case "サ": return "sa";
                case "シ": return "shi";
                case "ス": return "su";
                case "セ": return "se";
                case "ソ": return "so";
                case "タ": return "ta";
                case "チ": return "chi";
                case "ツ": return "tsu";
                case "テ": return "te";
                case "ト": return "to";
                case "ナ": return "na";
                case "ニ": return "ni";
                case "ヌ": return "nu";
                case "ネ": return "ne";
                case "ノ": return "no";
                case "ハ": return "ha";
                case "ヒ": return "hi";
                case "フ": return "hu";
                case "ヘ": return "he";
                case "ホ": return "ho";
                case "マ": return "ma";
                case "ミ": return "mi";
                case "ム": return "mu";
                case "メ": return "me";
                case "モ": return "mo";
                case "ヤ": return "ya";
                case "ユ": return "yu";
                case "ヨ": return "yo";
                case "ラ": return "ra";
                case "リ": return "ri";
                case "ル": return "ru";
                case "レ": return "re";
                case "ロ": return "ro";
                case "ワ": return "wa";
                case "ヲ": return "wo";
                case "ン": return "n";
                case "ガ": return "ga";
                case "ギ": return "gi";
                case "グ": return "gu";
                case "ゲ": return "ge";
                case "ゴ": return "go";
                case "ザ": return "za";
                case "ジ": return "ji";
                case "ズ": return "zu";
                case "ゼ": return "ze";
                case "ゾ": return "zo";
                case "ダ": return "da";
                case "ヂ": return "ji";
                case "ヅ": return "du";
                case "デ": return "de";
                case "ド": return "do";
                case "バ": return "ba";
                case "ビ": return "bi";
                case "ブ": return "bu";
                case "ベ": return "be";
                case "ボ": return "bo";
                case "パ": return "pa";
                case "ピ": return "pi";
                case "プ": return "pu";
                case "ペ": return "pe";
                case "ポ": return "po";
                case "ァ": return "a";
                case "ィ": return "i";
                case "ゥ": return "u";
                case "ェ": return "e";
                case "ォ": return "o";
                case "ャ": return "lya";
                case "ュ": return "lyu";
                case "ョ": return "lyo";
                case "ヴ": return "vu";
                default: return "*";
            }
        }
    }
}
