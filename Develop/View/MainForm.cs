using QuickStudyEnglish.Model.Enumeration;
using System;
using System.Drawing;
using System.Windows.Forms;
using QuickStudyEnglish.View.Containers;
using QuickStudyEnglish.View.FormError;
using SpeechLib;
using System.IO;

namespace QuickStudyEnglish
{
    public partial class MainForm : Form
    {
        ToolTip tooltip = new ToolTip();
        QuickStudyEnglish.OptionForm optionForm = null;
        SerchForm serchForm = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) {
            ContainerBase.parent = this;

            if(_ControlsInit() != ResultCodes.Success) {
                QFormError.OpenErrorMessage("初期化異常です。アプリケーションを終了します。", QFormError.ERROR_CLEAN_UP_TYPE.SHATDOWN);
            }

            // フォームの色をコンフィグの色に変更
            string path = Application.StartupPath + @"\option.conf";
            if (!File.Exists(path)) {
                using (StreamWriter sw = File.CreateText(path)) {
                    // 初期背景色を設定
                    sw.WriteLine("color=15790320");
                }
                this.BackColor = ColorTranslator.FromWin32(15790320);
            } else {
                using (StreamReader sr = File.OpenText(path)) {
                    this.BackColor = ColorTranslator.FromWin32(int.Parse(sr.ReadLine().Replace("color=", "")));
                }
            }

            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;
            checkedListBox1.SetItemChecked(lastCheckedIndex, true);
        }

        int lastCheckedIndex = 0;
        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(e.Index != lastCheckedIndex) {
                if(lastCheckedIndex != -1) {
                    checkedListBox1.SetItemCheckState(lastCheckedIndex, CheckState.Unchecked);
                }
                lastCheckedIndex = e.Index;
                ContainerFactory.SubjectList._SubjectListView_Click(checkedListBox1.Text);
            }
        }

        ///// <summary>
        ///// View初期化
        ///// </summary>
        ///// <returns></returns>
        public ResultCodes _ControlsInit() {
            ResultCodes ret = ResultCodes.Success;

            //難易度初期化
            ContainerFactory.StepLevelComboBox.SetStepLevel();

            //読み仮名初期化
            ContainerFactory.JapaneseReadTypeComboBox.SetJapaneseReadType();

            // 主語を初期セット
            ContainerFactory.SubjectList.SetSubjectList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, true);

            //副詞を初期セット
            ContainerFactory.AdverbList.SetAdverbList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType, AdverbPosition.None, true);

            // 形動存のパネルを初期セット
            ContainerFactory.ConvertModePanel.SetInitPanel();

            // 語尾初期化
            ContainerFactory.EndOfWordListView.SetEndOfWordList(true);

            // 名詞（一般、人、場所）のパネルを初期セット
            ContainerFactory.NounTypePanel.SetInitPanel();
            
            //前置詞初期化
            ContainerFactory.PrePositionList.SetPrePositionList(ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType,true);

            //コメント表示

            return ret;
        }
        private void _RateTrackBar_ValueChanged(object sender, EventArgs e) {
            sv.Rate = _RateTrackBar.Value;
        }

        private void _VolumeTrackBar_ValueChanged(object sender, EventArgs e) {
            sv.Volume = _VolumeTrackBar.Value;
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) {
            Control[] control = Controls.Find("_ResultLabel", true);
            if (control[0].Focused) {
                _ResultLabel.Copy();
            } else {
                _SourceViewLabel.Copy();
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e) {
            Control[] control = Controls.Find("_ResultLabel", true);
            if (control[0].Focused) {
                _ResultLabel.Paste();
            } else {
                _SourceViewLabel.Paste();
            }
        }

        private void CuttingToolStripMenuItem_Click(object sender, EventArgs e) {
            Control[] control = Controls.Find("_ResultLabel", true);
            if (control[0].Focused) {
                _ResultLabel.Copy();
                string clip = Clipboard.GetText().Replace(Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf);
                _ResultLabel.Text = _ResultLabel.Text.Replace(clip, "");
            } else {
                _SourceViewLabel.Copy();
                string clip = Clipboard.GetText().Replace(Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf);
                _SourceViewLabel.Text = _SourceViewLabel.Text.Replace(clip, "");
            }
        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e) {
            if(optionForm == null) {
                optionForm = new OptionForm();
            }
            if (!optionForm.Visible) {
                optionForm.Show(this);
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("アプリケーションを終了しますか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
                Application.ExitThread();
            }
        }
        private void SearchToolStripMenuItem_Click(object sender, EventArgs e) {
            if (serchForm != null) {
                serchForm.Close();
                serchForm = null;
            }
            serchForm = new SerchForm();
            serchForm.Show(this);
        }



        #region<<ControlMethod>>
        private void textBox1_TextChanged(object sender, EventArgs e) {
        }
        private void _EndOfWordSerchTextBox_TextChanged(object sender, EventArgs e) {
        }
        private void _PrePositionsListView_Click(object sender, EventArgs e) {
        }
        private void _LocationPrepositionListView_Click(object sender, EventArgs e) {
        }
        private void _PersonPrepositionListView_Click(object sender, EventArgs e) {
        }
        private void _StepLevelComboBox_SelectedIndexChanged(object sender, EventArgs e) {
        }
        private void _AddCommaBtn_CheckedChanged(object sender, EventArgs e) {
        }
        private void _KanaChangeBtn_CheckedChanged(object sender, EventArgs e) {
        }
        private void _AdverbListView_Click(object sender, EventArgs e) {
        }
        #endregion


        private SpVoice sv = new SpVoice();

        private void button2_Click(object sender, EventArgs e) {
            // 読み上げ中かどうかを判断
            if (sv.Status.RunningState == SpeechRunState.SRSEIsSpeaking) {
                // 読み上げ中なら停止
                sv.Speak(" ", SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            } else {
                // 読み上げ対象文字列
                string str = ResultLabel.Text;

                // 空文字ならば読み上げない
                if (string.IsNullOrEmpty(str)) return;

                bool hit = false;
                foreach (SpObjectToken vp in sv.GetVoices()) {
                    string lang = vp.GetAttribute("Language");

                    if (lang == "809" || lang == "409") {
                        sv.Voice = vp;
                        hit = true;
                    }
                }

                if (hit) {
                    sv.Rate = _RateTrackBar.Value;
                    sv.Volume = _VolumeTrackBar.Value;
                    sv.Speak(ResultLabel.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                } else {
                    MessageBox.Show("英語合成音声が利用できません。\r\nMicrosoft Speech PlatformよりMSSpeech_SR_en-US_TELEを　インストールしてください。\r\n");
                }
            }
        }

        private void _PauseBtn_Click(object sender, EventArgs e) {
            // 読み上げ中かどうかを判断
            if (sv.Status.RunningState == SpeechRunState.SRSEIsSpeaking) {
                // 読み上げ中なら停止
                sv.Pause();
            } else {
                sv.Resume();
            }
        }
        /* 音声認識実験用ソース */
        private SpInProcRecoContext recRule = null;
        private ISpeechRecoGrammar recGrammerRule = null;
        private ISpeechGrammarRule recGRGrammerRule = null;
        
        private void button3_Click(object sender, EventArgs e) {

            if(this.recRule != null) {
                return;
            }
            //this.button3.Enabled = false;
            this.recRule = new SpInProcRecoContext();

            bool hit = false;
            foreach(SpObjectToken recPerson in this.recRule.Recognizer.GetRecognizers()) {
                string lang = recPerson.GetAttribute("Language");
                if(lang == "411") {
                    this.recRule.Recognizer.Recognizer = recPerson;
                    hit = true;
                    break;
                }
            }

            if (!hit) {
                MessageBox.Show("日本語認識が利用できません。");
            } else {
                Console.WriteLine("マイク取得開始\n");

                Console.WriteLine(this.recRule.Recognizer.Status.ClsidEngine);
                this.recRule.Recognizer.AudioInput = this.CreateMicrofon();

                Console.WriteLine("マイク取得完了\n");
                Console.WriteLine("デリゲート登録\n");
                //認識の途中
                this.recRule.Hypothesis +=
                    delegate (int streamNumber, object streamPosition, SpeechLib.ISpeechRecoResult result) {
                        string strText = result.PhraseInfo.GetText(0, -1, true);
                        this._SourceViewLabel.Text = strText;
                    };
                //認識完了
                this.recRule.Recognition +=
                    delegate (int streamNumber, object streamPosition, SpeechLib.SpeechRecognitionType srt, SpeechLib.ISpeechRecoResult isrr) {
                        string strText = isrr.PhraseInfo.GetText(0, -1, true);
                        this._ResultLabel.Text = strText;
                    };
                //ストリームに何かデータが来た(?)
                this.recRule.StartStream +=
                    delegate (int streamNumber, object streamPosition) {
                        this._SourceViewLabel.Text = "認識？";
                        this._ResultLabel.Text = "認識？";
                    };
                //認識失敗
                this.recRule.FalseRecognition +=
                    delegate (int streamNumber, object streamPosition, SpeechLib.ISpeechRecoResult isrr) {
                        this._ResultLabel.Text = "--ERROR!--";
                    };
                Console.WriteLine("デリゲート登録完了\n");
                Console.WriteLine("モデル作成\n");
                //言語モデルの作成
                this.recGrammerRule = this.recRule.CreateGrammar(0);
                Console.WriteLine("モデル作成完了\n");
                this.recGrammerRule.Reset(0);
                //言語モデルのルールのトップレベルを作成する.
                this.recGRGrammerRule = this.recGrammerRule.Rules.Add("TopLevelRule",
                    SpeechRuleAttributes.SRATopLevel | SpeechRuleAttributes.SRADynamic);

                // 読み込む対象文字列をここですべて読み込んでおく必要がある
                this.recGRGrammerRule.InitialState.AddWordTransition(null, "私は");

                //ルールを反映させる。
                this.recGrammerRule.Rules.Commit();

                //音声認識開始。(トップレベルのオブジェクトの名前で SpeechRuleState.SGDSActive を指定する.)
                this.recGrammerRule.CmdSetRuleState("TopLevelRule", SpeechRuleState.SGDSActive);

                Console.WriteLine("音声認識開始");
            }
            
        }
        
        private SpObjectToken CreateMicrofon() {
            SpObjectTokenCategory objAudioTokenCategory = new SpObjectTokenCategory();
            objAudioTokenCategory.SetId(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\AudioInput");
            SpObjectToken objAudioToken = new SpObjectToken();
            objAudioToken.SetId(objAudioTokenCategory.Default, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\AudioInput");
            return objAudioToken;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
