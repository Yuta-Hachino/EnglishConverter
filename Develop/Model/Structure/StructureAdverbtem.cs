using QuickStudyEnglish.Model.Enumeration;
using System;

namespace QuickStudyEnglish.Model.Structure
{
    public class StructureAdverbItem : IDisposable, ICloneable
    {
        /// <summary>Dispose()関数が呼び出し済みであるかどうかのフラグ</summary>
        private bool _disposed = false;
        /// <summary>コンストラクタ</summary>
        public StructureAdverbItem()
        {
        }
        /// <summary>管理番号</summary>
        public int ID { get; set; } = 0;
        /// <summary>副詞</summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>前置詞</summary>
        public bool mode { get; set; } = false;

        /// <summary>位置</summary>
        public AdverbPosition Position { get; set; } = AdverbPosition.None;
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
            StructureAdverbItem temp = new StructureAdverbItem();
            temp.ID = ID;
            temp.Value = Value;
            temp.mode = mode;
            temp.Position = Position;
            temp.Source = Source;
            return temp;
        }
    }
}

