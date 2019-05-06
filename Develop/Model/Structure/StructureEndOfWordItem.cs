using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.Model.Structure
{
    public class StructureEndOfWordItem : IDisposable, ICloneable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public StructureEndOfWordItem()
        {
        }

        /// <summary>管理番号</summary>
        public int ID { get; set; } = 0;
        public int MinorID { get; set; } = 0;
        public int GroupID { get; set; } = 0;
        /// <summary>1人称動詞</summary>
        public string FirstPerson { get; set; } = string.Empty;
        /// <summary>2人称動詞</summary>
        public string SecondPerson { get; set; } = string.Empty;
        /// <summary>3人称単数動詞</summary>
        public string ThirdSinglePerson { get; set; } = string.Empty;
        /// <summary>3人称複数動詞</summary>
        public string ThirdMultiPerson { get; set; } = string.Empty;

        public string FirstVerb { get; set; } = string.Empty;
        public string SecondVerb { get; set; } = string.Empty;
        public string ThirdVerb { get; set; } = string.Empty;



        public string Adverb { get; set; } = string.Empty;
        /// <summary>英単語</summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>否定文</summary>
        public string Not { get; set; } = string.Empty;
        /// <summary>英単語</summary>
        public string Value2 { get; set; } = string.Empty;
        /// <summary>英単語</summary>
        public string Value3 { get; set; } = string.Empty;
        /// <summary>英単語（その他）</summary>
        public string TimeWordValue { get; set; } = string.Empty;

        /// <summary>人称の種別</summary>
        public PersonalPronounCategory PersonalCategory { get; set; } = PersonalPronounCategory.None;
        /// <summary>形容詞の種別</summary>
        public AdjectiveCategory Category { get; set; } = AdjectiveCategory.None;
        /// <summary>主語の種別</summary>
        public SubjectCategory SubCategory { get; set; } = SubjectCategory.None;
        /// <summary>難易度</summary>
        public StepLevel Level { get; set; } = StepLevel.ALLSTEP;
        /// <summary>動詞</summary>
        public string Verb { get; set; } = string.Empty;




        /// <summary>日本語</summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>日本語</summary>
        public string Source2 { get; set; } = string.Empty;
        /// <summary>日本語</summary>
        public string Source3 { get; set; } = string.Empty;
        public string TimeWordSource { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        /// <summary>インスタンスの破棄</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);    //  ファイナライザによるDispose()呼び出しの抑制。
        }
        /// <summary>インスタンスの破棄</summary>
        /// <param name="disposing">呼び出し元の判別
        ///     <list type="bullet" >
        ///         <item>true=Dispose()関数からの呼び出し。</item>
        ///         <item>false=ファイナライザによる呼び出し。</item>
        ///     </list>
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (false == _disposed)
                {
                    if (true == disposing)
                    {
                        //  マネージリソースの解放
                    }
                    //  アンマネージリソースの解放
                }
                _disposed = true;
            }
            finally
            {
                ;   //  基底クラスのDispose()を確実に呼び出す。
                    //base.Dispose( disposing );
            }
        }
        /// <summary>クローン</summary>
        /// <returns>クローンされたインスタンス</returns>
        public object Clone()
        {
            StructureEndOfWordItem temp = new StructureEndOfWordItem();
            temp.ID = ID;
            temp.MinorID = MinorID;
            temp.GroupID = GroupID;
            temp.FirstPerson = FirstPerson;
            temp.SecondPerson = SecondPerson;
            temp.ThirdSinglePerson = ThirdSinglePerson;
            temp.ThirdMultiPerson = ThirdMultiPerson;
            temp.Adverb = Adverb;
            temp.Verb = Verb;
            temp.Not = Not;
            temp.Value = Value;
            temp.Value2 = Value2;
            temp.Value3 = Value3;
            temp.TimeWordValue = TimeWordValue;
            temp.Category = Category;
            temp.SubCategory = SubCategory;
            temp.PersonalCategory = PersonalCategory;
            temp.Level = Level;
            temp.Source = Source;
            temp.Source2 = Source2;
            temp.Source3 = Source3;
            temp.TimeWordSource = TimeWordSource;
            temp.Comment = Comment;
            temp.Verb = Verb;
            temp.FirstVerb = FirstVerb;
            temp.SecondVerb = SecondVerb;
            temp.ThirdVerb = ThirdVerb;
            return temp;
        }
    }
}

