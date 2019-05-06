using QuickStudyEnglish.Model.Enumeration;
using System;

namespace QuickStudyEnglish.Model.Structure
{
    public class StructurePrepositionItem : IDisposable, ICloneable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public StructurePrepositionItem()
        {
        }

        /// <summary>冠詞、所有格、名詞</summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>前置詞</summary>
        public PrePositions Preposition { get; set; } = PrePositions.None;
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
            StructurePrepositionItem temp = new StructurePrepositionItem();
            temp.Value = Value;
            temp.Preposition = Preposition;
            temp.Source = Source;
            return temp;
        }
    }
}

