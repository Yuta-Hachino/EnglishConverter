using QuickStudyEnglish.Model.Enumeration;
using System;

namespace QuickStudyEnglish.Model.Structure
{
    public class StructureVerbItem : IDisposable, ICloneable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public StructureVerbItem()
        {
        }
        /// <summary>管理番号</summary>
        public int ID { get; set; } = 0;
        /// <summary>グループ番号</summary>
        public int GroupID { get; set; } = 0;
        /// <summary>動詞/形容詞</summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>前置詞</summary>
        public PrePositions PreValue { get; set; } = PrePositions.None;
        /// <summary>前置詞日本語</summary>
        public string PreSource { get; set; } = string.Empty;
        /// <summary>語尾種別</summary>
        public AdjectiveCategory Category { get; set; } = AdjectiveCategory.None;
        /// <summary>主語種別</summary>
        public SubjectCategory SubCategory { get; set; } = SubjectCategory.None;
        /// <summary>主語の固定（It）</summary>
        public bool IsConstIt { get; set; } = false;
        /// <summary>日本語</summary>
        public string Source { get; set; } = string.Empty;


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
            StructureVerbItem temp = new StructureVerbItem();
            temp.ID = ID;
            temp.GroupID = GroupID;
            temp.Value = Value;
            temp.PreValue = PreValue;
            temp.PreSource = PreSource;
            temp.Category = Category;
            temp.SubCategory = SubCategory;
            temp.Source = Source;
            temp.IsConstIt = IsConstIt;
            return temp;
        }
    }
}

