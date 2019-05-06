using QuickStudyEnglish.Model.Enumeration;
using QuickStudyEnglish.Model.Master;
using QuickStudyEnglish.Model.Master.MasterData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickStudyEnglish.View.Containers
{
    public class ResultTextBoxPanel : ContainerBase
    {
        string[] adjectiveVerb = new string[] {
            "be", "been", "being",
            "feel", "feels", "felt", "feeling",
            "get", "gets", "got", "getting", "gotten",
            "go", "goes", "went", "gone", "going"};

        /// <summary>
        /// 変換結果出力
        /// </summary>
        public void UpdateResultTextBoxPanel() {
            switch (ContainerFactory.ConvertModePanel.NowPanelMode) {
                case TargetMode.Adjective:
                    SetAdjectiveResultTextBox();
                    SetAdjectiveSourceTextBox();
                    break;

                case TargetMode.Verb:
                    SetVerbResultTextBox();
                    SetVerbSourceTextBox();
                    break;

                case TargetMode.Being:
                    SetBeingResultTextBox();
                    SetBeingSourceTextBox();
                    break;
            }
        }

        /// <summary>
        /// 英語の出力
        /// </summary>
        private void SetAdjectiveResultTextBox() {
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            AdjectiveEndOfWord adjectiveEndItem = (
                ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? (MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeItemGroupByGroupIdAndStepLevel(
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.GroupId, 
                                                                                        ContainerFactory.StepLevelComboBox._StepLevel,
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.Category)).ToList()
                                                                                   : (MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeItemGroupByGroupId(
                                                                                       ContainerFactory.AdjectiveList.SelectedItem.GroupId,
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.Category)).ToList()
                                                                                   )[(ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 : 0];
            AdjectiveNumberOfPeopleEndOfWord adjectiveNumEndItem = (
                ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishItemGroupByGroupIdAndPersonCategoryAndStepLevel(
                                                                                        ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId,
                                                                                        ContainerFactory.SubjectList._PersonalCategory,
                                                                                        ContainerFactory.StepLevelComboBox._StepLevel,
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.Category)
                                                                                   : MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishItemGroupByGroupIdAndPersonCategory(
                                                                                       ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId,
                                                                                       ContainerFactory.SubjectList._PersonalCategory,
                                                                                    ContainerFactory.AdjectiveList.SelectedItem.Category))[(ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 : 0];
        //語尾と時間詞
        string end = string.Empty;
            string timeValue = string.Empty;
            switch (ContainerFactory.SubjectList._PersonalCategory) {
                case PersonalPronounCategory.ThirdSingle:
                    end = MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetData(adjectiveEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    timeValue = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(adjectiveEndItem.Id, 0)[0];
                    break;

                default:
                    end = MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetEnglishWord(adjectiveNumEndItem.EnglishId);
                    timeValue = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(0, adjectiveNumEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    break;
                    
            }
            string be = MasterFactory.GetMasterData<AdjectiveMaster>().GetData(ContainerFactory.AdjectiveList.SelectedItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //副詞
            string adverbFront = string.Empty;
            string adverbMiddle = string.Empty;
            string adverbBack = string.Empty;

            switch (MasterFactory.GetMasterData<AdverbMaster>().GetAdverbPosFromId(ContainerFactory.AdverbList.SelectedItemIndex)) {
                case AdverbPosition.Front:
                    adverbFront = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0] + " ";
                    break;

                case AdverbPosition.Middle:
                    adverbMiddle = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    break;

                case AdverbPosition.Back:
                    adverbBack = " " + MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    break;
            }

            string nounPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.NounList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //冠詞
            string article = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.NounList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //前置詞が共通になっているので各名詞リスト毎にする。
            //前置詞
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(
                MasterFactory.GetMasterData<AdjectiveConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.AdjectiveList.SelectedItem.Id), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //名詞
            string noun = string.Empty;
            noun = (prePosition == string.Empty) ? "" : MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.NounList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (noun.Contains("@冠詞@")) {
                noun = noun.Replace("@冠詞@", article + " ");
            }
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //冠詞（人）
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //名詞（人）
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (person.Contains("@冠詞@")) {
                person = person.Replace("@冠詞@", personArticle + " ");
            }
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //冠詞（場所）
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //名詞（場所）
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(
                (ContainerFactory.LocationPrepositionList.SelectedItem == null) ? 0 : ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (location.Contains("@冠詞@")) {
                location = location.Replace("@冠詞@", locationArticle + " ");
            }

            //文中に副詞を付ける。
            if(adverbMiddle != string.Empty) {
                string[] splitEndWords = end.Split(' ');
                bool addAdjective = true;
                foreach (string n in adjectiveVerb.ToList()) {
                    //to beの場合、形容詞の前に副詞が付く
                    if (end.Contains("going to be")) {
                        break;
                    }
                    //動詞が付いている場合、動詞の前につく。
                    if (splitEndWords[splitEndWords.Length - 1] == n) {
                        string addWords = "";
                        for (int ct = 0; ct < splitEndWords.Length; ct++) {
                            if (ct == splitEndWords.Length - 1) {
                                addWords += " " + adverbMiddle;
                            }
                            addWords += " " + splitEndWords[ct];
                        }
                        end = addWords;
                        //動詞の前につける場合、形容詞の前にはつけない。
                        addAdjective = false;
                        break;
                    }
                }
                //形容詞の前につける
                if (addAdjective) {
                    be = adverbMiddle + " " + be;
                }
            }

            parent.ResultLabel.Text = (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(adverbFront) + subject + " " + end + " " + be + " "  + adverbBack
                + ((noun == string.Empty) ? "" : " " + prePosition + " " + nounPossessive + ((article == string.Empty) ? "" : " " + article) + " " + noun)
                + ((person == string.Empty) ? "" : " " + "with " + " " + personPossessive + ((personArticle == string.Empty) ? "" : " " + personArticle) + " " + person)
                + ((location == string.Empty) ? "" : " " + prePosition + " " + locationPossessive + ((locationArticle == string.Empty) ? "" : " " + locationArticle) + " " + location)
                + ((timeValue == string.Empty) ? "" : " " + timeValue))
                .Replace("  ", " ").Replace("  ", " ");
            
        }

        /// <summary>
        /// 動詞の出力
        /// </summary>
        private void SetVerbResultTextBox()
        {
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            
            VerbEndOfWord verbEndItem = (
                ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? (MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupIdAndStepLevel(
                                                                                        ContainerFactory.VerbList.SelectedItem.Id, ContainerFactory.StepLevelComboBox._StepLevel)).ToList()
                                                                                   : (MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupId(ContainerFactory.VerbList.SelectedVerbGroupId)).ToList()
                                                                                   )[(ContainerFactory.VerbList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.VerbList.SelectedEndOfWordItemId - 1 : 0];
            string end = string.Empty;
                end = MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetData(verbEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            string verb = MasterFactory.GetMasterData<VerbMaster>().GetData(ContainerFactory.VerbList.SelectedItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //後付けマスタが表示されない。。マスタのGet処理作り直す？
            string timeValue = MasterFactory.GetMasterData<VerbAddEndOfWordMaster>().GetGroupByGroupIdData(verbEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //副詞
            string adverbFront = string.Empty;
            string adverbMiddle = string.Empty;
            string adverbBack = string.Empty;
            
            switch (MasterFactory.GetMasterData<AdverbMaster>().GetAdverbPosFromId(ContainerFactory.AdverbList.SelectedItemIndex)) {
                case AdverbPosition.Front:
                    adverbFront = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0] + " " ;
                    break;

                case AdverbPosition.Middle:
                    adverbMiddle = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0] + " ";
                    break;

                case AdverbPosition.Back:
                    adverbBack = " " + MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    break;
            }
            //冠詞
            string article = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.NounList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //前置詞が共通になっているので各名詞リスト毎にする。
            string nounPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.NounList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //前置詞
            int prepositionid = MasterFactory.GetMasterData<VerbConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.VerbList.SelectedItem.Id);
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(prepositionid, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //名詞
            string noun =  MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.NounList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (noun.Contains("@冠詞@")) {
                noun = noun.Replace("@冠詞@", article + " ");
            }
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //冠詞
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //名詞
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (person.Contains("@冠詞@")) {
                person = person.Replace("@冠詞@", personArticle + " ");
            }
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //前置詞
            string locationPreposition = MasterFactory.GetMasterData<NounConnectionMaster>().GetGroupByGroupIdData(
                (ContainerFactory.LocationPrepositionList.SelectedItem == null) ? 0 : ContainerFactory.LocationPrepositionList.SelectedItem.Id );
            //冠詞
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //名詞
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (location.Contains("@冠詞@")) {
                location = location.Replace("@冠詞@", locationArticle + " ");
            }

            //文中の副詞を語尾に結合
            if (adverbMiddle != string.Empty) {
                string targetVerb;
                switch (verbEndItem.TimeType) {
                    case TimeType.Current:
                        targetVerb = MasterFactory.GetMasterData<VerbMaster>().GetData(ContainerFactory.VerbList.SelectedItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                        end = end.Replace(targetVerb, adverbMiddle + " " + targetVerb);
                        break;

                    case TimeType.Past:
                        targetVerb = MasterFactory.GetMasterData<EnglishWordMaster>().GetData(ContainerFactory.VerbList.SelectedItem.PastVerbId);
                        end = end.Replace(targetVerb, adverbMiddle + " " + targetVerb);
                        break;
                    case TimeType.Progressing:
                        targetVerb = MasterFactory.GetMasterData<EnglishWordMaster>().GetData(ContainerFactory.VerbList.SelectedItem.ProgressingId);
                        end = end.Replace(targetVerb, adverbMiddle + " " + targetVerb);
                        break;

                    case TimeType.PastParticiple:
                        targetVerb = MasterFactory.GetMasterData<EnglishWordMaster>().GetData(ContainerFactory.VerbList.SelectedItem.PastParticipleId);
                        end = end.Replace(targetVerb, adverbMiddle + " " + targetVerb);
                        break;

                }
            }

            //変換結果
            parent.ResultLabel.Text = (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(adverbFront) + subject + " " + end + " " + adverbBack
                + ((noun == string.Empty) ? "" : " " + prePosition + " " + nounPossessive + ((article == string.Empty) ? "" : " " + article) + " " + noun)
                + ((person == string.Empty) ? "" : " " + "with " + " " + personPossessive + ((personArticle == string.Empty) ? "" : " " + personArticle) + " " + person)
                + ((location == string.Empty) ? "" : " " + locationPreposition + " " + locationPossessive + ((locationArticle == string.Empty) ? "" : " " + locationArticle) + " " + location)
                + ((timeValue == string.Empty) ? "" : " " + timeValue))
                //.Replace(be, adverbMiddle + be)
                .Replace("  ", " ");
        }

        /// <summary>
        /// 存在の出力
        /// </summary>
        private void SetBeingResultTextBox() {
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            BeingWord tempWord = (ContainerFactory.StepLevelComboBox._StepLevel == StepLevel.ALLSTEP) ? 
                MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounAndSubjectMaster(
                    ContainerFactory.SubjectList._PersonalCategory
                    , ContainerFactory.SubjectList._Subject).ToList()[ContainerFactory.BeingList.SelectedItemIndex] 
                : MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounSubjectAndStepLevelMaster(
                    ContainerFactory.StepLevelComboBox._StepLevel
                    , ContainerFactory.SubjectList._PersonalCategory
                    , ContainerFactory.SubjectList._Subject).ToList()[ContainerFactory.BeingList.SelectedItemIndex];
            string be = MasterFactory.GetMasterData<BeingWordMaster>().GetData(tempWord.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //後付けマスタ
            List <List<string>> tempTimeValue = MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);
            
            string timeValue = (tempTimeValue.Count == 0) ? string.Empty: tempTimeValue[0][0];
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //冠詞
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];

            //名詞
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (person.Contains("@冠詞@")) {
                person = person.Replace("@冠詞@", personArticle + " ");
            }
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //前置詞
            string locationPreposition = MasterFactory.GetMasterData<NounConnectionMaster>().GetGroupByGroupIdData(
                (ContainerFactory.LocationPrepositionList.SelectedItem == null) ? 0 : ContainerFactory.LocationPrepositionList.SelectedItem.Id);
            //冠詞
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            //名詞
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
            if (location.Contains("@冠詞@")) {
                location = location.Replace("@冠詞@", locationArticle + " ");
            }

            //副詞
            string adverbFront = string.Empty;
            string adverbMiddle = string.Empty;
            string adverbBack = string.Empty;
            switch (MasterFactory.GetMasterData<AdverbMaster>().GetAdverbPosFromId(ContainerFactory.AdverbList.SelectedItemIndex)) {
                case AdverbPosition.Front:
                    adverbFront = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0] + " ";
                    break;

                case AdverbPosition.Middle:
                    adverbMiddle = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0] + " ";
                    break;

                case AdverbPosition.Back:
                    adverbBack = " " + MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];
                    break;
            }
            //前置詞が共通になっているので各名詞リスト毎にする。
            //前置詞
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(
                MasterFactory.GetMasterData<BeingConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.BeingList.SelectedItemId), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[0];


            //文中に副詞を付ける。
            if (adverbMiddle != string.Empty) {
                if (be.Contains("going to be")) {
                    be = be.Replace("going to be", "going to be " + adverbMiddle);
                } else {
                    be = be + " " + adverbMiddle;
                }
            }

            parent.ResultLabel.Text = (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(adverbFront) + subject + " " + be + adverbBack
                + ((person == string.Empty) ? "" : " " + "with " + " " + personPossessive + ((personArticle == string.Empty) ? "" : " " + personArticle) + " " + person)
                + ((location == string.Empty) ? "" : " " + locationPreposition + " " + locationPossessive + ((locationArticle == string.Empty) ? "" : " " + locationArticle) + " " + location)
                + ((timeValue == string.Empty) ? "" : " " + timeValue))
                .Replace("  ", " ");
        }



        /// <summary>
        /// 日本語（形容詞）の出力
        /// </summary>
        private void SetAdjectiveSourceTextBox() {
            //主語
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1] + ContainerFactory.SubjectList.SelectedTENIOHA;
            //形容詞
            string adjective = MasterFactory.GetMasterData<AdjectiveMaster>().GetData(ContainerFactory.AdjectiveList.SelectedItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];

            AdjectiveEndOfWord adjectiveEndItem = (
                ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? (MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeItemGroupByGroupIdAndStepLevel(
                                                                             ContainerFactory.AdjectiveList.SelectedItem.GroupId,
                                                                             ContainerFactory.StepLevelComboBox._StepLevel,
                                                                             ContainerFactory.AdjectiveList.SelectedItem.Category)).ToList()
                                                                        : (MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetRangeItemGroupByGroupId(
                                                                            ContainerFactory.AdjectiveList.SelectedItem.GroupId,
                                                                             ContainerFactory.AdjectiveList.SelectedItem.Category)).ToList()
                                                                        )[(ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 : 0];
            AdjectiveNumberOfPeopleEndOfWord adjectiveNumEndItem = (
                ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishItemGroupByGroupIdAndPersonCategoryAndStepLevel(
                                                                                        ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId,
                                                                                        ContainerFactory.SubjectList._PersonalCategory,
                                                                                        ContainerFactory.StepLevelComboBox._StepLevel,
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.Category)
                                                                                   : MasterFactory.GetMasterData<AdjectiveNumberOfPeopleEndOfWordMaster>().GetRangeEnglishItemGroupByGroupIdAndPersonCategory(
                                                                                       ContainerFactory.AdjectiveList.SelectedAdjectiveGroupId,
                                                                                       ContainerFactory.SubjectList._PersonalCategory,
                                                                                        ContainerFactory.AdjectiveList.SelectedItem.Category))[(ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.AdjectiveList.SelectedEndOfWordItemId - 1 : 0];

            //後付け  
            //語尾を持ってくる際、文字列でなく、語尾ID/人数語尾IDを持ってくる。
            string timeWord = string.Empty;
            switch (ContainerFactory.SubjectList._PersonalCategory) {
                case PersonalPronounCategory.ThirdSingle:
                    timeWord = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(adjectiveEndItem.Id, 0)[1];
                    break;

                default:
                    timeWord = MasterFactory.GetMasterData<AdjectiveAddEndOfWordMaster>().GetData(0, adjectiveNumEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
                    break;

            }            //語尾
            string verb = MasterFactory.GetMasterData<AdjectiveEndOfWordMaster>().GetData(adjectiveEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            
            //副詞
            string adverb = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //前置詞が共通になっているので各名詞リスト毎にする。
            //前置詞
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(
                MasterFactory.GetMasterData<AdjectiveConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.AdjectiveList.SelectedItem.Id), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string nounPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.NounList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string article = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.NounList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string noun = (prePosition == string.Empty)? "" : MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.NounList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            person = (person == string.Empty) ? person : person + "と";
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];

            //名詞
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];


            parent.SourceViewLabel.Text = subject + timeWord
                + ((person == string.Empty) ? string.Empty : personPossessive + personArticle + person)
                + ((location == string.Empty)? string.Empty : locationPossessive + locationArticle + location)
                + ((noun == string.Empty) ? string.Empty : nounPossessive + article + noun + prePosition)
                + adverb + adjective + verb;
        }
        /// <summary>
         /// 日本語（動詞）の出力
         /// </summary>
        private void SetVerbSourceTextBox() {
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1] + ContainerFactory.SubjectList.SelectedTENIOHA;
            VerbEndOfWord verbEndItem = (
    ContainerFactory.StepLevelComboBox._StepLevel != StepLevel.ALLSTEP ? (MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupIdAndStepLevel(
                                                                            ContainerFactory.VerbList.SelectedItem.Id, ContainerFactory.StepLevelComboBox._StepLevel)).ToList()
                                                                       : (MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetRangeGroupByGroupId(ContainerFactory.VerbList.SelectedVerbGroupId)).ToList()
                                                                       )[(ContainerFactory.VerbList.SelectedEndOfWordItemId - 1 >= 0) ? ContainerFactory.VerbList.SelectedEndOfWordItemId - 1 : 0];

            string end = string.Empty;
            end = MasterFactory.GetMasterData<VerbEndOfWordMaster>().GetData(verbEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string verb = MasterFactory.GetMasterData<VerbMaster>().GetData(ContainerFactory.VerbList.SelectedItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //後付けマスタが表示されない。。マスタのGet処理作り直す？
            string timeValue = MasterFactory.GetMasterData<VerbAddEndOfWordMaster>().GetGroupByGroupIdData(verbEndItem.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];

            //副詞
            string adverb = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string nounPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.NounList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string article = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.NounList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //前置詞
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(
                MasterFactory.GetMasterData<VerbConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.VerbList.SelectedItem.Id), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string noun = (MasterFactory.GetMasterData<NounMaster>().GetData(
                ContainerFactory.NounList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1] == string.Empty) ? 
                string.Empty 
                : ((prePosition == string.Empty) ?
                MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.NounList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1] + "に"
                : string.Empty);
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            person = (person == string.Empty) ? person : person + "と";
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];

            parent.SourceViewLabel.Text = subject + timeValue + 
                ((person == string.Empty) ? string.Empty : personPossessive + personArticle + person)
                + ((location == string.Empty) ? string.Empty : locationPossessive + locationArticle + location)
                + ((noun == string.Empty) ? string.Empty : nounPossessive + article + noun + prePosition)
                + adverb
                + verb
                + end;
        }

        /// <summary>
        /// 日本語（存在）の出力
        /// </summary>
        private void SetBeingSourceTextBox() {
            string subject = MasterFactory.GetMasterData<SubjectMaster>().GetData(ContainerFactory.SubjectList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1] + ContainerFactory.SubjectList.SelectedTENIOHA;
            BeingWord tempWord = (ContainerFactory.StepLevelComboBox._StepLevel == StepLevel.ALLSTEP) ?
                 MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounAndSubjectMaster(
                     ContainerFactory.SubjectList._PersonalCategory
                     , ContainerFactory.SubjectList._Subject).ToList()[ContainerFactory.BeingList.SelectedItemIndex]
                 : MasterFactory.GetMasterData<BeingWordMaster>().GetRangeIdPersonalPronounSubjectAndStepLevelMaster(
                     ContainerFactory.StepLevelComboBox._StepLevel
                     , ContainerFactory.SubjectList._PersonalCategory
                     , ContainerFactory.SubjectList._Subject).ToList()[ContainerFactory.BeingList.SelectedItemIndex];
            string endVerb = MasterFactory.GetMasterData<BeingWordMaster>().GetData(tempWord.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //後付けマスタ
            List<List<string>> tempTimeValue = MasterFactory.GetMasterData<BeingAddWordMaster>().GetRangeData(tempWord.Id, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType);

            string timeWord = (tempTimeValue.Count == 0) ? string.Empty : tempTimeValue[0][1];
            //副詞
            string adverb = MasterFactory.GetMasterData<AdverbMaster>().GetData(ContainerFactory.AdverbList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string personPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string personArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //前置詞
            string prePosition = MasterFactory.GetMasterData<PrepositionMaster>().GetData(
                MasterFactory.GetMasterData<BeingConnectionMaster>().GetGroupByGroupIdData(ContainerFactory.BeingList.SelectedItemId), ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string person = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.PersonPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            person += (person == string.Empty) ? string.Empty : "と";
            string locationPossessive = MasterFactory.GetMasterData<PossessiveMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedPossessiveId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            string locationArticle = MasterFactory.GetMasterData<ArticleMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedArticleId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];
            //名詞
            string location = MasterFactory.GetMasterData<NounMaster>().GetData(ContainerFactory.LocationPrepositionList.SelectedItemId, ContainerFactory.JapaneseReadTypeComboBox._JapaneseReadType)[1];

            parent.SourceViewLabel.Text = subject + timeWord
                + ((person == string.Empty) ? string.Empty : personPossessive + personArticle + person)
                + ((location == string.Empty) ? string.Empty : locationPossessive + locationArticle + location)
                 + adverb + endVerb;
        }
    }
}
